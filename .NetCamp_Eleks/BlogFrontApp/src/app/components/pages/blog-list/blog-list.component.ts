import { Component, OnInit } from '@angular/core';
import BlogListStore from '../../../stores/blog-list.store';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {

  constructor(public blogListStore: BlogListStore) { }

   async ngOnInit(): Promise<void> {
    await this.blogListStore.Init();
  }

}
