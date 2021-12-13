import {Component, OnInit, ViewChild} from '@angular/core';
import NotificationStore from '../../../stores/notification.store';
import {MatMenuTrigger} from '@angular/material/menu';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  @ViewChild('menuTrigger') menuTrigger: MatMenuTrigger;
  constructor(public notificationStore: NotificationStore) { }

  ngOnInit(): void {
    this.notificationStore.Init();
  }

}
