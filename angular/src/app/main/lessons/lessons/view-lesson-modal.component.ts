import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetLessonForViewDto,
    LessonDto,
    LessonTypeEnum,
    ClassificationLessonEnum,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewLessonModal',
    templateUrl: './view-lesson-modal.component.html',
})
export class ViewLessonModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetLessonForViewDto;
    lessonTypeEnum = LessonTypeEnum;
    classificationLessonEnum = ClassificationLessonEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetLessonForViewDto();
        this.item.lesson = new LessonDto();
    }

    show(item: GetLessonForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
