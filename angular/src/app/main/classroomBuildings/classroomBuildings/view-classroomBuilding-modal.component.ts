import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetClassroomBuildingForViewDto,
    ClassroomBuildingDto,
    ClassificationClassroomBuildingEnum,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewClassroomBuildingModal',
    templateUrl: './view-classroomBuilding-modal.component.html',
})
export class ViewClassroomBuildingModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetClassroomBuildingForViewDto;
    classificationClassroomBuildingEnum = ClassificationClassroomBuildingEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetClassroomBuildingForViewDto();
        this.item.classroomBuilding = new ClassroomBuildingDto();
    }

    show(item: GetClassroomBuildingForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
