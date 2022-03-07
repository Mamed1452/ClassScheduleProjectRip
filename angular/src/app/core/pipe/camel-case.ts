import { Pipe, PipeTransform, Injectable } from '@angular/core';
import * as moment from 'jalali-moment';

@Pipe({
  name: 'camelCase'
})
@Injectable()
export class CamelCasePipe implements PipeTransform {
  transform(val: string): string {
    if (val) {
      return val
        .replace(/\s(.)/g, ($1) => $1.toUpperCase())
        .replace(/\s/g, '')
        .replace(/^(.)/, ($1) => $1.toLowerCase());
    }
  }
}
