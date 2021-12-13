import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Error} from '../../../Common/Models/Error';
import {SetupBlogRequest} from './Request/SetupBlogRequest';
import {SetupBlogResponse} from './Response/SetupBlogResponse';
import {GetBlogListResponse} from './Response/GetBlogListResponse';
import {GetSingleBlogPageResponse} from './Response/GetSingleBlogPageResponse';

@Injectable({
  providedIn: 'root'
})
export class BlogBlogService {

  constructor(private http: HttpClient) { }
  public async SetupBlog(setupBlogRequest: SetupBlogRequest): Promise<SetupBlogResponse | Error> {
    const  url = `http://localhost:5001/api/blog/SetupBlog`;
    const result = await this.http.post<SetupBlogResponse>(url, setupBlogRequest).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
  public async GetBlogList(): Promise<GetBlogListResponse | Error> {
    const  url = `http://localhost:5001/api/blog/GetBlogList`;
    const result = await this.http.get<GetBlogListResponse>(url).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
  public async GetSingleBlogPage(blogUrl: string): Promise<GetSingleBlogPageResponse | Error> {
    const  url = `http://localhost:5001/api/blog/GetSingleBlogPage/${blogUrl}`;
    const result = await this.http.get<GetSingleBlogPageResponse>(url).toPromise().catch((errorResponse: HttpErrorResponse) => {
      return errorResponse.error as Error;
    });
    return result;
  }
}
