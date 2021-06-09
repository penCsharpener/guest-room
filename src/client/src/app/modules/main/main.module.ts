import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { LegalComponent } from './legal/legal.component';
import { RoomComponent } from './room/room.component';

@NgModule({
  declarations: [
    ContactComponent,
    HomeComponent,
    LegalComponent,
    RoomComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class MainModule { }
