import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  hasSubmitted = false;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) { 
    this.forgotPasswordForm = this.formBuilder.group({
      email: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  submit() {
    console.log("🚀 ~ file: forgot-password.component.ts ~ line 25 ~ ForgotPasswordComponent ~ this.authService.forgotPassword ~ this.forgotPasswordForm.value.email", this.forgotPasswordForm.value.email)
    this.authService.forgotPassword(this.forgotPasswordForm.value.email).subscribe(() => {
    }, error => {
      console.log(error);
    });
    this.hasSubmitted = true;
  }

}
