import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetWorkTimeInDayForViewDto, WorkTimeInDayDto, DayOfWeekEnum ,WorkTimeInDaysServiceProxy} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewWorkTimeInDayModal',
    templateUrl: './view-workTimeInDay-modal.component.html',
})
export class ViewWorkTimeInDayModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetWorkTimeInDayForViewDto;
    dayOfWeekEnum = DayOfWeekEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetWorkTimeInDayForViewDto();
        this.item.workTimeInDay = new WorkTimeInDayDto();
    }

    show(item: GetWorkTimeInDayForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
