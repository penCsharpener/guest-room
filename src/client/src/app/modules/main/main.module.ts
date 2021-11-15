import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { LegalComponent } from './legal/legal.component';
import { RoomComponent } from './room/room.component';
import { PricingComponent } from './room/pricing/pricing.component';
import { MapLinkComponent } from './map-link/map-link.component';
import { MaterialModule } from '../../shared/modules/material.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ContactComponent,
    HomeComponent,
    LegalComponent,
    RoomComponent,
    PricingComponent,
    MapLinkComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class MainModule { }
