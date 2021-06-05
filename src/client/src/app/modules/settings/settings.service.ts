import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ContactModel, HomeModel, LegalModel, RoomModel } from './settings.models';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  baseUrl = `${environment.apiUrl}/settings/`;

  constructor(private http: HttpClient) { }

  getContact() : Observable<ContactModel> {
    return this.http.get<ContactModel>(this.baseUrl + 'contact');
  }

  getHome() : Observable<HomeModel> {
    return this.http.get<HomeModel>(this.baseUrl + 'home');
  }

  getLegal() : Observable<LegalModel> {
    return this.http.get<LegalModel>(this.baseUrl + 'legal');
  }

  getRoom(roomId: number) : Observable<RoomModel> {
    return this.http.get<RoomModel>(this.baseUrl + 'room/' + roomId);
  }

  updateContact(model: ContactModel) : Observable<any> {
    return this.http.put(this.baseUrl + 'contact', model);
  }

  updateHome(model: HomeModel) : Observable<any> {
    return this.http.put(this.baseUrl + 'home', model);
  }

  updateLegal(model: LegalModel) : Observable<any> {
    return this.http.put(this.baseUrl + 'legal', model);
  }

  updateRoom(roomId: number, model: RoomModel) : Observable<any> {
    return this.http.put(this.baseUrl + 'room/' + roomId, model);
  }
}
