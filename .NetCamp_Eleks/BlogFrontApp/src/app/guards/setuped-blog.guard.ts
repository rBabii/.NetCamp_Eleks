import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import NotificationStore from '../stores/notification.store';
import {Observable} from 'rxjs';
import UserStore from '../stores/user.store';

@Injectable({
  providedIn: 'root'
})
export class SetupedBlogGuard implements CanActivate {
  constructor(public userStore: UserStore, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.userStore.Init().then(() => {
      if (this.userStore.HasOwnBlog) {
        this.router.navigate(['/']);
        return false;
      }
    });
    return true;
  }
}
