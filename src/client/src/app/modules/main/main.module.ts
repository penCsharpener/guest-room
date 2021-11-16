import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../shared/modules/material.module';
import { SharedModule } from '../../shared/shared.module';
import { ContactComponent } from '../contact/contact.component';
import { HomeComponent } from './home/home.component';
import { LegalComponent } from './legal/legal.component';
import { PricingComponent } from './room/pricing/pricing.component';
import { RoomComponent } from './room/room.component';

@NgModule({
  declarations: [
    ContactComponent,
    HomeComponent,
    LegalComponent,
    RoomComponent,
    PricingComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class MainModule { }
