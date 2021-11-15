import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ImageComponent } from './image/image.component';
import { ListComponent } from './list/list.component';

const MODULES = [
  CommonModule,
  FlexLayoutModule
]

@NgModule({
  declarations: [ImageComponent, ListComponent],
  imports: [
    ...MODULES,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  exports: [
    ...MODULES,
    TranslateModule,
    ImageComponent,
    ListComponent
  ]
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/translations/', '.json');
}