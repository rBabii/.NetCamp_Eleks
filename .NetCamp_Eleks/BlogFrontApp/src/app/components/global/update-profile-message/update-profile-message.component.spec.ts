import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateProfileMessageComponent } from './update-profile-message.component';

describe('UpdateProfileMessageComponent', () => {
  let component: UpdateProfileMessageComponent;
  let fixture: ComponentFixture<UpdateProfileMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateProfileMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateProfileMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
