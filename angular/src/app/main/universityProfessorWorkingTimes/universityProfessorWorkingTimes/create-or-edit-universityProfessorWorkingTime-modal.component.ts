import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    UniversityProfessorWorkingTimesServiceProxy,
    CreateOrEditUniversityProfessorWorkingTimeDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent } from './universityProfessorWorkingTime-universityProfessor-lookup-table-modal.component';
import { UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent } from './universityProfessorWorkingTime-workTimeInDay-lookup-table-modal.component';

@Component({
    selector: 'createOrEditUniversityProfessorWorkingTimeModal',
    templateUrl: './create-or-edit-universityProfessorWorkingTime-modal.component.html',
})
export class CreateOrEditUniversityProfessorWorkingTimeModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('universityProfessorWorkingTimeUniversityProfessorLookupTableModal', { static: true })
    universityProfessorWorkingTimeUniversityProfessorLookupTableModal: UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent;
    @ViewChild('universityProfessorWorkingTimeWorkTimeInDayLookupTableModal', { static: true })
    universityProfessorWorkingTimeWorkTimeInDayLookupTableModal: UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityProfessorWorkingTime: CreateOrEditUniversityProfessorWorkingTimeDto = new CreateOrEditUniversityProfessorWorkingTimeDto();

    universityProfessorUniversityProfessorName = '';
    workTimeInDayNameWorkTimeInDay = '';

    constructor(
        injector: Injector,
        private _universityProfessorWorkingTimesServiceProxy: UniversityProfessorWorkingTimesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(universityProfessorWorkingTimeId?: number): void {
        if (!universityProfessorWorkingTimeId) {
            this.universityProfessorWorkingTime = new CreateOrEditUniversityProfessorWorkingTimeDto();
            this.universityProfessorWorkingTime.id = universityProfessorWorkingTimeId;
            this.universityProfessorUniversityProfessorName = '';
            this.workTimeInDayNameWorkTimeInDay = '';

            this.active = true;
            this.modal.show();
        } else {
            this._universityProfessorWorkingTimesServiceProxy
                .getUniversityProfessorWorkingTimeForEdit(universityProfessorWorkingTimeId)
                .subscribe((result) => {
                    this.universityProfessorWorkingTime = result.universityProfessorWorkingTime;

                    this.universityProfessorUniversityProfessorName = result.universityProfessorUniversityProfessorName;
                    this.workTimeInDayNameWorkTimeInDay = result.workTimeInDayNameWorkTimeInDay;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._universityProfessorWorkingTimesServiceProxy
            .createOrEdit(this.universityProfessorWorkingTime)
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

    openSelectUniversityProfessorModal() {
        this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.id = this.universityProfessorWorkingTime.universityProfessorId;
        this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.displayName = this.universityProfessorUniversityProfessorName;
        this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.show();
    }
    openSelectWorkTimeInDayModal() {
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.id = this.universityProfessorWorkingTime.workTimeInDayId;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.displayName = this.workTimeInDayNameWorkTimeInDay;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.show();
    }

    setUniversityProfessorIdNull() {
        this.universityProfessorWorkingTime.universityProfessorId = null;
        this.universityProfessorUniversityProfessorName = '';
    }
    setWorkTimeInDayIdNull() {
        this.universityProfessorWorkingTime.workTimeInDayId = null;
        this.workTimeInDayNameWorkTimeInDay = '';
    }

    getNewUniversityProfessorId() {
        this.universityProfessorWorkingTime.universityProfessorId = this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.id;
        this.universityProfessorUniversityProfessorName = this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.displayName;
    }
    getNewWorkTimeInDayId() {
        this.universityProfessorWorkingTime.workTimeInDayId = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.id;
        this.workTimeInDayNameWorkTimeInDay = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
