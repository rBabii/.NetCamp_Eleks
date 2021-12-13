import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Error} from '../../Common/Models/Error';
import {CreatePostRequest} from './Request/CreatePostRequest';
import {CreatePostResponse} from './Response/CreatePostResponse';
import {GetPostListRequest} from './Request/GetPostListRequest';
import {GetPostListResponse} from './Response/GetPostListResponse';
import {GetSinglePostRequest} from './Request/GetSinglePostRequest';
import {GetSingleBlogPageResponse} from '../Blog/Models/Response/GetSingleBlogPageResponse';
import {GetSinglePostResponse} from './Response/GetSinglePostResponse';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  public async CreatePost(createPostRequest: CreatePostRequest): Promise<CreatePostResponse | Error> {
    const  url = `http://localhost:5001/api/post/CreatePost`;
    const result = await this.http.post<CreatePostResponse>(url, createPostRequest).toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }

  public async GetPostList(getPostListRequest: GetPostListRequest): Promise<GetPostListResponse | Error> {
    const  url = `http://localhost:5001/api/post/GetPostList`;
    const result = await this.http.post<GetPostListResponse>(url, getPostListRequest).toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
        return errorResponse.error as Error;
      });
    return result;
  }

  public async GetSinglePost(getSinglePostRequest: GetSinglePostRequest): Promise<GetSinglePostResponse | Error> {
    const  url = `http://localhost:5001/api/post/GetSinglePost`;
    const result = await this.http.post<GetSinglePostResponse>(url, getSinglePostRequest).toPromise()
      .catch((errorResponse: HttpErrorResponse) => {
        return errorResponse.error as Error;
      });
    return result;
  }

}
