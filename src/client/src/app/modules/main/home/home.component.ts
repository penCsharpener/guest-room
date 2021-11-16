import { Component, OnInit } from '@angular/core';
import { HomeModel } from '../../settings/settings.models';
import { SettingsService } from '../../settings/settings.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  homeModel = <HomeModel><unknown>null;

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.settingsService.getHome().subscribe(result => this.homeModel = result);
  }
}
