import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import UserStore from '../stores/user.store';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VerifiedUserGuard implements CanActivate {
  constructor(public userStore: UserStore, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.userStore.Init().then(() => {
      if (this.userStore.IsVerified) {
        this.router.navigate(['/']);
        return false;
      }
    });
    return true;
  }
}
