import {Injectable} from '@angular/core';
import {BlogUserService} from '../services/BlogAPI/User/blog.user.service';
import {makeObservable} from 'mobx';
import LoginStore from './login.store';
import {GetUserResponse} from '../services/BlogAPI/User/Models/Response/GetUserResponse';
import {computed, observable} from 'mobx-angular';
import {Error} from '../services/Common/Models/Error';
import NotificationStore from './notification.store';

@Injectable({
  providedIn: 'root'
})
class UserStore {
  @observable
  User: GetUserResponse = {
    firstName: '',
    lastName: '',
    phoneNumber: '',
    birthDate: null,
    email: '',
    gender: null,
    blogUrl: '',
    imageUrl: 'https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg'
  };

  @observable
  Error: Error;

  constructor(private loginStore: LoginStore, private blogUserService: BlogUserService, private notificationStore: NotificationStore) {
    makeObservable(this);
    this.Init();
  }
  SetDefault(): void{
    this.User = {
      firstName: '',
      lastName: '',
      phoneNumber: '',
      birthDate: null,
      email: '',
      gender: null,
      blogUrl: '',
      imageUrl: 'https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg'
    };
  }

  async Init(): Promise<void>{
      this.SetDefault();
      if (this.loginStore.IsAuthentificated){
        const res = await this.blogUserService.GetUser();
        if (res && 'email' in res) {
          this.User = res;
        }else if (res && 'errorMessages' in res) {
          this.Error = res;
        }
      }
  }

  @computed
  get HasOwnBlog(): boolean {
    if (this.User && this.User.blogUrl && this.User.blogUrl !== '') {
      return true;
    }
    return false;
  }
}

export default UserStore;
