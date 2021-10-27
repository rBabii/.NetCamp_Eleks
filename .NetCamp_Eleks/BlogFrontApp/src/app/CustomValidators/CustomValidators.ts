import {AbstractControl, ValidationErrors} from '@angular/forms';

export class CustomValidators{
  static PasswordMatch(form: AbstractControl): ValidationErrors | null {
    if (form.get('password').value !== form.get('confirmPassword').value) {
      return {
        passwordNotMatch: true
      };
    }
    return null;
  }
  static UserNameRegex(field: AbstractControl): ValidationErrors | null {
    const regex = new RegExp('^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$');
    if (!regex.test(field.value)) {
      return {
        userNameRegexNotPass: true
      };
    }
    return null;
  }
  static PasswordRegex(field: AbstractControl): ValidationErrors | null {
    const regex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$');
    if (!regex.test(field.value)) {
      return {
        passwordRegexNotPass: true
      };
    }
    return null;
  }

  static PhoneRegex(field: AbstractControl): ValidationErrors | null {
    const regex = new RegExp('^(\\d+\\s?(x|\\.txe?)\\s?)?((\\)(\\d+[\\s\\-\\.]?)?\\d+\\(|\\d+)[\\s\\-\\.]?)*(\\)([\\s\\-\\.]?\\d+)?\\d+\\+?\\((?!\\+.*)|\\d+)(\\s?\\+)?$', 'i');
    const value = field.value.split('').reverse().join('');
    if (regex.test(value)) {
      return {
        phoneRegexNotPass: true
      };
    }
    return null;
  }
}
