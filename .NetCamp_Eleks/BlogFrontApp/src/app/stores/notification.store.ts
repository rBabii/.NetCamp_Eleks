import {makeObservable} from 'mobx';
import {computed, observable} from 'mobx-angular';
import {AuthAuthService} from '../services/AuthAPI/Auth/auth.auth.service';
import {Injectable} from '@angular/core';
import {BlogUserService} from '../services/BlogAPI/User/blog.user.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Notification} from '../models/Notification';

@Injectable({
  providedIn: 'root'
})
class NotificationStore {
  @observable
  IsVerified = false;

  @observable
  IsUserSetuped = false;

  @observable
  Notifications: Notification[] = [];

  constructor(private authAuthService: AuthAuthService, private blogUserService: BlogUserService, private jwtHelper: JwtHelperService) {
    makeObservable(this);
    this.Init();
  }
  public async Init(): Promise<void> {
    this.Notifications = [];
    if (!this.jwtHelper.isTokenExpired()){
      const IsVerified = await this.authAuthService.IsVerified();
      if (IsVerified && 'isVerified' in IsVerified) {
        this.IsVerified = IsVerified.isVerified;
        if (!this.IsVerified){
          this.Notifications.push({
              key: 'IsVerified',
              message: 'Your email is not verified.'
          });
        }
      }

      const IsUserSetuped = await this.blogUserService.IsUserSetuped();
      if (IsUserSetuped && 'isUserSetuped' in IsUserSetuped) {
        this.IsUserSetuped = IsUserSetuped.isUserSetuped;
        if (!this.IsUserSetuped){
          this.Notifications.push({
              key: 'IsUserSetuped',
              message: 'Your Account is not fully setuped.'
          });
        }
      }
    }
  }

  @computed
  get Count(): number{
    return this.Notifications.length;
  }
}

export default NotificationStore;
