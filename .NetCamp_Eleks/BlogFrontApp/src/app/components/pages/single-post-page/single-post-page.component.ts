import { Component, OnInit } from '@angular/core';
import {SinglePostViewModel} from '../../global/single-post-view/Models/SinglePostViewModel';
import {BlogPostService} from '../../../services/BlogAPI/Post/blog.post.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-single-post-page',
  templateUrl: './single-post-page.component.html',
  styleUrls: ['./single-post-page.component.css']
})
export class SinglePostPageComponent implements OnInit {
  PostData: SinglePostViewModel;

  constructor(private blogPostService: BlogPostService, private activatedRoute: ActivatedRoute) { }

  async ngOnInit(): Promise<void> {
    this.activatedRoute.paramMap.subscribe( async (params) => {
      const res = await this.blogPostService.GetSinglePost({
        // tslint:disable-next-line:radix
        postId: Number.parseInt(params.get('postId'))
      });
      if ('datePosted' in res) {
        this.PostData = {
          authorImage: res.authorImage ? `http://localhost:5003/files/${res.authorImage}` : 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
          authorFirstName: res.authorFirstName,
          authorLastName: res.authorLastName,
          datePosted: res.datePosted,
          title: res.title,
          subTitle: res.subTitle,
          postMainImage: res.postMainImage ? `http://localhost:5003/files/${res.postMainImage}` : '../../../../assets/img/demopic/10.jpg',
          blogUrl: res.blogUrl,
          headerContent: res.postHeader,
          mainContent: res.postMainContent,
          footerContent: res.postFooter
        };
      }
    });
  }
}
