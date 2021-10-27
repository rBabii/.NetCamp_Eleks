import { Component, OnInit } from '@angular/core';
import {BlogUserService} from '../../../services/BlogAPI/User/blog.user.service';
import NotificationStore from '../../../stores/notification.store';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../CustomValidators/CustomValidators';
import { Gender } from 'src/app/services/BlogAPI/User/Models/Enums/Gender';
import {UpdateUserRequest} from '../../../services/BlogAPI/User/Models/Request/UpdateUserRequest';
import {Router} from '@angular/router';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {
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

  constructor(private blogUserService: BlogUserService, public notificationStore: NotificationStore, private router: Router) { }

  async OnSubmit(): Promise<void> {
    if (this.UpdateUserForm.valid) {
      const res = await this.blogUserService
        .UpdateUser(this.UpdateUserForm.value as UpdateUserRequest);

      if (!res) {
        await this.notificationStore.Init();
        await this.router.navigate(['/']);
      }
      if (res && res.errorMessages) {
        this.UpdateUserForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }
    }
  }

  ngOnInit(): void {
  }

}
