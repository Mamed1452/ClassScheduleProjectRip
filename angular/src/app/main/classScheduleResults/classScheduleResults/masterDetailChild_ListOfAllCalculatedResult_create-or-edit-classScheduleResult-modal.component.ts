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
import { MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent } from './masterDetailChild_ListOfAllCalculatedResult_classScheduleResult-classScheduleModeSpace-lookup-table-modal.component';

@Component({
    selector: 'masterDetailChild_ListOfAllCalculatedResult_createOrEditClassScheduleResultModal',
    templateUrl:
        './masterDetailChild_ListOfAllCalculatedResult_create-or-edit-classScheduleResult-modal.component.html',
})
export class MasterDetailChild_ListOfAllCalculatedResult_CreateOrEditClassScheduleResultModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('classScheduleResultClassScheduleModeSpaceLookupTableModal', { static: true })
    classScheduleResultClassScheduleModeSpaceLookupTableModal: MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    classScheduleResult: CreateOrEditClassScheduleResultDto = new CreateOrEditClassScheduleResultDto();

    classScheduleModeSpaceNameClassScheduleModeSpaces = '';

    constructor(
        injector: Injector,
        private _classScheduleResultsServiceProxy: ClassScheduleResultsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    listOfAllCalculatedResultId: any;

    show(listOfAllCalculatedResultId: any, classScheduleResultId?: number): void {
        this.listOfAllCalculatedResultId = listOfAllCalculatedResultId;

        if (!classScheduleResultId) {
            this.classScheduleResult = new CreateOrEditClassScheduleResultDto();
            this.classScheduleResult.id = classScheduleResultId;
            this.classScheduleModeSpaceNameClassScheduleModeSpaces = '';

            this.active = true;
            this.modal.show();
        } else {
            this._classScheduleResultsServiceProxy
                .getClassScheduleResultForEdit(classScheduleResultId)
                .subscribe((result) => {
                    this.classScheduleResult = result.classScheduleResult;

                    this.classScheduleModeSpaceNameClassScheduleModeSpaces =
                        result.classScheduleModeSpaceNameClassScheduleModeSpaces;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this.classScheduleResult.listOfAllCalculatedResultId = this.listOfAllCalculatedResultId;

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

    openSelectClassScheduleModeSpaceModal() {
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.id = this.classScheduleResult.classScheduleModeSpaceId;
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.displayName = this.classScheduleModeSpaceNameClassScheduleModeSpaces;
        this.classScheduleResultClassScheduleModeSpaceLookupTableModal.show();
    }

    setClassScheduleModeSpaceIdNull() {
        this.classScheduleResult.classScheduleModeSpaceId = null;
        this.classScheduleModeSpaceNameClassScheduleModeSpaces = '';
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
