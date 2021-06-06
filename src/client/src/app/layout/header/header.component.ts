import { Component, OnInit } from '@angular/core';
import { HomeModel } from '../../modules/settings/settings.models';
import { SettingsService } from '../../modules/settings/settings.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  model = <HomeModel>{};

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.settingsService.getHome().subscribe(result => this.model = result);
  }

}
