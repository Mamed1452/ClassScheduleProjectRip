import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetUniversityProfessorForViewDto, UniversityProfessorDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewUniversityProfessorModal',
    templateUrl: './view-universityProfessor-modal.component.html',
})
export class ViewUniversityProfessorModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetUniversityProfessorForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetUniversityProfessorForViewDto();
        this.item.universityProfessor = new UniversityProfessorDto();
    }

    show(item: GetUniversityProfessorForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
