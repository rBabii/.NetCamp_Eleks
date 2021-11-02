import {GetSingleBlogPagePostItem} from './Childs/GetSingleBlogPagePostItem';

export interface GetSingleBlogPageResponse {
  posts: GetSingleBlogPagePostItem[];
  dateCreated: Date;
  title: string;
  subTitle: string;
  authorFirstName: string;
  authorLastName: string;
}
