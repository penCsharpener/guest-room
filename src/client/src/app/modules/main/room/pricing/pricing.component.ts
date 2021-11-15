import { Component, Input, OnInit } from '@angular/core';
import { PricingModel } from '../../../settings/settings.models';

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.scss']
})
export class PricingComponent implements OnInit {

  @Input()
  pricing = {} as PricingModel;

  constructor() { }

  ngOnInit(): void {
  }

}
