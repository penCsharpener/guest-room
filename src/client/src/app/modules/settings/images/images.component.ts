import { HttpClient, HttpEventType } from '@angular/common/http';
import { HtmlAstPath } from '@angular/compiler';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { ImageModel } from '../settings.models';
import { UploadService } from '../upload.service';

@Component({
  selector: 'app-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.scss']
})
export class ImagesComponent implements OnInit {
  model: ImageModel = {} as ImageModel;
  editForm: FormGroup = {} as FormGroup;

  fileName = '';
  uploadProgress?: number | null;
  uploadSub?: Subscription | null;

  constructor(private http: HttpClient, private fb: FormBuilder, private uploadService: UploadService) {
    this.editForm = this.fb.group({
      path: ['', Validators.required],
      description: ['', Validators.required],
      name: ['', Validators.required],
      location: ['', Validators.required],
    });
  }

  ngOnInit(): void {

  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log("🚀 ~ file: images.component.ts ~ line 41 ~ ImagesComponent ~ onFileSelected ~ event", event)
  
    if (file) {
        this.fileName = file.name;
        const formData = new FormData();
        formData.append("imageName", file.name);
        formData.append("file", file);
        formData.append("description", 'image description text');
        formData.append("location", 'room1 location');


        const upload$ = this.http.post(environment.apiUrl + "/settings/upload", formData, {
            reportProgress: true,
            observe: 'events'
        })
        .pipe(
            finalize(() => this.reset())
        );
      
        this.uploadSub = upload$.subscribe(event => {
          if (event.type == HttpEventType.UploadProgress && event.total) {
            this.uploadProgress = Math.round(100 * (event.loaded / event.total));
          }
        })
    }
}

  cancelUpload() {
    this.uploadSub?.unsubscribe();
    this.reset();
  }

  reset() {
    this.uploadProgress = null;
    this.uploadSub = null;
  }
}
