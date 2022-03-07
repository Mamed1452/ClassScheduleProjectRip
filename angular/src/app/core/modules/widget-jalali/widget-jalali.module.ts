import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WidgetJalaliPipe } from '@app/core/pipe/widget-jalali.pipe';


@NgModule({
    declarations: [
        WidgetJalaliPipe
    ],
    imports: [
        CommonModule
    ],
    exports: [
        WidgetJalaliPipe
    ]
})
export class WidgetJalaliModule {
}
