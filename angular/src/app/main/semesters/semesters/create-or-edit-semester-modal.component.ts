import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { SemestersServiceProxy, CreateOrEditSemesterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { SemesterAssigningGradeToUniversityMajorLookupTableModalComponent } from './semester-assigningGradeToUniversityMajor-lookup-table-modal.component';

@Component({
    selector: 'createOrEditSemesterModal',
    templateUrl: './create-or-edit-semester-modal.component.html',
})
export class CreateOrEditSemesterModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('semesterAssigningGradeToUniversityMajorLookupTableModal', { static: true })
    semesterAssigningGradeToUniversityMajorLookupTableModal: SemesterAssigningGradeToUniversityMajorLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    semester: CreateOrEditSemesterDto = new CreateOrEditSemesterDto();

    assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = '';

    constructor(
        injector: Injector,
        private _semestersServiceProxy: SemestersServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(semesterId?: number): void {
        if (!semesterId) {
            this.semester = new CreateOrEditSemesterDto();
            this.semester.id = semesterId;
            this.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = '';

            this.active = true;
            this.modal.show();
        } else {
            this._semestersServiceProxy.getSemesterForEdit(semesterId).subscribe((result) => {
                this.semester = result.semester;

                this.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor =
                    result.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._semestersServiceProxy
            .createOrEdit(this.semester)
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

    openSelectAssigningGradeToUniversityMajorModal() {
        this.semesterAssigningGradeToUniversityMajorLookupTableModal.id = this.semester.assigningGradeToUniversityMajorId;
        this.semesterAssigningGradeToUniversityMajorLookupTableModal.displayName = this.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor;
        this.semesterAssigningGradeToUniversityMajorLookupTableModal.show();
    }

    setAssigningGradeToUniversityMajorIdNull() {
        this.semester.assigningGradeToUniversityMajorId = null;
        this.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = '';
    }

    getNewAssigningGradeToUniversityMajorId() {
        this.semester.assigningGradeToUniversityMajorId = this.semesterAssigningGradeToUniversityMajorLookupTableModal.id;
        this.assigningGradeToUniversityMajorNameAssignedGradeToUniversityMajor = this.semesterAssigningGradeToUniversityMajorLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
