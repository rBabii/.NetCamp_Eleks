import {GetBlogListItem} from './Childs/GetBlogListItem';
import {Error} from '../../../../Common/Models/Error';

export interface GetBlogListResponse {
  blogs: GetBlogListItem[];
  error: Error;
}
