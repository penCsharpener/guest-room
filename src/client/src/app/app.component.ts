import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private translate: TranslateService) {
    this.translate.setDefaultLang(environment.defaultLanguage);
    this.translate.use(environment.defaultLanguage);
  }

  ngOnInit(): void {
    
  }
  title = 'client';
}
