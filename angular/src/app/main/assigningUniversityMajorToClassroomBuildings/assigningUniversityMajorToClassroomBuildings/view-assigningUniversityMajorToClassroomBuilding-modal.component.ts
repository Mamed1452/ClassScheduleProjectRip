import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetAssigningUniversityMajorToClassroomBuildingForViewDto,
    AssigningUniversityMajorToClassroomBuildingDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAssigningUniversityMajorToClassroomBuildingModal',
    templateUrl: './view-assigningUniversityMajorToClassroomBuilding-modal.component.html',
})
export class ViewAssigningUniversityMajorToClassroomBuildingModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAssigningUniversityMajorToClassroomBuildingForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetAssigningUniversityMajorToClassroomBuildingForViewDto();
        this.item.assigningUniversityMajorToClassroomBuilding = new AssigningUniversityMajorToClassroomBuildingDto();
    }

    show(item: GetAssigningUniversityMajorToClassroomBuildingForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
