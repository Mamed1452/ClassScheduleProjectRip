import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    ListOfMainDomainsServiceProxy,
    CreateOrEditListOfMainDomainDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditListOfMainDomainModal',
    templateUrl: './create-or-edit-listOfMainDomain-modal.component.html',
})
export class CreateOrEditListOfMainDomainModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    listOfMainDomain: CreateOrEditListOfMainDomainDto = new CreateOrEditListOfMainDomainDto();

    constructor(
        injector: Injector,
        private _listOfMainDomainsServiceProxy: ListOfMainDomainsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(listOfMainDomainId?: number): void {
        if (!listOfMainDomainId) {
            this.listOfMainDomain = new CreateOrEditListOfMainDomainDto();
            this.listOfMainDomain.id = listOfMainDomainId;

            this.active = true;
            this.modal.show();
        } else {
            this._listOfMainDomainsServiceProxy.getListOfMainDomainForEdit(listOfMainDomainId).subscribe((result) => {
                this.listOfMainDomain = result.listOfMainDomain;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._listOfMainDomainsServiceProxy
            .createOrEdit(this.listOfMainDomain)
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
