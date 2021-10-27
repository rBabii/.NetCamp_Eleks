import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import NotificationStore from '../stores/notification.store';

@Injectable({
  providedIn: 'root'
})
export class SetupedUserGuard implements CanActivate {
  constructor(public notificationStore: NotificationStore, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.notificationStore.IsUserSetuped){
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
