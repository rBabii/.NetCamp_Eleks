import {Injectable} from '@angular/core';
import {action, observable} from 'mobx-angular';
import {Error} from '../services/Common/Models/Error';
import {BlogAuthService} from '../services/BlogAPI/Auth/blog.auth.service';
import {makeObservable} from 'mobx';
import {RegisterRequest} from '../services/BlogAPI/Auth/Models/Request/RegisterRequest';
import {BlogBlogService} from '../services/BlogAPI/Blog/Models/blog.blog.service';
import {GetBlogListItem} from '../services/BlogAPI/Blog/Models/Response/Childs/GetBlogListItem';

@Injectable({
  providedIn: 'root'
})
class BlogListStore {

  @observable
  Blogs: GetBlogListItem[] = [];

  @observable
  Error: Error;

  constructor(private blogBlogService: BlogBlogService) {
    makeObservable(this);
    this.Init();
  }

  async Init(): Promise<void>{
    const res = await this.blogBlogService.GetBlogList();
    if (res && 'blogs' in res){
      this.Blogs = res.blogs;
      this.Error = res.error;
    } else if ('errorMessages' in res) {
      this.Error = res;
    }
  }
}

export default BlogListStore;
