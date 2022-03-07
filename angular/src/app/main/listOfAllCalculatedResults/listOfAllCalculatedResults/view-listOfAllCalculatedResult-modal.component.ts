import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
    GetListOfAllCalculatedResultForViewDto,
    ListOfAllCalculatedResultDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewListOfAllCalculatedResultModal',
    templateUrl: './view-listOfAllCalculatedResult-modal.component.html',
})
export class ViewListOfAllCalculatedResultModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetListOfAllCalculatedResultForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetListOfAllCalculatedResultForViewDto();
        this.item.listOfAllCalculatedResult = new ListOfAllCalculatedResultDto();
    }

    show(item: GetListOfAllCalculatedResultForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
