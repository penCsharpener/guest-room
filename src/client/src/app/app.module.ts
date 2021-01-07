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

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RoomComponent,
    LegalComponent,
    ContactComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    LayoutModule,
    AppRoutingModule,
    SettingsModule,
    LoginModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
