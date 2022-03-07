import { Pipe, PipeTransform } from '@angular/core';
import * as moment from '@node_modules/jalali-moment';

@Pipe({
  name: 'widgetJalali'
})
export class WidgetJalaliPipe implements PipeTransform {

    transform(val: string, format: string = 'YYYY/MM/DD'): string {
        if (val) {
            return moment(new Date(val)).locale('fa').format(format);
        }
    }

}
