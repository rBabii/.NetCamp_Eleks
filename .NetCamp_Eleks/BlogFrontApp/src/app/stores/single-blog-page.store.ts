import {Injectable} from '@angular/core';
import {action, observable} from 'mobx-angular';
import {Error} from '../services/Common/Models/Error';
import {BlogAuthService} from '../services/BlogAPI/Auth/blog.auth.service';
import {makeObservable} from 'mobx';
import {RegisterRequest} from '../services/BlogAPI/Auth/Models/Request/RegisterRequest';
import {BlogBlogService} from '../services/BlogAPI/Blog/Models/blog.blog.service';
import {GetBlogListItem} from '../services/BlogAPI/Blog/Models/Response/Childs/GetBlogListItem';
import {GetSingleBlogPageResponse} from '../services/BlogAPI/Blog/Models/Response/GetSingleBlogPageResponse';

@Injectable({
  providedIn: 'root'
})
class SingleBlogPageStore {

  @observable
  BlogPage: GetSingleBlogPageResponse;

  @observable
  Error: Error;

  constructor(private blogBlogService: BlogBlogService) {
    makeObservable(this);
  }

  async Init(blogUrl: string): Promise<void>{
    const res = await this.blogBlogService.GetSingleBlogPage(blogUrl);
    if (res && 'posts' in res){
      this.BlogPage = res;
    } else if ('errorMessages' in res) {
      this.Error = res;
    }
  }
}

export default SingleBlogPageStore;
