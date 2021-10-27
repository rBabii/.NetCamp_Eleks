import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse, HttpClient
} from '@angular/common/http';

import {BehaviorSubject, from, Observable, of, throwError} from 'rxjs';
import {catchError, filter, finalize, switchMap, take} from 'rxjs/operators';
import {AUTH_HOST, LOGIN_REFRESH} from '../services/endpoints';
import {RefreshTokenRequest} from '../services/AuthAPI/Auth/Models/Request/RefreshTokenRequest';
import {AuthentificatedUserResponse} from '../services/AuthAPI/Auth/Models/Response/AuthentificatedUserResponse';

/** Pass untouched request through to the next request handler. */
@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
  private RefreshFlag = true;
  private refreshTokenInProgress = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    if (req.url.includes(LOGIN_REFRESH)){
      return next.handle(req);
    }
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error && error.status === 401) {
          // 401 errors are most likely going to be because we have an expired token that we need to refresh.
          if (this.refreshTokenInProgress) {
            // If refreshTokenInProgress is true, we will wait until refreshTokenSubject has a non-null value
            // which means the new token is ready and we can retry the request again
            return this.refreshTokenSubject.pipe(
              filter(result => result !== null),
              take(1),
              switchMap(() => next.handle(this.addAuthenticationToken(req)))
            );
          } else {
            this.refreshTokenInProgress = true;

            // Set the refreshTokenSubject to null so that subsequent API calls will wait until the new token has been retrieved
            this.refreshTokenSubject.next(null);

            return this.refreshAccessToken().pipe(
              switchMap((success: boolean) => {
                this.refreshTokenSubject.next(success);
                return next.handle(this.addAuthenticationToken(req));
              }),
              // When the call to refreshToken completes we reset the refreshTokenInProgress to false
              // for the next time the token needs to be refreshed
              finalize(() => this.refreshTokenInProgress = false)
            );
          }
        } else {
          return throwError(error);
        }
      })
    );
  }

  public async Refresh(): Promise<void> {
    if (this.RefreshFlag){
      this.RefreshFlag = false;
      const refreshToken = localStorage.getItem('refreshToken');
      if (refreshToken) {
        const refreshRequest: RefreshTokenRequest = {
          refreshToken
        };
        const url = `${AUTH_HOST}/${LOGIN_REFRESH}`;
        const loginResponse = await this.http.post<AuthentificatedUserResponse>(url, refreshRequest).toPromise().catch((error) => {
          console.log(error);
        });
        if (loginResponse && loginResponse.accessToken && loginResponse.refreshToken){
          localStorage.setItem('accessToken', loginResponse.accessToken);
          localStorage.setItem('refreshToken', loginResponse.refreshToken);
        }else {
          localStorage.removeItem('accessToken');
          localStorage.removeItem('refreshToken');
        }
      }
      this.RefreshFlag = true;
    }
  }

  private refreshAccessToken(): Observable<any> {
    return from(this.Refresh());
  }

  private addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
    // If we do not have a token yet then we should not set the header.
    // Here we could first retrieve the token from where we store it.
    const token = localStorage.getItem('accessToken');
    if (!token) {
      return request;
    }
    return request.clone({
      headers: request.headers.set('Authorization', 'Bearer ' + token)
    });
  }
}
