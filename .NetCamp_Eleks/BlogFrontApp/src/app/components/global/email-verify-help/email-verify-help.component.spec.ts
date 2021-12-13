import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailVerifyHelpComponent } from './email-verify-help.component';

describe('EmailVerifyHelpComponent', () => {
  let component: EmailVerifyHelpComponent;
  let fixture: ComponentFixture<EmailVerifyHelpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailVerifyHelpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailVerifyHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
