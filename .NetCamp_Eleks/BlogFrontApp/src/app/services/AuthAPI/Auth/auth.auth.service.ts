import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {LoginRequest} from './Models/Request/LoginRequest';
import {AuthentificatedUserResponse} from './Models/Response/AuthentificatedUserResponse';
import {Error} from '../../Common/Models/Error';
import {RefreshTokenRequest} from './Models/Request/RefreshTokenRequest';
import {IsVerifiedResponse} from './Models/Response/IsVerifiedResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthAuthService {
  RefreshFlag = true;
  constructor(private http: HttpClient) { }

  public async Login(loginRequest: LoginRequest): Promise<AuthentificatedUserResponse | Error>{
    const url = `http://localhost:5000/api/auth/login`;
    return await this.http.post<AuthentificatedUserResponse>(url, loginRequest).toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as  Error;
    });
  }

  public async Refresh(refreshTokenRequest: RefreshTokenRequest): Promise<AuthentificatedUserResponse | Error> {
    if (this.RefreshFlag){
      this.RefreshFlag = false;
      const url = `http://localhost:5000/api/auth/login/refresh`;
      const result = await this.http.post<AuthentificatedUserResponse>(url, refreshTokenRequest).toPromise()
        .catch((errorResponse: HttpErrorResponse) => {
        return errorResponse.error as  Error;
      });
      this.RefreshFlag = true;
      return result;
    }
  }

  public async Logout(): Promise<void | Error> {
    const url = `http://localhost:5000/api/auth/logout`;
    const result = await this.http.post<void>(url, '').toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
        return errorResponse.error as  Error;
      });
    return  result;
  }

  public async IsVerified(): Promise<IsVerifiedResponse | Error> {
    const url = `http://localhost:5000/api/auth/IsVerified`;
    const result = await this.http.post<IsVerifiedResponse>(url, '').toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
        return errorResponse.error as  Error;
      });
    return  result;
  }
}
