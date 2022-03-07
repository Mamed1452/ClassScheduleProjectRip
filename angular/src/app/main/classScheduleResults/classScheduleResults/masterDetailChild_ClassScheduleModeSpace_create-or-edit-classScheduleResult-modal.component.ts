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

@Component({
    selector: 'masterDetailChild_ClassScheduleModeSpace_createOrEditClassScheduleResultModal',
    templateUrl: './masterDetailChild_ClassScheduleModeSpace_create-or-edit-classScheduleResult-modal.component.html',
})
export class MasterDetailChild_ClassScheduleModeSpace_CreateOrEditClassScheduleResultModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    classScheduleResult: CreateOrEditClassScheduleResultDto = new CreateOrEditClassScheduleResultDto();

    constructor(
        injector: Injector,
        private _classScheduleResultsServiceProxy: ClassScheduleResultsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    classScheduleModeSpaceId: any;

    show(classScheduleModeSpaceId: any, classScheduleResultId?: number): void {
        this.classScheduleModeSpaceId = classScheduleModeSpaceId;

        if (!classScheduleResultId) {
            this.classScheduleResult = new CreateOrEditClassScheduleResultDto();
            this.classScheduleResult.id = classScheduleResultId;

            this.active = true;
            this.modal.show();
        } else {
            this._classScheduleResultsServiceProxy
                .getClassScheduleResultForEdit(classScheduleResultId)
                .subscribe((result) => {
                    this.classScheduleResult = result.classScheduleResult;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this.classScheduleResult.classScheduleModeSpaceId = this.classScheduleModeSpaceId;

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

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
