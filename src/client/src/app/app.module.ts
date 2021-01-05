import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from './layout/layout.module';
import { TestComponent } from './modules/main/test/test.component';
import { RoomComponent } from './modules/main/room/room.component';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    RoomComponent,
  ],
  imports: [
    BrowserModule,
    LayoutModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
