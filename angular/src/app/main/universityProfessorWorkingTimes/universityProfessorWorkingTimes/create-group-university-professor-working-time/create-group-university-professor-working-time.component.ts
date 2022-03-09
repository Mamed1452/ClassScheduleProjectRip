import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    UniversityProfessorWorkingTimesServiceProxy,
    CreateGroupInputDto

} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent } from '../universityProfessorWorkingTime-universityProfessor-lookup-table-modal.component';
import { UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent } from '..//universityProfessorWorkingTime-workTimeInDay-lookup-table-modal.component';

@Component({
    selector: 'createGroupUniversityProfessorWorkingTimeModule',
    templateUrl: './create-group-university-professor-working-time.component.html',
})
export class CreateGroupUniversityProfessorWorkingTimeComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('universityProfessorWorkingTimeUniversityProfessorLookupTableModal', { static: true })
    universityProfessorWorkingTimeUniversityProfessorLookupTableModal: UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent;
    @ViewChild('universityProfessorWorkingTimeWorkTimeInDayLookupTableModal', { static: true })
    universityProfessorWorkingTimeWorkTimeInDayLookupTableModal: UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent;
    @ViewChild('universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2', { static: true })
    universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2: UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityProfessorWorkingTime: CreateGroupInputDto = new CreateGroupInputDto();

    universityProfessorUniversityProfessorName = '';
    workTimeInDayNameWorkTimeInDayMax = '';
    workTimeInDayNameWorkTimeInDayMin = '';
    constructor(
        injector: Injector,
        private _universityProfessorWorkingTimesServiceProxy: UniversityProfessorWorkingTimesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }
    show(): void {

            this.universityProfessorWorkingTime = new CreateGroupInputDto();
            this.universityProfessorUniversityProfessorName = '';
            this.workTimeInDayNameWorkTimeInDayMax = '';
            this.workTimeInDayNameWorkTimeInDayMin = '';
            this.active = true;
            this.modal.show();
    }


    save(): void {
        this.saving = true;

        this._universityProfessorWorkingTimesServiceProxy
            .createGroup(this.universityProfessorWorkingTime)
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
    openSelectWorkTimeInDayModalMin() {
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.id = this.universityProfessorWorkingTime.workTimeInDayIdMin;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.displayName = this.workTimeInDayNameWorkTimeInDayMin;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.show();
    }
    openSelectWorkTimeInDayModalMax() {
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2.id = this.universityProfessorWorkingTime.workTimeInDayIdMax;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2.displayName = this.workTimeInDayNameWorkTimeInDayMax;
        this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2.show();
    }

    setUniversityProfessorIdNull() {
        this.universityProfessorWorkingTime.universityProfessorId = null;
        this.universityProfessorUniversityProfessorName = '';
    }
    setWorkTimeInDayIdNullMin() {
        this.universityProfessorWorkingTime.workTimeInDayIdMin = null;
        this.workTimeInDayNameWorkTimeInDayMin = '';
    }
    setWorkTimeInDayIdNullMax() {
        this.universityProfessorWorkingTime.workTimeInDayIdMax = null;
        this.workTimeInDayNameWorkTimeInDayMax= '';
    }
    getNewUniversityProfessorId() {
        this.universityProfessorWorkingTime.universityProfessorId = this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.id;
        this.universityProfessorUniversityProfessorName = this.universityProfessorWorkingTimeUniversityProfessorLookupTableModal.displayName;
    }
    getNewWorkTimeInDayIdMin() {
        this.universityProfessorWorkingTime.workTimeInDayIdMin = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.id;
        this.workTimeInDayNameWorkTimeInDayMin = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal.displayName;
    }
    getNewWorkTimeInDayIdMax() {
        this.universityProfessorWorkingTime.workTimeInDayIdMax = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2.id;
        this.workTimeInDayNameWorkTimeInDayMax = this.universityProfessorWorkingTimeWorkTimeInDayLookupTableModal2.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
