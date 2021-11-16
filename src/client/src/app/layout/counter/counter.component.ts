import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { VisitorService } from '../../shared/visitor.service';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})
export class CounterComponent implements OnInit {
  visitor$ = of<number>();

  constructor(private visitorService: VisitorService) { }

  ngOnInit(): void {
    this.visitor$ = this.visitorService.countVisitor();
  }
}
