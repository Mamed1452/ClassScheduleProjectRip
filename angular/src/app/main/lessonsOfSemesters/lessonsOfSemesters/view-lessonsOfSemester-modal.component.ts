﻿import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetLessonsOfSemesterForViewDto,
    LessonsOfSemesterDto,
    LessonOfSemesterTypeEnum,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewLessonsOfSemesterModal',
    templateUrl: './view-lessonsOfSemester-modal.component.html',
})
export class ViewLessonsOfSemesterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetLessonsOfSemesterForViewDto;
    lessonOfSemesterTypeEnum = LessonOfSemesterTypeEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetLessonsOfSemesterForViewDto();
        this.item.lessonsOfSemester = new LessonsOfSemesterDto();
    }

    show(item: GetLessonsOfSemesterForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
