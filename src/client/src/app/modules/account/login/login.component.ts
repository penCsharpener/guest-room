import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isLoginFailure = false;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  login(): void {
    this.isLoginFailure = false;
    this.authService.login(this.loginForm.value).subscribe(() => {
      console.log('user logged in');
      this.router.navigate(['/']);

    }, error => {
      console.log(error);
      this.isLoginFailure = true;
    });
  }

  forgotPassword() {
    this.router.navigate(['/account/password/forgot']);
  }
}
