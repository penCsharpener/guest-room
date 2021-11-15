import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { ContactModel, LegalModel } from '../../settings/settings.models';
import { SettingsService } from '../../settings/settings.service';

@Component({
  selector: 'app-legal',
  templateUrl: './legal.component.html',
  styleUrls: ['./legal.component.scss']
})
export class LegalComponent implements OnInit {
  legal$ = of<LegalModel>();
  contact$ = of<ContactModel>();

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.legal$ = this.settingsService.getLegal();
    this.contact$ = this.settingsService.getContact();
  }

}
