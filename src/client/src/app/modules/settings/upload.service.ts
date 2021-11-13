import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';
import { ImageModel } from './settings.models';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = `${environment.apiUrl}/`;
  assetUrl = 'assets/images/'

  constructor(private http: HttpClient) { }

  uploadImage(data: FormData): Observable<any> {
    const headers = new HttpHeaders().append('Content-Disposition', 'undefined').append('Accept', 'application/json');

    return this.http.post(`${this.baseUrl}settings/upload`, data, {
      headers: headers,
      observe: 'events',
      reportProgress: true
    });
  }

  getImages(): Observable<ImageModel[]> {
    return this.http.get<ImageModel[]>(`${this.baseUrl}settings/images`);
  }
}
