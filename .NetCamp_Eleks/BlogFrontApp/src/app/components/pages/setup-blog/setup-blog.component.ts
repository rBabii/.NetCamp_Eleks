import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {UpdateProfileMessageComponent} from '../../global/update-profile-message/update-profile-message.component';
import NotificationStore from '../../../stores/notification.store';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {CustomValidators} from '../../../CustomValidators/CustomValidators';
import {BlogBlogService} from '../../../services/BlogAPI/Blog/Models/blog.blog.service';
import {SetupBlogRequest} from '../../../services/BlogAPI/Blog/Models/Request/SetupBlogRequest';
import UserStore from '../../../stores/user.store';

@Component({
  selector: 'app-setup-blog',
  templateUrl: './setup-blog.component.html',
  styleUrls: ['./setup-blog.component.css']
})
export class SetupBlogComponent implements OnInit {

  BlogUrl = new FormControl('', [Validators.required, CustomValidators.BlogUrlRegex]);
  Title = new FormControl('', [Validators.required]);
  SubTitle = new FormControl('', [Validators.required]);
  Visible = new FormControl('', [Validators.required]);

  SetupBlogForm = new FormGroup({
    blogUrl: this.BlogUrl,
    title: this.Title,
    subTitle: this.SubTitle,
    visible: this.Visible
  });

  constructor(public notificationStore: NotificationStore,
              public dialog: MatDialog,
              public router: Router,
              public blogBlogService: BlogBlogService,
              public userStore: UserStore) { }

  async OnSubmit(): Promise<void> {
    if (this.SetupBlogForm.valid) {
      const res = await this.blogBlogService.SetupBlog(this.SetupBlogForm.value as SetupBlogRequest);
      if (res && 'errorMessages' in res) {
        this.SetupBlogForm.setErrors({
          responseErrorMessages: res.errorMessages
        });
      }else if (res && 'blogUrl' in res) {
        await this.userStore.Init();
        await this.router.navigate([`/blog/${this.userStore.User.blogUrl}`]);
      }
    }
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(UpdateProfileMessageComponent, {
      width: '90vw',
      height: '90vh'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!this.notificationStore.IsUserSetuped){
        this.router.navigate(['/']);
      }
    });
  }


  async ngOnInit(): Promise<void> {
    await this.notificationStore.Init();
    if (!this.notificationStore.IsUserSetuped){
      this.openDialog();
    }
  }

}
