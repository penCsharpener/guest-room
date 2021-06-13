import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  uploadImage(data: FormData): Observable<any> {
    const headers = new HttpHeaders().set('Content-Disposition', 'undefined');

    return this.http.post(`${environment.apiUrl}/upload`, data, {
      headers: headers,
      observe: 'events',
      reportProgress: true
    });
  }
}
