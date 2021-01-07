import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { LegalComponent } from './legal/legal.component';
import { RoomComponent } from './room/room.component';

const routes: Routes = [
  { path: 'settings/contact', component: ContactComponent },
  { path: 'settings/home', component: HomeComponent },
  { path: 'settings/legal', component: LegalComponent },
  { path: 'settings/room/:id', component: RoomComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
