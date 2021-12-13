export interface CreatePostRequest {
  datePosted: Date;
  visible: boolean;
  title: string;
  subTitle: string;
  headerContent: string;
  mainContent: string;
  footerContent: string;
  previewtext: string;
  postImageName: string;
}
