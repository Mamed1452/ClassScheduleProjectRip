import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { MainDomainsServiceProxy, CreateOrEditMainDomainDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { MasterDetailChild_ListOfMainDomain_MainDomainLessonsOfSemesterLookupTableModalComponent } from './masterDetailChild_ListOfMainDomain_mainDomain-lessonsOfSemester-lookup-table-modal.component';

@Component({
    selector: 'masterDetailChild_ListOfMainDomain_createOrEditMainDomainModal',
    templateUrl: './masterDetailChild_ListOfMainDomain_create-or-edit-mainDomain-modal.component.html',
})
export class MasterDetailChild_ListOfMainDomain_CreateOrEditMainDomainModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('mainDomainLessonsOfSemesterLookupTableModal', { static: true })
    mainDomainLessonsOfSemesterLookupTableModal: MasterDetailChild_ListOfMainDomain_MainDomainLessonsOfSemesterLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    mainDomain: CreateOrEditMainDomainDto = new CreateOrEditMainDomainDto();

    lessonsOfSemesterLessonsOfSemesterName = '';

    constructor(
        injector: Injector,
        private _mainDomainsServiceProxy: MainDomainsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    listOfMainDomainId: any;

    show(listOfMainDomainId: any, mainDomainId?: number): void {
        this.listOfMainDomainId = listOfMainDomainId;

        if (!mainDomainId) {
            this.mainDomain = new CreateOrEditMainDomainDto();
            this.mainDomain.id = mainDomainId;
            this.lessonsOfSemesterLessonsOfSemesterName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._mainDomainsServiceProxy.getMainDomainForEdit(mainDomainId).subscribe((result) => {
                this.mainDomain = result.mainDomain;

                this.lessonsOfSemesterLessonsOfSemesterName = result.lessonsOfSemesterLessonsOfSemesterName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this.mainDomain.listOfMainDomainId = this.listOfMainDomainId;

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

    openSelectLessonsOfSemesterModal() {
        this.mainDomainLessonsOfSemesterLookupTableModal.id = this.mainDomain.lessonsOfSemesterId;
        this.mainDomainLessonsOfSemesterLookupTableModal.displayName = this.lessonsOfSemesterLessonsOfSemesterName;
        this.mainDomainLessonsOfSemesterLookupTableModal.show();
    }

    setLessonsOfSemesterIdNull() {
        this.mainDomain.lessonsOfSemesterId = null;
        this.lessonsOfSemesterLessonsOfSemesterName = '';
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
