import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetUniversityDepartmentForViewDto, UniversityDepartmentDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewUniversityDepartmentModal',
    templateUrl: './view-universityDepartment-modal.component.html',
})
export class ViewUniversityDepartmentModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetUniversityDepartmentForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetUniversityDepartmentForViewDto();
        this.item.universityDepartment = new UniversityDepartmentDto();
    }

    show(item: GetUniversityDepartmentForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
