import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetLessonsOfUniversityProfessorForViewDto,
    LessonsOfUniversityProfessorDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewLessonsOfUniversityProfessorModal',
    templateUrl: './view-lessonsOfUniversityProfessor-modal.component.html',
})
export class ViewLessonsOfUniversityProfessorModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetLessonsOfUniversityProfessorForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetLessonsOfUniversityProfessorForViewDto();
        this.item.lessonsOfUniversityProfessor = new LessonsOfUniversityProfessorDto();
    }

    show(item: GetLessonsOfUniversityProfessorForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
