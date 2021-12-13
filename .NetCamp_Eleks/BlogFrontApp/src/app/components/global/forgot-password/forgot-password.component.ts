import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MatDialogRef} from '@angular/material/dialog';
import {BlogAuthService} from '../../../services/BlogAPI/Auth/blog.auth.service';
import {SendResetPasswordRequest} from '../../../services/BlogAPI/Auth/Models/Request/SendResetPasswordRequest';
import {LoginRequest} from '../../../services/AuthAPI/Auth/Models/Request/LoginRequest';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  Email = new FormControl('', [Validators.required, Validators.email]);

  ForgotPasswordForm = new FormGroup({
    email: this.Email
  });

  constructor(public blogAuthService: BlogAuthService, public dialogRef: MatDialogRef<ForgotPasswordComponent>) { }

  ngOnInit(): void {
  }

  async OnSubmit(): Promise<void> {
    if (this.ForgotPasswordForm.valid) {
      const res = await this.blogAuthService
        .SendResetPasswordEmail(this.ForgotPasswordForm.value as SendResetPasswordRequest);

      if (!res) {
        this.dialogRef.close();
      }
      if (res && 'errorMessages' in res){
        this.ForgotPasswordForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }
    }
  }

}
