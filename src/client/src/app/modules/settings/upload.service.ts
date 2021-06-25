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

  constructor(private http: HttpClient, private authService: AuthService) { }

  uploadImage(data: FormData): Observable<any> {
    const headers = new HttpHeaders().set('Content-Disposition', 'undefined');

    return this.http.post(`${environment.apiUrl}/upload`, data, {
      headers: headers,
      observe: 'events',
      reportProgress: true
    });
  }

  getImages(): Observable<ImageModel[]> {
    return this.http.get<ImageModel[]>(`${this.baseUrl}/images`);
  }
}
