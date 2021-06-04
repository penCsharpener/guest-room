import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUser } from 'src/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSubject = new BehaviorSubject<IUser>(<IUser><unknown>null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  getCurrentUserValue() {
    return this.currentUserSubject.value;
  }

  loadCurrentUser(token: string): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IUser>(this.baseUrl + '/account', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSubject.next(user);
        }
      })
    )
  }

  register(values: any) {
    return this.http.post(this.baseUrl + '/account/register', values);
  }

  login(values: any) {
    return this.http.post<IUser>(this.baseUrl + '/account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSubject.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSubject.next(<IUser><unknown>null);
    this.router.navigateByUrl('/');
  }

  verifyEmail(email: string, code: string) {
    return this.http.post(this.baseUrl + `/account/email/verify`, { email: email, code: code });
  }

  forgotPassword(email: string) {
    return this.http.post(this.baseUrl + `/account/password/forgot`, { emailAddress: email });
  }

  resetPassword(email: string, token: string, password: string, passwordConfirm: string) {
    return this.http.post(this.baseUrl + `/account/password/reset`, { email: email, token: token, password: password, confirmPassword: passwordConfirm });
  }
}
