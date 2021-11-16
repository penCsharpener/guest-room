import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { MainComponent } from './main/main.component';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../shared/modules/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from '../shared/shared.module';
import { MapLinkComponent } from './map-link/map-link.component';
import { CounterComponent } from './counter/counter.component';

@NgModule({
  declarations: [HeaderComponent, FooterComponent, MainComponent, MapLinkComponent, CounterComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MaterialModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    MainComponent
  ]
})
export class LayoutModule { }
