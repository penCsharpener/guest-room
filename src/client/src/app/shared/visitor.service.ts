import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { SendMessageModel } from './contact.models';

@Injectable({
  providedIn: 'root'
})
export class VisitorService {
  baseUrl = `${environment.apiUrl}/visitor/`;

  constructor(private http: HttpClient) { }

  sendMessage(sendMessageModel: SendMessageModel) : Observable<any> {
    return this.http.post<any>(this.baseUrl, sendMessageModel);
  }
  
  countVisitor(): Observable<number> {
    var id = localStorage.getItem('sessionId');

    if (!id) {
      id = this.uuid4();
      localStorage.setItem('sessionId', id);
    }

    return this.http.post<number>(`${this.baseUrl}counter/${id}`, null);
  }

  // https://gist.github.com/erikvullings/b71a0be49e5e79945805bd209e22c7d2
  uuid4 = () => {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
      // tslint:disable-next-line:no-bitwise
      const r = (Math.random() * 16) | 0;
      // tslint:disable-next-line:no-bitwise
      const v = c === 'x' ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  };
}
