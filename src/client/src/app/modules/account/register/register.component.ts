import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmedValidator } from '../../../shared/validation/password-confirm.validator';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { 
    this.registerForm = this.formBuilder.group({
      displayName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required]
    }, ConfirmedValidator('password', 'passwordConfirm'));
  }

  ngOnInit(): void {
  }

  register(): void {
    this.authService.register(this.registerForm.value).subscribe(() => {
      console.log('user registered');
    }, error => {
      console.log(error);
    });
  }
}
