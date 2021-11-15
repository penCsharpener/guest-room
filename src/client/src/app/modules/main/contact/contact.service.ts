import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { SendMessageModel } from './contact.models';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  baseUrl = `${environment.apiUrl}/contact/`;

  constructor(private http: HttpClient) { }

  sendMessage(sendMessageModel: SendMessageModel) : Observable<any> {
    return this.http.post<any>(this.baseUrl, sendMessageModel);
  }
}
