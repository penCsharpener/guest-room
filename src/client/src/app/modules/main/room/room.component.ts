import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs/operators';
import { RoomModel } from '../../settings/settings.models';
import { SettingsService } from '../../settings/settings.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  roomId = 0;
  roomModel = <RoomModel>{};

  constructor(private route: ActivatedRoute, private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.route.params.pipe(
        tap(params => this.roomId = +params.id)
      ).subscribe(_ => this.settingsService.getRoom(this.roomId).subscribe(result => this.roomModel = result));
  }

  getFurnishing(): string[] {
    return this.roomModel.furnishing?.split(/\r?\n/) ?? [];
  }

  getMisc(): string[] {
    return this.roomModel?.miscellaneous?.content ?? [];
  }
}
