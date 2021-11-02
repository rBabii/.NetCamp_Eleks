import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import SingleBlogPageStore from '../../../stores/single-blog-page.store';

@Component({
  selector: 'app-single-blog-page',
  templateUrl: './single-blog-page.component.html',
  styleUrls: ['./single-blog-page.component.css']
})
export class SingleBlogPageComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, public singleBlogPageStore: SingleBlogPageStore) {

  }

   async ngOnInit(): Promise<void> {
    this.activatedRoute.paramMap.subscribe((params) => {
      this.singleBlogPageStore.Init(params.get('blogUrl'));
    });
  }

}
