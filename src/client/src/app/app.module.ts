import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from './layout/layout.module';
import { HomeComponent } from './modules/main/home/home.component';
import { RoomComponent } from './modules/main/room/room.component';
import { LegalComponent } from './modules/main/legal/legal.component';
import { ContactComponent } from './modules/main/contact/contact.component';
import { LoginModule } from './modules/account/login/login.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SettingsModule } from './modules/settings/settings.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from './core/auth/interceptors/authorize.interceptor';
import { RegisterComponent } from './modules/account/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RoomComponent,
    LegalComponent,
    ContactComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    LayoutModule,
    HttpClientModule,
    AppRoutingModule,
    SettingsModule,
    LoginModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
