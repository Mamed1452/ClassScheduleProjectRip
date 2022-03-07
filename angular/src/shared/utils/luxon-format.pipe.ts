import { Pipe, PipeTransform } from '@angular/core';
import { DateTime } from 'luxon';
import * as jalaliMoment from 'jalali-moment';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';

@Pipe({ name: 'luxonFormat' })
export class LuxonFormatPipe implements PipeTransform {
    transform(value: DateTime, format: string, isUTC = true) {
        if (!value) {
            return '';
        }
        if (abp.localization.currentLanguage.name === 'fa-IR' || abp.localization.currentLanguage.name === 'fa') {
            let valueString = (typeof (value) === 'object') ? value.toISO() : value;
            if (isUTC) {
                let nvalue = new DatePipe('en').transform(valueString, 'YYYY-MM-dd HH:mm:ss', undefined, 'fa');
                return jalaliMoment(nvalue).locale('fa').format(format);
            } else {
                return jalaliMoment(new Date(valueString)).locale('fa').format(format);
            }
        } else {
            return value.toFormat(format);
        }
    }
}
