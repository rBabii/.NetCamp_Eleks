import { Component } from '@angular/core';
import NotificationStore from './stores/notification.store';
import UserStore from './stores/user.store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'BlogFrontApp';
  constructor(private notificationStore: NotificationStore, private userStore: UserStore) {
  }
}
