import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetupBlogComponent } from './setup-blog.component';

describe('SetupBlogComponent', () => {
  let component: SetupBlogComponent;
  let fixture: ComponentFixture<SetupBlogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetupBlogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetupBlogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
