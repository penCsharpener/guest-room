import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact/contact.component';
import { LegalComponent } from './legal/legal.component';
import { HomeComponent } from './home/home.component';
import { MaterialModule } from '../../../shared/modules/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RoomComponent } from './room/room.component';
import { SettingsRoutingModule } from './settings-routing.module';
import { ImagesComponent } from './images/images.component';



@NgModule({
  declarations: [ContactComponent, LegalComponent, HomeComponent, RoomComponent, ImagesComponent],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
