import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ConfirmedValidator } from '../../../../shared/validation/password-confirm.validator';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  isReset = false;
  resetSuccessful = false;
  private token = '';
  private email = '';

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private authService: AuthService) {
    this.resetPasswordForm = this.formBuilder.group({
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required]
    }, ConfirmedValidator('password', 'passwordConfirm'));
   }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.token = params.token;
      this.email = params.email;
    })
  }

  resetPassword() {
    this.isReset = true;

    this.authService.resetPassword(this.email, this.token, this.resetPasswordForm.value.password, this.resetPasswordForm.value.passwordConfirm).subscribe(() => {
      this.resetSuccessful = true;
    }, err => {
      this.resetSuccessful = false;
      console.log(err);
    });
  }

}
