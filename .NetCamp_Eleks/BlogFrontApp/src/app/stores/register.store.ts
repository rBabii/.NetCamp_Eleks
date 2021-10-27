import {makeObservable} from 'mobx';
import {action, observable} from 'mobx-angular';
import {Injectable} from '@angular/core';
import {Error} from '../services/Common/Models/Error';
import {BlogAuthService} from '../services/BlogAPI/Auth/blog.auth.service';
import {RegisterRequest} from '../services/BlogAPI/Auth/Models/Request/RegisterRequest';

@Injectable({
  providedIn: 'root'
})
class RegisterStore {
  @observable
  Result: void | Error = null;
  constructor(private blogAuthService: BlogAuthService) {
    makeObservable(this);
  }

  @action
  public async Register(registerRequest: RegisterRequest): Promise<void | Error> {
    this.Result = await this.blogAuthService.Register(registerRequest);
    return this.Result;
  }
}

export default RegisterStore;
