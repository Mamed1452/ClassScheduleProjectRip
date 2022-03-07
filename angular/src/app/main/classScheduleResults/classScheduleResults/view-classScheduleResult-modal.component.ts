import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetClassScheduleResultForViewDto, ClassScheduleResultDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewClassScheduleResultModal',
    templateUrl: './view-classScheduleResult-modal.component.html',
})
export class ViewClassScheduleResultModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetClassScheduleResultForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetClassScheduleResultForViewDto();
        this.item.classScheduleResult = new ClassScheduleResultDto();
    }

    show(item: GetClassScheduleResultForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
