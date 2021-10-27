import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BlogAuthService} from '../../../services/BlogAPI/Auth/blog.auth.service';
import {Error} from '../../../services/Common/Models/Error';
import {ErrorType} from '../../../services/Common/Enums/ErrorType';
import LoginStore from '../../../stores/login.store';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.css']
})
export class VerifyComponent implements OnInit {
  Result: void | Error = {
    errorMessages: [],
    errorType: ErrorType.None
  };
  constructor(private activatedRoute: ActivatedRoute, private blogAuthService: BlogAuthService, public loginStore: LoginStore) {

  }

  async ngOnInit(): Promise<void> {
    this.activatedRoute.queryParams.subscribe(async params => {
      const token = params.token;
      this.Result = await this.blogAuthService.Verify({token});
    });
  }

}
