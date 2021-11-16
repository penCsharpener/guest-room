import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { HomeModel } from '../../modules/settings/settings.models';
import { SettingsService } from '../../modules/settings/settings.service';

@Component({
  selector: 'app-map-link',
  templateUrl: './map-link.component.html',
  styleUrls: ['./map-link.component.scss']
})
export class MapLinkComponent implements OnInit {
  home$ = of<HomeModel>();

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.home$ = this.settingsService.getHome();
  }
}
