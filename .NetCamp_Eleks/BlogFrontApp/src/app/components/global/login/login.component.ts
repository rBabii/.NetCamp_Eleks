import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../CustomValidators/CustomValidators';
import LoginStore from '../../../stores/login.store';
import {Router} from '@angular/router';
import {LoginRequest} from '../../../services/AuthAPI/Auth/Models/Request/LoginRequest';
import {MatDialog} from '@angular/material/dialog';
import {ForgotPasswordComponent} from '../forgot-password/forgot-password.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  Email = new FormControl('', [Validators.required, Validators.email]);
  Password = new FormControl('', [Validators.required, CustomValidators.PasswordRegex]);

  LoginForm = new FormGroup({
    email: this.Email,
    password: this.Password
  });

  HidePassword = true;

  constructor(public loginStore: LoginStore, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  async OnSubmit(): Promise<void> {
    if (this.LoginForm.valid) {
      const res = await this.loginStore
        .Login(this.LoginForm.value as LoginRequest);

      if (res && 'accessToken' in res){
        await this.router.navigate(['/']);
      }
      if (res && 'errorMessages' in res){
        this.LoginForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }
    }
  }

  OpenForgotPasswordPopup(): void {
    const dialogRef = this.dialog.open(ForgotPasswordComponent, {
      width: 'auto',
      height: 'auto'
    });
  }
}
