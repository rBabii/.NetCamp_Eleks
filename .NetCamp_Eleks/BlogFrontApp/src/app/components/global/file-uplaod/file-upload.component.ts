import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {HttpEventType, HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Error} from '../../../services/Common/Models/Error';

@Component({
  selector: 'app-file-uplaod',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  public progress: number;
  public messages: string[];
  @Output() public onUploadFinished = new EventEmitter();
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
  }
  public uploadFile(files): void {
    if (files.length === 0) {
      return;
    }
    const fileToUpload = files[0] as File;
    const formData = new FormData();
    formData.append('image', fileToUpload, fileToUpload.name);
    formData.append('key', '1');
    this.http
      .post('http://localhost:5003/api/Attachment/SaveSingleImage', formData, {reportProgress: true, observe: 'events'}).
    subscribe((event) => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response) {
          this.messages = ['Upload success.'];
          this.onUploadFinished.emit(event.body);
        }
      }, (error: HttpErrorResponse) => {
      const err = error.error as Error;
      this.messages = err.errorMessages;
    });
  }

}
