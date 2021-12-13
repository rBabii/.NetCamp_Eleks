import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {BlogAuthService} from '../../../services/BlogAPI/Auth/blog.auth.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../CustomValidators/CustomValidators';
import {ResetPasswordRequest} from '../../../services/BlogAPI/Auth/Models/Request/ResetPasswordRequest';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  Email = new FormControl('', [Validators.required, Validators.email]);
  Password = new FormControl('', [Validators.required, CustomValidators.PasswordRegex]);
  ConfirmPassword = new FormControl('', [Validators.required, CustomValidators.PasswordRegex]);

  ResetPasswordForm = new FormGroup({
    email: this.Email,
    password: this.Password,
    confirmPassword: this.ConfirmPassword
  }, { validators: CustomValidators.PasswordMatch} );

  HidePassword = true;
  HideConfirmPassword = true;


  Token: string;
  constructor(private activatedRoute: ActivatedRoute, private blogAuthService: BlogAuthService, public router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(async params => {
      const token = params.token;
      if (!token) {
        this.router.navigate(['/']);
      }

      this.Token = token;
    });
  }

  async OnSubmit(): Promise<void> {
    if (this.ResetPasswordForm.valid) {
      const request = this.ResetPasswordForm.value as ResetPasswordRequest;
      request.token = this.Token;
      const res = await this.blogAuthService
        .ResetPassword(request);

      if (!res) {
        await this.router.navigate(['/login']);
      }
      if (res && res.errorMessages) {
        this.ResetPasswordForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }
    }
  }

}
