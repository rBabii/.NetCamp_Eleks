import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../CustomValidators/CustomValidators';
import RegisterStore from '../../../stores/register.store';
import {Router} from '@angular/router';
import {RegisterRequest} from '../../../services/BlogAPI/Auth/Models/Request/RegisterRequest';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  Email = new FormControl('', [Validators.required, Validators.email]);
  Password = new FormControl('', [Validators.required, CustomValidators.PasswordRegex]);
  ConfirmPassword = new FormControl('', [Validators.required, CustomValidators.PasswordRegex]);

  RegisterForm = new FormGroup({
    email: this.Email,
    password: this.Password,
    confirmPassword: this.ConfirmPassword
  }, { validators: CustomValidators.PasswordMatch} );

  HidePassword = true;
  HideConfirmPassword = true;

  constructor(public registerStore: RegisterStore, private router: Router) { }

  ngOnInit(): void {
  }

  async OnSubmit(): Promise<void> {
    if (this.RegisterForm.valid) {
      const res = await this.registerStore
        .Register(this.RegisterForm.value as RegisterRequest);

      if (!res) {
        await this.router.navigate(['/login']);
      }
      if (res && res.errorMessages) {
        this.RegisterForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }
    }
  }
}
