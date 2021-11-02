import {Component, Input, OnInit} from '@angular/core';
import {GetBlogListItem} from '../../../../services/BlogAPI/Blog/Models/Response/Childs/GetBlogListItem';

@Component({
  selector: 'app-blog-list-item',
  templateUrl: './blog-list-item.component.html',
  styleUrls: ['./blog-list-item.component.css']
})
export class BlogListItemComponent implements OnInit {
  @Input('blogItem') BlogItem: GetBlogListItem;

  constructor() { }

  ngOnInit(): void {
  }

}
