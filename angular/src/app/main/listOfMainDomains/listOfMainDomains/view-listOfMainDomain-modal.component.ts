import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetListOfMainDomainForViewDto, ListOfMainDomainDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewListOfMainDomainModal',
    templateUrl: './view-listOfMainDomain-modal.component.html',
})
export class ViewListOfMainDomainModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetListOfMainDomainForViewDto;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetListOfMainDomainForViewDto();
        this.item.listOfMainDomain = new ListOfMainDomainDto();
    }

    show(item: GetListOfMainDomainForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
