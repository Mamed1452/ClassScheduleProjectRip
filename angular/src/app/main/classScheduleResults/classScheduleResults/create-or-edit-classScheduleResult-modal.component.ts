import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    ClassScheduleResultsServiceProxy,
    CreateOrEditClassScheduleResultDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent } from './classScheduleResult-listOfAllCalculatedResult-lookup-table-modal.component';
import { ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent } from './classScheduleResult-classScheduleModeSpace-lookup-table-modal.component';

@Component({
    selector: 'createOrEditClassScheduleResultModal',
    templateUrl: './create-or-edit-classScheduleResult-modal.component.html',
})
export class CreateOrEditClassScheduleResultModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('classScheduleResultListOfAllCalculatedResultLookupTableModal', { static: true })
    classScheduleResultListOfAllCalculatedResultLookupTableModal: ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent;
    @ViewChild('classScheduleResultClassScheduleModeSpaceLookupTableModal', { static: true })
    classScheduleResultClassScheduleModeSpaceLookupTableModal: ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    classScheduleResult: CreateOrEditClassScheduleResultDto = new CreateOrEditClassScheduleResultDto();

    listOfAllCalculatedResultNameCalculatedResult = '';
    classScheduleModeSpaceNameClassScheduleModeSpaces = '';

    constructor(
        injector: Injector,
        private _classScheduleResultsServiceProxy: ClassScheduleResultsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(classScheduleResultId?: number): void {
        if (!classScheduleResultId) {
            this.classScheduleResult = new CreateOrEditClassScheduleResultDto();
            this.classScheduleResult.id = classScheduleResultId;
            this.listOfAllCalculatedResultNameCalculatedResult = '';
            this.classScheduleModeSpaceNameClassScheduleModeSpaces = '';

            this.active = true;
            this.modal.show();
        } else {
            this._classScheduleResultsServiceProxy
                .getClassScheduleResultForEdit(classScheduleResultId)
                .subscribe((result) => {
                    this.classScheduleResult = result.classScheduleResult;

                    this.listOfAllCalculatedResultNameCalculatedResult =
                        result.listOfAllCalculatedResultNameCalculatedResult;
                    this.classScheduleModeSpaceNameClassScheduleModeSpaces =
                        result.classScheduleModeSpaceNameClassScheduleModeSpaces;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._classScheduleResultsServiceProxy
            .createOrEdit(this.classScheduleResult)
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

    openSelectListOfAllCalculatedResultModal() {
        this.classScheduleResultListOfAllCalculatedResultLookupTableModal.id = this.classScheduleResult.listOfAllCalculatedResultId;
        this.classScheduleResultListOfAllCalculatedResultLookupTableModal.displayName = this.listOfAllCalculatedResultNameCalculatedResult;
        this.classScheduleResultListOfAllCalculatedResultLookupTableModal.show();
    }
    openSelectClassScheduleModeSpaceModal() {
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.id = this.classScheduleResult.classScheduleModeSpaceId;
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.displayName = this.classScheduleModeSpaceNameClassScheduleModeSpaces;
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.show();
    }

    setListOfAllCalculatedResultIdNull() {
        this.classScheduleResult.listOfAllCalculatedResultId = null;
        this.listOfAllCalculatedResultNameCalculatedResult = '';
    }
    setClassScheduleModeSpaceIdNull() {
        this.classScheduleResult.classScheduleModeSpaceId = null;
        this.classScheduleModeSpaceNameClassScheduleModeSpaces = '';
    }

    getNewListOfAllCalculatedResultId() {
        this.classScheduleResult.listOfAllCalculatedResultId = this.classScheduleResultListOfAllCalculatedResultLookupTableModal.id;
        this.listOfAllCalculatedResultNameCalculatedResult = this.classScheduleResultListOfAllCalculatedResultLookupTableModal.displayName;
    }
    getNewClassScheduleModeSpaceId() {
        this.classScheduleResult.classScheduleModeSpaceId = this.classScheduleResultClassScheduleModeSpaceLookupTableModal.id;
        this.classScheduleModeSpaceNameClassScheduleModeSpaces = this.classScheduleResultClassScheduleModeSpaceLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
