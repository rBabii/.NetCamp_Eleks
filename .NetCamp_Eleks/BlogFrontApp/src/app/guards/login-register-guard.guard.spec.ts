import { TestBed } from '@angular/core/testing';

import { LoginRegisterGuardGuard } from './login-register-guard.guard';

describe('LoginRegisterGuardGuard', () => {
  let guard: LoginRegisterGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LoginRegisterGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
