import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RoomModel } from '../settings.models';
import { SettingsService } from '../settings.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  title = '';
  roomId = 0;
  roomModel = <RoomModel>{};
  editForm = <FormGroup>{};

  get pricingGroup(): FormGroup {
    return this.editForm.get('pricing') as FormGroup;
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private settingsService: SettingsService) {
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      contentHtml: ['', Validators.required],
      furnishing: ['', Validators.required],
      pricing: this.fb.group({
        firstNightOnePerson: [0, Validators.required],
        everyFollowingNight: [0, Validators.required],
        twoPersons: [0, Validators.required],
        breakfastPerPerson: [0, Validators.required]
      }),
      miscellaneous: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.roomId = +params.id;

      if (this.roomId) {
        this.title = `Room ${this.roomId} settings`;
      }
    });

    this.settingsService.getRoom(this.roomId).subscribe(result => {
      console.log("🚀 ~ file: room.component.ts ~ line 44 ~ RoomComponent ~ this.settingsService.getRoom ~ result", result)
      this.editForm.patchValue(result);
      this.editForm.patchValue({ miscellaneous: result.miscellaneous.content.join('\r\n') });
    });
  }

  saveModel(): void {
    const room = this.createModel();
    this.settingsService.updateRoom(this.roomId, room).subscribe();
  }

  private createModel(): RoomModel {
    const formValue = this.editForm.getRawValue();
    return {
      title: formValue.title,
      contentHtml: formValue.contentHtml,
      furnishing: formValue.furnishing,
      pricing: formValue.pricing,
      miscellaneous: { content: formValue.miscellaneous.split(/\r?\n/) }
    } as RoomModel;
  }
}
