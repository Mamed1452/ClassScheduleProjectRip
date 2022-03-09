import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { MainDomainsServiceProxy, CreateOrEditMainDomainDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { MainDomainListOfMainDomainLookupTableModalComponent } from './mainDomain-listOfMainDomain-lookup-table-modal.component';
import { MainDomainLessonsOfSemesterLookupTableModalComponent } from './mainDomain-lessonsOfSemester-lookup-table-modal.component';

@Component({
    selector: 'createOrEditMainDomainModal',
    templateUrl: './create-or-edit-mainDomain-modal.component.html',
})
export class CreateOrEditMainDomainModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('mainDomainListOfMainDomainLookupTableModal', { static: true })
    mainDomainListOfMainDomainLookupTableModal: MainDomainListOfMainDomainLookupTableModalComponent;
    @ViewChild('mainDomainLessonsOfSemesterLookupTableModal', { static: true })
    mainDomainLessonsOfSemesterLookupTableModal: MainDomainLessonsOfSemesterLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    mainDomain: CreateOrEditMainDomainDto = new CreateOrEditMainDomainDto();

    listOfMainDomainListOfMainDomainName = '';
    lessonsOfSemesterLessonsOfSemesterName = '';

    constructor(
        injector: Injector,
        private _mainDomainsServiceProxy: MainDomainsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(mainDomainId?: number): void {
        if (!mainDomainId) {
            this.mainDomain = new CreateOrEditMainDomainDto();
            this.mainDomain.id = mainDomainId;
            this.listOfMainDomainListOfMainDomainName = '';
            this.lessonsOfSemesterLessonsOfSemesterName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._mainDomainsServiceProxy.getMainDomainForEdit(mainDomainId).subscribe((result) => {
                this.mainDomain = result.mainDomain;

                this.listOfMainDomainListOfMainDomainName = result.listOfMainDomainListOfMainDomainName;
                this.lessonsOfSemesterLessonsOfSemesterName = result.lessonsOfSemesterLessonsOfSemesterName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._mainDomainsServiceProxy
            .createOrEdit(this.mainDomain)
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

    openSelectListOfMainDomainModal() {
        this.mainDomainListOfMainDomainLookupTableModal.id = this.mainDomain.listOfMainDomainId;
        this.mainDomainListOfMainDomainLookupTableModal.displayName = this.listOfMainDomainListOfMainDomainName;
        this.mainDomainListOfMainDomainLookupTableModal.show();
    }
    openSelectLessonsOfSemesterModal() {
        this.mainDomainLessonsOfSemesterLookupTableModal.id = this.mainDomain.lessonsOfSemesterId;
        this.mainDomainLessonsOfSemesterLookupTableModal.displayName = this.lessonsOfSemesterLessonsOfSemesterName;
        this.mainDomainLessonsOfSemesterLookupTableModal.show();
    }

    setListOfMainDomainIdNull() {
        this.mainDomain.listOfMainDomainId = null;
        this.listOfMainDomainListOfMainDomainName = '';
    }
    setLessonsOfSemesterIdNull() {
        this.mainDomain.lessonsOfSemesterId = null;
        this.lessonsOfSemesterLessonsOfSemesterName = '';
    }

    getNewListOfMainDomainId() {
        this.mainDomain.listOfMainDomainId = this.mainDomainListOfMainDomainLookupTableModal.id;
        this.listOfMainDomainListOfMainDomainName = this.mainDomainListOfMainDomainLookupTableModal.displayName;
    }
    getNewLessonsOfSemesterId() {
        this.mainDomain.lessonsOfSemesterId = this.mainDomainLessonsOfSemesterLookupTableModal.id;
        this.lessonsOfSemesterLessonsOfSemesterName = this.mainDomainLessonsOfSemesterLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
