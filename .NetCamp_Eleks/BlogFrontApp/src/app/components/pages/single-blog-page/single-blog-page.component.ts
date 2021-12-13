import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import SingleBlogPageStore from '../../../stores/single-blog-page.store';
import {StoryPost} from '../../../../Models/StoryPost';

@Component({
  selector: 'app-single-blog-page',
  templateUrl: './single-blog-page.component.html',
  styleUrls: ['./single-blog-page.component.css']
})
export class SingleBlogPageComponent implements OnInit {
  StoryPostData: StoryPost[];

  constructor(private activatedRoute: ActivatedRoute, public singleBlogPageStore: SingleBlogPageStore) {

  }

   async ngOnInit(): Promise<void> {
    this.activatedRoute.paramMap.subscribe( async (params) => {
      await this.singleBlogPageStore.Init(params.get('blogUrl'));
      this.StoryPostData = this.singleBlogPageStore.Posts.posts.map((p) => {
        return {
          StoryPostThumbnail: p.postMainImage ? `http://localhost:5003/files/${p.postMainImage}` : `assets/img/demopic/2.jpg`,
          Title: p.title,
          Text: p.previewText,
          AuthorThumbnail: `http://localhost:5003/files/${p.authorImage}`,
          AuthorName: p.authorFirstName + ' ' + p.authorLastName,
          PostedDate: p.datePosted.toString(),
          ReadTimeSec: '6 min',
          postId: p.postId
        };
      });
    });
  }

}
