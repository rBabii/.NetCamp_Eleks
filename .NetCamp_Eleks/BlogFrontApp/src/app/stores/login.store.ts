import {makeObservable} from 'mobx';
import {action, observable} from 'mobx-angular';
import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Error} from '../services/Common/Models/Error';
import {AuthentificatedUserResponse} from '../services/AuthAPI/Auth/Models/Response/AuthentificatedUserResponse';
import {AuthAuthService} from '../services/AuthAPI/Auth/auth.auth.service';
import {LoginRequest} from '../services/AuthAPI/Auth/Models/Request/LoginRequest';
import {RefreshTokenRequest} from '../services/AuthAPI/Auth/Models/Request/RefreshTokenRequest';

@Injectable({
  providedIn: 'root'
})
class LoginStore {
  @observable
  AuthResult: AuthentificatedUserResponse | Error = null;

  @observable
  IsAuthentificated = false;

  constructor(private authAuthService: AuthAuthService, private jwtHelper: JwtHelperService) {
    makeObservable(this);
    this.CheckIsAuth();
  }

  @action
  public async Login(loginRequest: LoginRequest): Promise<AuthentificatedUserResponse | Error> {
    this.AuthResult = await this.authAuthService.Login(loginRequest);
    if (this.AuthResult && 'accessToken' in this.AuthResult) {
      localStorage.setItem('accessToken', this.AuthResult.accessToken);
      localStorage.setItem('refreshToken', this.AuthResult.refreshToken);
      await this.CheckIsAuth();
      return this.AuthResult;
    }
    return this.AuthResult;
  }

  @action
  public async Refresh(): Promise<AuthentificatedUserResponse | Error> {
    const refreshToken = localStorage.getItem('refreshToken');
    if (refreshToken) {
      const refreshTokenRequest: RefreshTokenRequest = {
        refreshToken
      };

      this.AuthResult = await this.authAuthService.Refresh(refreshTokenRequest);
      if (this.AuthResult && 'accessToken' in this.AuthResult){
        localStorage.setItem('accessToken', this.AuthResult.accessToken);
        localStorage.setItem('refreshToken', this.AuthResult.refreshToken);
        return this.AuthResult;
      }
    }
  }

  @action
  public async Logout(): Promise<void | Error> {
    const result = await this.authAuthService.Logout();
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    await this.CheckIsAuth();
    return result;
  }

  @action
  public async CheckIsAuth(): Promise<void> {
    if (this.jwtHelper.isTokenExpired()){
      await this.Refresh();
    }
    this.IsAuthentificated = !this.jwtHelper.isTokenExpired();
  }
}

export default LoginStore;
