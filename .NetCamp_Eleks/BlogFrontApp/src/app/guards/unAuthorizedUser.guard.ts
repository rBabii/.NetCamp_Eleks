import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import LoginStore from '../stores/login.store';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnAuthorizedUserGuard implements CanActivate {
  constructor(public loginStore: LoginStore, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.loginStore.IsAuthentificated){
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
