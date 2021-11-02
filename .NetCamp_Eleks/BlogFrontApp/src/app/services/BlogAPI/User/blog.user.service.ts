import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Error} from '../../Common/Models/Error';
import {IsUserSetupedResponse} from './Models/Response/IsUserSetupedResponse';
import {UpdateUserRequest} from './Models/Request/UpdateUserRequest';
import {GetUserResponse} from './Models/Response/GetUserResponse';

@Injectable({
  providedIn: 'root'
})
export class BlogUserService {

  constructor(private http: HttpClient) { }
  public async IsUserSetuped(): Promise<IsUserSetupedResponse | Error> {
    const url = `http://localhost:5001/api/user/IsUserSetuped`;

    const result = await this.http.post<IsUserSetupedResponse>(url, '').toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
  public async UpdateUser(updateUserRequest: UpdateUserRequest): Promise<void | Error> {
    const  url = `http://localhost:5001/api/user/Update`;
    const result = await this.http.post<void>(url, updateUserRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
  public async GetUser(): Promise<GetUserResponse | Error> {
    const  url = `http://localhost:5001/api/user/get`;
    const result = await this.http.get<GetUserResponse>(url).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
}
