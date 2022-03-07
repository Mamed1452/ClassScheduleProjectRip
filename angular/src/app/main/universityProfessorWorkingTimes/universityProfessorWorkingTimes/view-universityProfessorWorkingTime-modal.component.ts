import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetUniversityProfessorWorkingTimeForViewDto,
    UniversityProfessorWorkingTimeDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewUniversityProfessorWorkingTimeModal',
    templateUrl: './view-universityProfessorWorkingTime-modal.component.html',
})
export class ViewUniversityProfessorWorkingTimeModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetUniversityProfessorWorkingTimeForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetUniversityProfessorWorkingTimeForViewDto();
        this.item.universityProfessorWorkingTime = new UniversityProfessorWorkingTimeDto();
    }

    show(item: GetUniversityProfessorWorkingTimeForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
