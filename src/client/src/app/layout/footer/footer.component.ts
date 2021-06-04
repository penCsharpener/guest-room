import { Component, ComponentFactoryResolver, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/core/auth.service';
import { IUser } from 'src/shared/models/user';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
  currentUser$: Observable<IUser> = of();

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token) {
      this.authService.loadCurrentUser(token).subscribe(() => {
        console.log('user loaded');
      }, error => {
        console.log(error);
      })
    }
  }

  logout() {
    this.authService.logout();
  }
}
