import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HomeModel } from '../settings.models';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  model: HomeModel = <HomeModel>{};
  editForm: FormGroup = <FormGroup>{};

  constructor(private fb: FormBuilder, private settingsService: SettingsService) { 
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      contentHtml: ['', Validators.required],
      pageTitle: ['', Validators.required],
      welcomeParagraph: ['', Validators.required],
      mapsLink: ['', Validators.required],
      roomOneLink: ['', Validators.required],
      roomTwoLink: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.settingsService.getHome().subscribe(result => {
      this.editForm.patchValue(result);
    });
  }

  saveModel(): void {
    const home = this.createModel();
    this.settingsService.updateHome(home).subscribe();
  }

  private createModel(): HomeModel {
    const formValue = this.editForm.getRawValue();
    return {
      title: formValue.title,
      contentHtml: formValue.contentHtml,
      pageTitle: formValue.phoneNumber,
      welcomeParagraph: formValue.faxNumber,
      mapsLink: formValue.emailAddress,
      roomOneLink: formValue.fullName,
      roomTwoLink: formValue.streetAndHouseNumber
    } as HomeModel;
  }
}
