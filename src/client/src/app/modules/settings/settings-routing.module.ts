import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { LegalComponent } from './legal/legal.component';
import { RoomComponent } from './room/room.component';

const routes: Routes = [
  { path: 'settings/contact', component: ContactComponent, canActivate: [AuthGuard] },
  { path: 'settings/home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'settings/legal', component: LegalComponent, canActivate: [AuthGuard] },
  { path: 'settings/room/:id', component: RoomComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
