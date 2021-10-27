import {Component, Input, OnInit} from '@angular/core';
import {StoryPost} from '../../../../../Models/StoryPost';

@Component({
  selector: 'app-story-post',
  templateUrl: './story-post.component.html',
  styleUrls: ['./story-post.component.css']
})
export class StoryPostComponent implements OnInit {
  @Input('data') Data: StoryPost;
  constructor() { }

  ngOnInit(): void {
  }

}
