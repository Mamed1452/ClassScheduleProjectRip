import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetSemesterForViewDto, SemesterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'masterDetailChild_AssigningGradeToUniversityMajor_viewSemesterModal',
    templateUrl: './masterDetailChild_AssigningGradeToUniversityMajor_view-semester-modal.component.html',
})
export class MasterDetailChild_AssigningGradeToUniversityMajor_ViewSemesterModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetSemesterForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetSemesterForViewDto();
        this.item.semester = new SemesterDto();
    }

    show(item: GetSemesterForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
