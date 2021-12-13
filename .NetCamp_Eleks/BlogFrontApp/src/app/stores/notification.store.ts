import {makeObservable} from 'mobx';
import {computed, observable} from 'mobx-angular';
import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Notification} from '../models/Notification';
import UserStore from './user.store';

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

  constructor(private jwtHelper: JwtHelperService, public userStore: UserStore) {
    makeObservable(this);
  }
  public async Init(): Promise<void> {
      await this.userStore.Init();

      this.Notifications = [];
      if (!this.jwtHelper.isTokenExpired()){
        const IsVerified = this.userStore.IsVerified;
        this.IsVerified = IsVerified;
        if (!this.IsVerified){
          if (!this.Notifications.find(n => n.key === 'IsVerified')){
            this.Notifications.push({
              key: 'IsVerified',
              message: 'Your email is not verified.'
            });
          }
        }

        const IsUserSetuped = this.userStore.IsUserSetuped;
        this.IsUserSetuped = IsUserSetuped;
        if (!this.IsUserSetuped){
          if (!this.Notifications.find((n => n.key === 'IsUserSetuped'))){
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
