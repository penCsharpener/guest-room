import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../../shared/modules/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { EmailVerifyComponent } from './email/verify/email-verify.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, EmailVerifyComponent],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ]
})
export class AccountModule { }
