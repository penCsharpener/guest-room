import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  @Input()
  heading: string = '';

  @Input()
  elements: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
