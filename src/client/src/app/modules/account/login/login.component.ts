import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string = '';
  isLoginFailure = false;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/';
  }

  login(): void {
    this.isLoginFailure = false;
    this.authService.login(this.loginForm.value).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);

    }, error => {
      console.log(error);
      this.isLoginFailure = true;
    });
  }

  forgotPassword() {
    this.router.navigate(['/account/password/forgot']);
  }
}
