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
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
  }

  submit() {
    this.authService.forgotPassword(this.forgotPasswordForm.value.email).subscribe(() => {
    }, error => {
      console.log(error);
    });
    this.hasSubmitted = true;
  }

}
