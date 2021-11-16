import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { VisitorService } from '../../shared/visitor.service';
import { ContactModel } from '../settings/settings.models';
import { SettingsService } from '../settings/settings.service';
import { SendMessageModel } from '../../shared/contact.models';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  contact$ = of<ContactModel>()
  contactForm = <FormGroup>{};
  titles = [] as KeyValuePair[];

  constructor(private fb: FormBuilder, private settingsService: SettingsService, private visitorService: VisitorService) {
    this.contactForm = this.fb.group({
      title: ['', Validators.required],
      name: ['', Validators.required],
      emailAddress: ['', Validators.required],
      address: [''],
      subject: ['', Validators.required],
      message: ['', Validators.required]
    });

    this.titles = [{ key: 'contact.pleaseChoose', value: '' }, { key: 'contact.mr', value: 'contact.mr' }, { key: 'contact.mrs', value: 'contact.mrs' }];
  }

  ngOnInit(): void {
    this.contact$ = this.settingsService.getContact();
  }

  saveModel() {
    var model = {
      title: this.contactForm.value.title,
      name: this.contactForm.value.name,
      email: this.contactForm.value.emailAddress,
      address: this.contactForm.value.address,
      subject: this.contactForm.value.subject,
      messageBody: this.contactForm.value.message
    } as SendMessageModel;

    this.visitorService.sendMessage(model).pipe(finalize(() => { this.contactForm.reset(); this.contactForm.updateValueAndValidity()})).subscribe();
  }
}

export class KeyValuePair {
  key = '';
  value = '';
}