import { Component, OnInit } from '@angular/core';
import UserStore from '../../../stores/user.store';
import {BlogAuthService} from '../../../services/BlogAPI/Auth/blog.auth.service';
import {Error} from '../../../services/Common/Models/Error';

@Component({
  selector: 'app-email-verify-help',
  templateUrl: './email-verify-help.component.html',
  styleUrls: ['./email-verify-help.component.css']
})
export class EmailVerifyHelpComponent implements OnInit {
  Result: void | Error;
  constructor(public userStore: UserStore, public blogAuthService: BlogAuthService) { }

  async ngOnInit(): Promise<void> {
    await this.userStore.Init();
  }

  async SendEmail(): Promise<void> {
    this.Result = await this.blogAuthService.SendEmailVerificationLink();
  }
}
