import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import LoginStore from '../../../stores/login.store';
import NotificationStore from '../../../stores/notification.store';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router, public loginStore: LoginStore, public notificationStore: NotificationStore) {  }

  ngOnInit(): void {
  }
}
