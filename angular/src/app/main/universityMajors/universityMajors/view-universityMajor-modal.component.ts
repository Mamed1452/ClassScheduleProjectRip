import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetUniversityMajorForViewDto,
    UniversityMajorDto,
    UniversityMajorTypeEnum,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewUniversityMajorModal',
    templateUrl: './view-universityMajor-modal.component.html',
})
export class ViewUniversityMajorModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetUniversityMajorForViewDto;
    universityMajorTypeEnum = UniversityMajorTypeEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetUniversityMajorForViewDto();
        this.item.universityMajor = new UniversityMajorDto();
    }

    show(item: GetUniversityMajorForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
