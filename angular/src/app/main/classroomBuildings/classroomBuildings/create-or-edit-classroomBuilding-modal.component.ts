import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    ClassroomBuildingsServiceProxy,
    CreateOrEditClassroomBuildingDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditClassroomBuildingModal',
    templateUrl: './create-or-edit-classroomBuilding-modal.component.html',
})
export class CreateOrEditClassroomBuildingModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    classroomBuilding: CreateOrEditClassroomBuildingDto = new CreateOrEditClassroomBuildingDto();

    constructor(
        injector: Injector,
        private _classroomBuildingsServiceProxy: ClassroomBuildingsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(classroomBuildingId?: number): void {
        if (!classroomBuildingId) {
            this.classroomBuilding = new CreateOrEditClassroomBuildingDto();
            this.classroomBuilding.id = classroomBuildingId;

            this.active = true;
            this.modal.show();
        } else {
            this._classroomBuildingsServiceProxy
                .getClassroomBuildingForEdit(classroomBuildingId)
                .subscribe((result) => {
                    this.classroomBuilding = result.classroomBuilding;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._classroomBuildingsServiceProxy
            .createOrEdit(this.classroomBuilding)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
