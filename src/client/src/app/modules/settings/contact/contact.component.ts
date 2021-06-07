import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactModel } from '../settings.models';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  model: ContactModel = <ContactModel>{};
  editForm: FormGroup = <FormGroup>{};

  constructor(private fb: FormBuilder, private settingsService: SettingsService) { 
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      contentHtml: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      faxNumber: ['', Validators.required],
      emailAddress: ['', Validators.required],
      fullName: ['', Validators.required],
      streetAndHouseNumber: ['', Validators.required],
      zipCode: ['', Validators.required],
      city: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.settingsService.getContact().subscribe(result => {
      this.editForm.patchValue(result);
    });
  }

  saveModel(): void {
    const contact = this.createModel();
    this.settingsService.updateContact(contact).subscribe();
  }

  private createModel(): ContactModel {
    const formValue = this.editForm.getRawValue();
    return {
      title: formValue.title,
      contentHtml: formValue.contentHtml,
      phoneNumber: formValue.phoneNumber,
      faxNumber: formValue.faxNumber,
      emailAddress: formValue.emailAddress,
      fullName: formValue.fullName,
      streetAndHouseNumber: formValue.streetAndHouseNumber,
      zipCode: formValue.zipCode,
      city: formValue.city
    } as ContactModel;
  }
}
