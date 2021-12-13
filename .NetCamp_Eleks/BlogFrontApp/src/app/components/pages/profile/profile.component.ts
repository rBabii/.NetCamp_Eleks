import { Component, OnInit } from '@angular/core';
import {Gender} from '../../../services/BlogAPI/User/Models/Enums/Gender';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UpdateUserRequest} from '../../../services/BlogAPI/User/Models/Request/UpdateUserRequest';
import {BlogUserService} from '../../../services/BlogAPI/User/blog.user.service';
import NotificationStore from '../../../stores/notification.store';
import {Router} from '@angular/router';
import UserStore from '../../../stores/user.store';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  GenderEnum = Gender;

  FirstName = new FormControl('', [Validators.required]);
  LastName = new FormControl('', [Validators.required]);
  GenderDdl = new FormControl('', [Validators.required]);
  BirthDate = new FormControl('', [Validators.required]);
  PhoneNumber = new FormControl('', [Validators.required]); // , CustomValidators.PhoneRegex]);

  UpdateUserForm = new FormGroup({
    firstName: this.FirstName,
    lastName: this.LastName,
    gender: this.GenderDdl,
    birthDate: this.BirthDate,
    phoneNumber: this.PhoneNumber
  });

  constructor(public userStore: UserStore,
              private blogUserService: BlogUserService,
              public notificationStore: NotificationStore,
              private router: Router) { }
  async OnSubmit(): Promise<void> {
    if (this.UpdateUserForm.valid) {
      if (this.userStore.User){
        const value = this.UpdateUserForm.value as UpdateUserRequest;
        if (
          !(Gender[this.userStore.User.gender]?.toString() === value.gender.toString()
          && this.userStore.User.phoneNumber === value.phoneNumber
          && this.userStore.User.birthDate === value.birthDate
          && this.userStore.User.lastName === value.lastName
          && this.userStore.User.firstName === value.firstName)
        ){
          const res = await this.blogUserService
            .UpdateUser(this.UpdateUserForm.value as UpdateUserRequest);
          if (!res) {
            await this.ngOnInit();
          }
          if (res && res.errorMessages) {
            this.UpdateUserForm.setErrors({
              responseErrorMessages: res.errorMessages
            });
          }
        }
      }else {
        const res = await this.blogUserService
          .UpdateUser(this.UpdateUserForm.value as UpdateUserRequest);
        if (!res) {
          await this.ngOnInit();
        }
        if (res && res.errorMessages) {
          this.UpdateUserForm.setErrors({
            responseErrorMessages: res.errorMessages
          });
        }
      }
    }
  }

  async ngOnInit(): Promise<void> {
    await this.notificationStore.Init();
    if (this.userStore.User){
      this.FirstName.setValue(this.userStore.User.firstName);
      this.LastName.setValue(this.userStore.User.lastName);
      this.GenderDdl.setValue(Gender[this.userStore.User.gender]?.toString());
      this.BirthDate.setValue(this.userStore.User.birthDate);
      this.PhoneNumber.setValue(this.userStore.User.phoneNumber);
    }
  }

  UploadFinished($event: any): void {
    this.ngOnInit();
  }
}
