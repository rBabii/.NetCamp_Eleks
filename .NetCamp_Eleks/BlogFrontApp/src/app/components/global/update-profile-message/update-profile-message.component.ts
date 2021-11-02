import { Component, OnInit } from '@angular/core';
import NotificationStore from '../../../stores/notification.store';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-update-profile-message',
  templateUrl: './update-profile-message.component.html',
  styleUrls: ['./update-profile-message.component.css']
})
export class UpdateProfileMessageComponent implements OnInit {

  constructor(public notificationStore: NotificationStore, public dialogRef: MatDialogRef<UpdateProfileMessageComponent>) { }
  ContinueClick(): void {
    this.dialogRef.close();
  }
  ngOnInit(): void {
  }

}
