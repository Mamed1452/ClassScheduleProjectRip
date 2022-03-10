import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    AssigningGradeToUniversityMajorsServiceProxy,
    CreateOrEditAssigningGradeToUniversityMajorDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AssigningGradeToUniversityMajorGradeLookupTableModalComponent } from './assigningGradeToUniversityMajor-grade-lookup-table-modal.component';
import { AssigningGradeToUniversityMajorUniversityMajorLookupTableModalComponent } from './assigningGradeToUniversityMajor-universityMajor-lookup-table-modal.component';

@Component({
    selector: 'createOrEditAssigningGradeToUniversityMajorModal',
    templateUrl: './create-or-edit-assigningGradeToUniversityMajor-modal.component.html',
})
export class CreateOrEditAssigningGradeToUniversityMajorModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('assigningGradeToUniversityMajorGradeLookupTableModal', { static: true })
    assigningGradeToUniversityMajorGradeLookupTableModal: AssigningGradeToUniversityMajorGradeLookupTableModalComponent;
    @ViewChild('assigningGradeToUniversityMajorUniversityMajorLookupTableModal', { static: true })
    assigningGradeToUniversityMajorUniversityMajorLookupTableModal: AssigningGradeToUniversityMajorUniversityMajorLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    assigningGradeToUniversityMajor: CreateOrEditAssigningGradeToUniversityMajorDto = new CreateOrEditAssigningGradeToUniversityMajorDto();

    gradeGradeName = '';
    universityMajorUniversityMajorName = '';

    constructor(
        injector: Injector,
        private _assigningGradeToUniversityMajorsServiceProxy: AssigningGradeToUniversityMajorsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(assigningGradeToUniversityMajorId?: number): void {
        if (!assigningGradeToUniversityMajorId) {
            this.assigningGradeToUniversityMajor = new CreateOrEditAssigningGradeToUniversityMajorDto();
            this.assigningGradeToUniversityMajor.id = assigningGradeToUniversityMajorId;
            this.gradeGradeName = '';
            this.universityMajorUniversityMajorName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._assigningGradeToUniversityMajorsServiceProxy
                .getAssigningGradeToUniversityMajorForEdit(assigningGradeToUniversityMajorId)
                .subscribe((result) => {
                    this.assigningGradeToUniversityMajor = result.assigningGradeToUniversityMajor;

                    this.gradeGradeName = result.gradeGradeName;
                    this.universityMajorUniversityMajorName = result.universityMajorUniversityMajorName;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._assigningGradeToUniversityMajorsServiceProxy
            .createOrEdit(this.assigningGradeToUniversityMajor)
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

    openSelectGradeModal() {
        this.assigningGradeToUniversityMajorGradeLookupTableModal.id = this.assigningGradeToUniversityMajor.gradeId;
        this.assigningGradeToUniversityMajorGradeLookupTableModal.displayName = this.gradeGradeName;
        this.assigningGradeToUniversityMajorGradeLookupTableModal.show();
    }
    openSelectUniversityMajorModal() {
        this.assigningGradeToUniversityMajorUniversityMajorLookupTableModal.id = this.assigningGradeToUniversityMajor.universityMajorId;
        this.assigningGradeToUniversityMajorUniversityMajorLookupTableModal.displayName = this.universityMajorUniversityMajorName;
        this.assigningGradeToUniversityMajorUniversityMajorLookupTableModal.show();
    }

    setGradeIdNull() {
        this.assigningGradeToUniversityMajor.gradeId = null;
        this.gradeGradeName = '';
        this.assigningGradeToUniversityMajor.nameAssignedGradeToUniversityMajor= this.gradeGradeName+ " "+  this.universityMajorUniversityMajorName;
    }
    setUniversityMajorIdNull() {
        this.assigningGradeToUniversityMajor.universityMajorId = null;
        this.universityMajorUniversityMajorName = '';
        this.assigningGradeToUniversityMajor.nameAssignedGradeToUniversityMajor= this.gradeGradeName+ " "+  this.universityMajorUniversityMajorName;
    }

    getNewGradeId() {
        this.assigningGradeToUniversityMajor.gradeId = this.assigningGradeToUniversityMajorGradeLookupTableModal.id;
        this.gradeGradeName = this.assigningGradeToUniversityMajorGradeLookupTableModal.displayName;
        this.assigningGradeToUniversityMajor.nameAssignedGradeToUniversityMajor= this.gradeGradeName+ " "+  this.universityMajorUniversityMajorName;
    }
    getNewUniversityMajorId() {
        this.assigningGradeToUniversityMajor.universityMajorId = this.assigningGradeToUniversityMajorUniversityMajorLookupTableModal.id;
        this.universityMajorUniversityMajorName = this.assigningGradeToUniversityMajorUniversityMajorLookupTableModal.displayName;
        this.assigningGradeToUniversityMajor.nameAssignedGradeToUniversityMajor= this.gradeGradeName+ " "+  this.universityMajorUniversityMajorName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
