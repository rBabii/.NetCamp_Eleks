import {Component, Input, OnInit} from '@angular/core';
import {FeaturedPost} from '../../../../../Models/FeaturedPost';

@Component({
  selector: 'app-featured-post',
  templateUrl: './featured-post.component.html',
  styleUrls: ['./featured-post.component.css']
})
export class FeaturedPostComponent implements OnInit {
  @Input('data') Data: FeaturedPost;
  constructor() { }

  ngOnInit(): void {
  }

}
