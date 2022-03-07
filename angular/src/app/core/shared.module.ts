import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { JalaliDatePickerModule } from '@shared/jalali/mat-core.module';
import { UploadComponent } from './upload/upload.component';
import { NgxUploaderModule } from 'ngx-uploader';
import { CommonModule } from '@angular/common';
import { UtilsModule } from '@shared/utils/utils.module';
import { JalaliPipe } from './pipe/jalali';
import { CamelCasePipe } from './pipe/camel-case';
import { SafeStylePipe } from './pipe/safe-style.pipe';
import { NumberFormatPipe } from '../../core/pipes/number-format.pipe';
import { SeparatorAmountDirective } from '../../core/directive/separator-amount.directive';
import { SanitizerHtmlEditorPipe } from './pipe/sanitizer-html-editor.pipe';
import { OnlyNumberDirective, OnlyDecimalDirective } from 'core/directive/numbers-only';

@NgModule({
    imports: [
        UtilsModule,
        JalaliDatePickerModule,
        FormsModule,
        ReactiveFormsModule,
        TableModule,
        CardModule,
        DropdownModule,
        NgxUploaderModule,
        CommonModule
    ],
    declarations: [
        UploadComponent,
        JalaliPipe,
        CamelCasePipe,
        SafeStylePipe,
        NumberFormatPipe,
        SeparatorAmountDirective,
        SanitizerHtmlEditorPipe,
        OnlyNumberDirective,
        OnlyDecimalDirective
    ],
  exports: [
    UploadComponent,
    JalaliPipe,
    CamelCasePipe,
    SafeStylePipe,
    NumberFormatPipe,
    SeparatorAmountDirective,
    SanitizerHtmlEditorPipe,
    OnlyNumberDirective,
    OnlyDecimalDirective
  ],
    providers: []
})
export class SharedModule {
}
