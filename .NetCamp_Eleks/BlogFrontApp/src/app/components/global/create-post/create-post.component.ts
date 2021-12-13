import { Component, OnInit } from '@angular/core';
import * as ClassicEditor from 'src/app/ckeditor5-build-classic';
import {BlogPostService} from '../../../services/BlogAPI/Post/blog.post.service';
import {CreatePostRequest} from '../../../services/BlogAPI/Post/Request/CreatePostRequest';
import {CreatePostResponse} from '../../../services/BlogAPI/Post/Response/CreatePostResponse';
import {Error} from '../../../services/Common/Models/Error';
import {SaveSingleImageResponse} from '../../../services/Common/Models/SaveSingleImageResponse';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {
  public Editor = ClassicEditor;

  public EditorConfig = {
    toolbar: {
      items: [
        'heading',
        '|',
        'bold',
        'italic',
        'link',
        'bulletedList',
        'numberedList',
        '|',
        'outdent',
        'indent',
        '|',
        'uploadImage',
        'blockQuote',
        'insertTable',
        'mediaEmbed',
        'undo',
        'redo'
      ]
    },
    image: {
      toolbar: [
        'imageStyle:inline',
        'imageStyle:block',
        'imageStyle:side',
        '|',
        'toggleImageCaption',
        'imageTextAlternative'
      ]
    },
    table: {
      contentToolbar: [
        'tableColumn',
        'tableRow',
        'mergeTableCells'
      ]
    },
    simpleUpload: {
      // The URL that the images are uploaded to.
      uploadUrl: 'http://localhost:5003/api/Attachment/SaveSingleImage',

      // Enable the XMLHttpRequest.withCredentials property.
      withCredentials: false,

      // Headers sent along with the XMLHttpRequest to the upload server.
      // headers: {
      //   'X-CSRF-TOKEN': 'CSRF-Token',
      //  Authorization: 'Bearer <JSON Web Token>'
      // }
    },
    // This value must be kept in sync with the language defined in webpack.config.js.
    language: 'en'
  };

  public DatePosted = new Date();

  public HeaderContent = '';

  public MainContent = '';

  public FooterContent = '';

  public Title = '';

  public Subtitle = '';

  public  PreviewText = '';

  public Visible = true;

  public ImageName = 'no-image.jpg';

  constructor(private blogPostService: BlogPostService) { }

  ngOnInit(): void {
  }

  async Post(): Promise<CreatePostResponse | Error> {
    const createPostRequest: CreatePostRequest = {
      datePosted: this.DatePosted,
      visible: this.Visible,
      title: this.Title,
      subTitle: this.Subtitle,
      headerContent: this.HeaderContent,
      mainContent: this.MainContent,
      footerContent: this.FooterContent,
      previewtext: this.PreviewText,
      postImageName: this.ImageName
    };
    return await this.blogPostService.CreatePost(createPostRequest);
  }

  OnFileUploaded($event: any): void {
    const imageResponse = $event as SaveSingleImageResponse;
    this.ImageName = imageResponse.fileName;
  }
}
