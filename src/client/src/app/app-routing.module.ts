import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './modules/account/login/login.component';
import { ContactComponent } from './modules/contact/contact.component';
import { LegalComponent } from './modules/main/legal/legal.component';
import { RoomComponent } from './modules/main/room/room.component';
import { HomeComponent } from './modules/main/home/home.component';
import { RegisterComponent } from './modules/account/register/register.component';
import { EmailVerifyComponent } from './modules/account/email/verify/email-verify.component';
import { ResetPasswordComponent } from './modules/account/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './modules/account/forgot-password/forgot-password.component';

const routes: Routes = [
  { path: 'main/contact', component: ContactComponent },
  { path: 'main/legal', component: LegalComponent },
  { path: 'account/login', component: LoginComponent },
  { path: 'account/logout', component: LoginComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'account/email/verify', component: EmailVerifyComponent },
  { path: 'account/password/reset', component: ResetPasswordComponent },
  { path: 'account/password/forgot', component: ForgotPasswordComponent },
  { path: 'main/room/:id', component: RoomComponent },
  { path: '', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
