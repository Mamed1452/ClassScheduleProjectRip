import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetAssigningGradeToUniversityMajorForViewDto,
    AssigningGradeToUniversityMajorDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAssigningGradeToUniversityMajorModal',
    templateUrl: './view-assigningGradeToUniversityMajor-modal.component.html',
})
export class ViewAssigningGradeToUniversityMajorModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAssigningGradeToUniversityMajorForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetAssigningGradeToUniversityMajorForViewDto();
        this.item.assigningGradeToUniversityMajor = new AssigningGradeToUniversityMajorDto();
    }

    show(item: GetAssigningGradeToUniversityMajorForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
