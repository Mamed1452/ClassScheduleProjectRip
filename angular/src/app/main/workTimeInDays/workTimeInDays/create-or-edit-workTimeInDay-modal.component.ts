import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { WorkTimeInDaysServiceProxy, CreateOrEditWorkTimeInDayDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditWorkTimeInDayModal',
    templateUrl: './create-or-edit-workTimeInDay-modal.component.html',
})
export class CreateOrEditWorkTimeInDayModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    workTimeInDay: CreateOrEditWorkTimeInDayDto = new CreateOrEditWorkTimeInDayDto();

    constructor(
        injector: Injector,
        private _workTimeInDaysServiceProxy: WorkTimeInDaysServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(workTimeInDayId?: number): void {
        if (!workTimeInDayId) {
            this.workTimeInDay = new CreateOrEditWorkTimeInDayDto();
            this.workTimeInDay.id = workTimeInDayId;

            this.active = true;
            this.modal.show();
        } else {
            this._workTimeInDaysServiceProxy.getWorkTimeInDayForEdit(workTimeInDayId).subscribe((result) => {
                this.workTimeInDay = result.workTimeInDay;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._workTimeInDaysServiceProxy
            .createOrEdit(this.workTimeInDay)
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
