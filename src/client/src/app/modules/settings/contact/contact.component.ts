import { Component, OnInit } from '@angular/core';
import { ContactModel } from '../settings.models';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  model: ContactModel = <ContactModel>{};

  constructor(private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.settingsService.getContact().subscribe(result => this.model = result);
  }

}
