﻿import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'numberFormat'
})
export class NumberFormatPipe implements PipeTransform {

    transform(value: any): any {
        let numberOnly = value.toString().replace(/[^0-9.]/g, '');
        // Round up to the nearest 100's place
        // numberOnly = Math.ceil( parseInt(numberOnly) / 100 ) * 100;
        // console.log('thing: ', numberOnly);
        // numberOnly = numberOnly.toString();
        return new Intl.NumberFormat('fa-En', { style: 'decimal'}).format(numberOnly);
    }
}
