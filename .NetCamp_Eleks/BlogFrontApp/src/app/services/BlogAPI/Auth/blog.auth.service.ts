import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {VerifyUserEmailRequest} from './Models/Request/VerifyUserEmailRequest';
import {Error} from '../../Common/Models/Error';
import {RegisterRequest} from './Models/Request/RegisterRequest';
import {SendResetPasswordRequest} from './Models/Request/SendResetPasswordRequest';
import {ResetPasswordRequest} from './Models/Request/ResetPasswordRequest';

@Injectable({
  providedIn: 'root'
})
export class BlogAuthService {

  constructor(private http: HttpClient) { }
  public async Verify(verifyUserEmailRequest: VerifyUserEmailRequest): Promise<void | Error> {
    const url = `http://localhost:5001/api/auth/verify`;
    let result: void | Error;

    result = await this.http.post<void>(url, verifyUserEmailRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }

  public async Register(registerRequest: RegisterRequest): Promise<void | Error>{
    const url = `http://localhost:5001/api/auth/register`;
    let result: void | Error;

    result = await this.http.post<void>(url, registerRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }


  public async SendResetPasswordEmail(sendResetPasswordRequest: SendResetPasswordRequest): Promise<void | Error>{
    const url = `http://localhost:5001/api/auth/SendResetPasswordLink`;
    let result: void | Error;

    result = await this.http.post<void>(url, sendResetPasswordRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }

  public async ResetPassword(resetPasswordRequest: ResetPasswordRequest): Promise<void | Error>{
    const url = `http://localhost:5001/api/auth/ResetPassword`;
    let result: void | Error;

    result = await this.http.post<void>(url, resetPasswordRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }

  public async SendEmailVerificationLink(): Promise<void | Error>{
    const url = `http://localhost:5001/api/auth/SendEmailVerificationLink`;
    let result: void | Error;

    result = await this.http.post<void>(url, '').toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }

}
