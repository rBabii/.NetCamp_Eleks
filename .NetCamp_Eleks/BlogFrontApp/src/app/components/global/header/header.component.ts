import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import LoginStore from '../../../stores/login.store';
import NotificationStore from '../../../stores/notification.store';
import UserStore from '../../../stores/user.store';
import {LoaderService} from '../../../loader-service/loader-service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router,
              public loginStore: LoginStore,
              public notificationStore: NotificationStore,
              public userStore: UserStore,
              public loaderService: LoaderService ) {  }
  async Logout(): Promise<void>{
    await this.loginStore.Logout();
    this.userStore.SetDefault();
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
  }
}
