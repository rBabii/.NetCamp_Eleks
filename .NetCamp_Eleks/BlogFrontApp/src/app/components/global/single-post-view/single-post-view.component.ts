import {Component, Input, OnInit} from '@angular/core';
import {SinglePostViewModel} from './Models/SinglePostViewModel';

@Component({
  selector: 'app-single-post-view',
  templateUrl: './single-post-view.component.html',
  styleUrls: ['./single-post-view.component.css']
})
export class SinglePostViewComponent implements OnInit {
  @Input('postData') postData: SinglePostViewModel;

  constructor() { }

  ngOnInit(): void {
  }

}
