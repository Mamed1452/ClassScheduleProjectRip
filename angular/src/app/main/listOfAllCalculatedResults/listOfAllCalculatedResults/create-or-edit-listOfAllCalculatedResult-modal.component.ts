import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    ListOfAllCalculatedResultsServiceProxy,
    CreateOrEditListOfAllCalculatedResultDto,
    ClassScheduleResultsServiceProxy,
    StartClassScheduleInputDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditListOfAllCalculatedResultModal',
    templateUrl: './create-or-edit-listOfAllCalculatedResult-modal.component.html',
})
export class CreateOrEditListOfAllCalculatedResultModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving : boolean;

    listOfAllCalculatedResult: CreateOrEditListOfAllCalculatedResultDto = new CreateOrEditListOfAllCalculatedResultDto();
    resultDt : StartClassScheduleInputDto=new StartClassScheduleInputDto();
    constructor(
        injector: Injector,
        private _listOfAllCalculatedResultsServiceProxy: ListOfAllCalculatedResultsServiceProxy,
        private _classScheduleResultsServiceProxy: ClassScheduleResultsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(listOfAllCalculatedResultId?: number): void {
        this.saving=false;
        if (!listOfAllCalculatedResultId) {
            this.listOfAllCalculatedResult = new CreateOrEditListOfAllCalculatedResultDto();
            this.listOfAllCalculatedResult.id = listOfAllCalculatedResultId;
            this.resultDt =new StartClassScheduleInputDto();
            this.active = true;

            this.modal.show();
        } else {
            this._listOfAllCalculatedResultsServiceProxy
                .getListOfAllCalculatedResultForEdit(listOfAllCalculatedResultId)
                .subscribe((result) => {
                    this.listOfAllCalculatedResult = result.listOfAllCalculatedResult;
                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._listOfAllCalculatedResultsServiceProxy
            .createOrEdit(this.listOfAllCalculatedResult)
            .subscribe((res) => {
                if(res)
                {
                    if(res.isCreated==true)
                    {

                        this.resultDt.listOfAllCalculatedResultId=res.id;
                        this._classScheduleResultsServiceProxy.startClassSchedule(this.resultDt).subscribe((ResultOfSchedule)=>
                        {
                            if(ResultOfSchedule)
                            {
                                    if(ResultOfSchedule.isSuccsses==true)
                                    {
                                        this.notify.info(this.l('SavedSuccessfully'));
                                        this.saving=false;
                                        this.close();
                                        this.modalSave.emit(null);
                                    }
                                    else
                                    {
                                        this.saving=false;
                                        this.notify.info(this.l('NotSaved'));
                                    }

                            }

                        });
                    }
                    else
                    {

                        this.notify.info(this.l('SavedSuccessfully'));
                        this.close();
                        this.modalSave.emit(null);
                    }
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
