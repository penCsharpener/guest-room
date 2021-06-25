import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LegalModel } from '../settings.models';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-legal',
  templateUrl: './legal.component.html',
  styleUrls: ['./legal.component.scss']
})
export class LegalComponent implements OnInit {
  model: LegalModel = {} as LegalModel;
  editForm: FormGroup = {} as FormGroup;

  get legalParagraphs(): FormArray {
    return this.editForm.get('legalParagraphs') as FormArray;
  }

  constructor(private fb: FormBuilder, private settingsService: SettingsService) {
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      contentHtml: ['', Validators.required],
      legalParagraphs: this.fb.array([this.buildLegalParagraph()]),
    });
  }

  ngOnInit(): void {
    this.settingsService.getLegal().subscribe(result => {
      this.editForm.patchValue(result);
    });
  }

  saveModel(): void {
    const legal = this.createModel();
    this.settingsService.updateLegal(legal).subscribe();
  }

  buildLegalParagraph(): FormGroup {
    return this.fb.group({
      heading: ['', Validators.required],
      text: ['', Validators.required]
    });
  }

  addLegalParagraph(): void {
    this.legalParagraphs.push(this.buildLegalParagraph());
  }

  private createModel(): LegalModel {
    const formValue = this.editForm.getRawValue();
    return formValue as LegalModel;
  }
}
