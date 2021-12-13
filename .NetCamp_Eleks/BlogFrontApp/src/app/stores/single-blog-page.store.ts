import {Injectable} from '@angular/core';
import {action, observable} from 'mobx-angular';
import {Error} from '../services/Common/Models/Error';
import {BlogAuthService} from '../services/BlogAPI/Auth/blog.auth.service';
import {makeObservable} from 'mobx';
import {RegisterRequest} from '../services/BlogAPI/Auth/Models/Request/RegisterRequest';
import {BlogBlogService} from '../services/BlogAPI/Blog/Models/blog.blog.service';
import {GetBlogListItem} from '../services/BlogAPI/Blog/Models/Response/Childs/GetBlogListItem';
import {GetSingleBlogPageResponse} from '../services/BlogAPI/Blog/Models/Response/GetSingleBlogPageResponse';
import {GetPostListResponse} from '../services/BlogAPI/Post/Response/GetPostListResponse';
import {BlogPostService} from '../services/BlogAPI/Post/blog.post.service';

@Injectable({
  providedIn: 'root'
})
class SingleBlogPageStore {

  @observable
  BlogPage: GetSingleBlogPageResponse;

  @observable
  Error: Error;

  @observable
  Posts: GetPostListResponse;

  constructor(private blogBlogService: BlogBlogService, private postService: BlogPostService) {
    makeObservable(this);
  }

  async Init(blogUrl: string): Promise<void>{
    const res = await this.blogBlogService.GetSingleBlogPage(blogUrl);
    if (res && 'dateCreated' in res){
      this.BlogPage = res;
    } else if ('errorMessages' in res) {
      this.Error = res;
    }

    const postsRes = await this.postService.GetPostList({
      blogUrl
    });
    if ('errorMessages' in postsRes) {
      if (this.Error) {
        this.Error.errorMessages = [...this.Error.errorMessages, ...postsRes.errorMessages];
      }else {
        this.Error = postsRes;
      }
    } else if ('posts' in postsRes) {
      this.Posts = postsRes;
    }

  }
}

export default SingleBlogPageStore;
