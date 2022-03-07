import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { SemestersServiceProxy, CreateOrEditSemesterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'masterDetailChild_AssigningGradeToUniversityMajor_createOrEditSemesterModal',
    templateUrl: './masterDetailChild_AssigningGradeToUniversityMajor_create-or-edit-semester-modal.component.html',
})
export class MasterDetailChild_AssigningGradeToUniversityMajor_CreateOrEditSemesterModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    semester: CreateOrEditSemesterDto = new CreateOrEditSemesterDto();

    constructor(
        injector: Injector,
        private _semestersServiceProxy: SemestersServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    assigningGradeToUniversityMajorId: any;

    show(assigningGradeToUniversityMajorId: any, semesterId?: number): void {
        this.assigningGradeToUniversityMajorId = assigningGradeToUniversityMajorId;

        if (!semesterId) {
            this.semester = new CreateOrEditSemesterDto();
            this.semester.id = semesterId;

            this.active = true;
            this.modal.show();
        } else {
            this._semestersServiceProxy.getSemesterForEdit(semesterId).subscribe((result) => {
                this.semester = result.semester;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this.semester.assigningGradeToUniversityMajorId = this.assigningGradeToUniversityMajorId;

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

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
