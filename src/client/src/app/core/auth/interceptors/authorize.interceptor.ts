import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../../auth.service';

@Injectable()
export class AuthorizeInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService, private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        'Content-Type' : 'application/json; charset=utf-8',
        'Accept'       : 'application/json',
        'Authorization': `Bearer ${this.authService.getToken()}`,
      },
    });

    return next.handle(request).pipe(
      catchError(res => {
        if (res.status === 403) {
          this.router.navigate(['/account/login']);
          return of(<HttpEvent<any>>{});
        } else {
          return throwError(res);
        }
      })
    );
  }
}
