import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';
import { SettingsService } from '../../settings/settings.service';

@Component({
  selector: 'app-map-link',
  templateUrl: './map-link.component.html',
  styleUrls: ['./map-link.component.scss']
})
export class MapLinkComponent implements OnInit {

  mapLink = '';

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.settingsService.getHome().pipe(
      tap(result => this.mapLink = result.mapsLink)
    ).subscribe();
  }

}
