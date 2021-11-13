import { HttpClient, HttpEventType, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of, Subscription } from 'rxjs';
import { finalize } from 'rxjs/operators';
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
  images$: Observable<ImageModel[]>;

  fileName = '';
  uploadProgress?: number | null;
  uploadSub?: Subscription | null;

  constructor(private http: HttpClient, private fb: FormBuilder, private uploadService: UploadService) {
    this.editForm = this.fb.group({
      id: ['', Validators.required],
      path: ['', Validators.required],
      description: ['', Validators.required],
      name: ['', Validators.required],
      location: ['', Validators.required],
    });

    this.images$ = of();
  }

  ngOnInit(): void {
    this.images$ = this.uploadService.getImages();
  }

  onFileSelected(event: HTMLInputElement) {
    console.log("🚀 ~ file: images.component.ts ~ line 38 ~ ImagesComponent ~ onFileSelected ~ file", event.files![0]);
    
    // this.processFile(event.files![0]);

    const file = event.files![0];
    console.log("🚀 ~ file: images.component.ts ~ line 43 ~ ImagesComponent ~ onFileSelected ~ file", file)
    const formData = new FormData();
    formData.append("id", "1");
    formData.append("imageName", file.name);
    formData.append("file", file, file.name);
    formData.append("description", 'image description text');
    formData.append("location", 'room1 location');
    
    console.log("🚀 ~ file: images.component.ts ~ line 51 ~ ImagesComponent ~ onFileSelected ~ formData", formData)
    this.uploadService.uploadImage(formData).subscribe();
  }

  // private processFile(file: File) {
  //   const reader = new FileReader();

  //   reader.onload = (e: any) => {
  //     console.log(e.target.result);
  //     // this.previews.push(e.target.result);
  //   };

  //   reader.readAsDataURL(file);

  //   const formData: FormData = new FormData();

  //   formData.append('file', file);

  //        const headers = new HttpHeaders().set('Content-Disposition', 'undefined').set('Content-Type', 'multipart/form-data;').set('Accept', '*/*;');


  //   const req = new HttpRequest('POST', `${environment.apiUrl}/settings/upload`, formData, {
  //     reportProgress: true,
  //     responseType: 'json',
  //     headers: headers
  //   });

  //   this.http.request(req).subscribe();
  // }

  // onFileSelected(event: any) {
  //   const file: File = event.files[0];
  //   console.log("🚀 ~ file: images.component.ts ~ line 41 ~ ImagesComponent ~ onFileSelected ~ event", event)
  
  //   if (file) {
  //       this.fileName = file.name;
  //       const formData = new FormData();
  //       formData.append("id", "1");
  //       formData.append("imageName", file.name);
  //       formData.append("file", file, file.name);
  //       formData.append("description", 'image description text');
  //       formData.append("location", 'room1 location');
      
  //       console.log("🚀 ~ file: images.component.ts ~ line 52 ~ ImagesComponent ~ onFileSelected ~ formData", formData)
  //       const headers = new HttpHeaders().set('Content-Disposition', 'undefined');

  //       const upload$ = this.http.post(environment.apiUrl + "/settings/upload", formData, {
  //           reportProgress: true,
  //           observe: 'events',
  //           headers: headers
  //       })
  //       .pipe(
  //           finalize(() => this.reset())
  //       );
      
  //       this.uploadSub = upload$.subscribe(event => {
  //         if (event.type == HttpEventType.UploadProgress && event.total) {
  //           this.uploadProgress = Math.round(100 * (event.loaded / event.total));
  //         }
  //       })
  //   }
  // }

  cancelUpload() {
    this.uploadSub?.unsubscribe();
    this.reset();
  }

  reset() {
    this.uploadProgress = null;
    this.uploadSub = null;
  }
}
