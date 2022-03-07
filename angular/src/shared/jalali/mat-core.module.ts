﻿import { CdkTableModule } from '@angular/cdk/table';
import { NgModule } from '@angular/core';
import {  JalaliMomentDateAdapter,  MAT_MOMENT_DATE_ADAPTER_OPTIONS} from './jalali-moment-date-adapter';
import { JALALI_MOMENT_FORMATS, MOMENT_FORMATS } from './jalali_moment_formats';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { LocalizationService } from 'abp-ng2-module';

function getLanguage(localizationService: LocalizationService) {
return localizationService.currentLanguage.name;
}
@NgModule({
  imports: [
  ],
  exports: [
    CdkTableModule
  ],
  providers: [
    {
      provide: DateAdapter,
      useClass: JalaliMomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
    { provide: MAT_DATE_LOCALE, useFactory: getLanguage, deps: [LocalizationService] }, // en-GB  fr
    {
      provide: MAT_DATE_FORMATS,
      useFactory: locale => {
        if (locale === 'fa-IR') {
          return JALALI_MOMENT_FORMATS;
        } else {
          return MOMENT_FORMATS;
        }
      },
      deps: [MAT_DATE_LOCALE],
      //useValue: JALALI_MOMENT_FORMATS
    },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ]
})
export class JalaliDatePickerModule {}


