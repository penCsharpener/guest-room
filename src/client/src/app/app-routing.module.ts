import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './modules/account/login/login.component';
import { RoomComponent } from './modules/main/room/room.component';
import { TestComponent } from './modules/main/test/test.component';

const routes: Routes = [
  { path: 'account/login', component: LoginComponent },
  { path: 'main/room/:id', component: RoomComponent },
  { path: '', component: TestComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
