import { Component, OnInit } from '@angular/core';
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

  constructor(private route: ActivatedRoute, private settingsService: SettingsService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.roomId = +params.id;

      if (this.roomId) {
        this.title = `Room ${this.roomId} settings`;
      }
    });

    this.settingsService.getRoom(this.roomId).subscribe(result => this.roomModel = result);
  }

}
