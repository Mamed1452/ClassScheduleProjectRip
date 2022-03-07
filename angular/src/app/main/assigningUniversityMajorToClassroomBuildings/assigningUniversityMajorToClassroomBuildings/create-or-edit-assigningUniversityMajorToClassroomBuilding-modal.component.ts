import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    AssigningUniversityMajorToClassroomBuildingsServiceProxy,
    CreateOrEditAssigningUniversityMajorToClassroomBuildingDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModalComponent } from './assigningUniversityMajorToClassroomBuilding-universityMajor-lookup-table-modal.component';
import { AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModalComponent } from './assigningUniversityMajorToClassroomBuilding-classroomBuilding-lookup-table-modal.component';

@Component({
    selector: 'createOrEditAssigningUniversityMajorToClassroomBuildingModal',
    templateUrl: './create-or-edit-assigningUniversityMajorToClassroomBuilding-modal.component.html',
})
export class CreateOrEditAssigningUniversityMajorToClassroomBuildingModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal', { static: true })
    assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal: AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModalComponent;
    @ViewChild('assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal', { static: true })
    assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal: AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    assigningUniversityMajorToClassroomBuilding: CreateOrEditAssigningUniversityMajorToClassroomBuildingDto = new CreateOrEditAssigningUniversityMajorToClassroomBuildingDto();

    universityMajorUniversityMajorName = '';
    classroomBuildingClassroomBuildingName = '';

    constructor(
        injector: Injector,
        private _assigningUniversityMajorToClassroomBuildingsServiceProxy: AssigningUniversityMajorToClassroomBuildingsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(assigningUniversityMajorToClassroomBuildingId?: number): void {
        if (!assigningUniversityMajorToClassroomBuildingId) {
            this.assigningUniversityMajorToClassroomBuilding = new CreateOrEditAssigningUniversityMajorToClassroomBuildingDto();
            this.assigningUniversityMajorToClassroomBuilding.id = assigningUniversityMajorToClassroomBuildingId;
            this.universityMajorUniversityMajorName = '';
            this.classroomBuildingClassroomBuildingName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._assigningUniversityMajorToClassroomBuildingsServiceProxy
                .getAssigningUniversityMajorToClassroomBuildingForEdit(assigningUniversityMajorToClassroomBuildingId)
                .subscribe((result) => {
                    this.assigningUniversityMajorToClassroomBuilding =
                        result.assigningUniversityMajorToClassroomBuilding;

                    this.universityMajorUniversityMajorName = result.universityMajorUniversityMajorName;
                    this.classroomBuildingClassroomBuildingName = result.classroomBuildingClassroomBuildingName;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._assigningUniversityMajorToClassroomBuildingsServiceProxy
            .createOrEdit(this.assigningUniversityMajorToClassroomBuilding)
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

    openSelectUniversityMajorModal() {
        this.assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal.id = this.assigningUniversityMajorToClassroomBuilding.universityMajorId;
        this.assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal.displayName = this.universityMajorUniversityMajorName;
        this.assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal.show();
    }
    openSelectClassroomBuildingModal() {
        this.assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal.id = this.assigningUniversityMajorToClassroomBuilding.classroomBuildingId;
        this.assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal.displayName = this.classroomBuildingClassroomBuildingName;
        this.assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal.show();
    }

    setUniversityMajorIdNull() {
        this.assigningUniversityMajorToClassroomBuilding.universityMajorId = null;
        this.universityMajorUniversityMajorName = '';
    }
    setClassroomBuildingIdNull() {
        this.assigningUniversityMajorToClassroomBuilding.classroomBuildingId = null;
        this.classroomBuildingClassroomBuildingName = '';
    }

    getNewUniversityMajorId() {
        this.assigningUniversityMajorToClassroomBuilding.universityMajorId = this.assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal.id;
        this.universityMajorUniversityMajorName = this.assigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModal.displayName;
    }
    getNewClassroomBuildingId() {
        this.assigningUniversityMajorToClassroomBuilding.classroomBuildingId = this.assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal.id;
        this.classroomBuildingClassroomBuildingName = this.assigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
