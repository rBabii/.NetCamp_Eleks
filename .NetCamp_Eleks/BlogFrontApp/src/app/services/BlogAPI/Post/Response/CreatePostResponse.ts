export interface CreatePostResponse {
  postId: number;
  blogId: number;
  dateCreated: Date;
  datePosted: Date;
  visible: boolean;
  title: string;
  subTitle: string;
  headerContent: string;
  mainContent: string;
  footerContent: string;
}
