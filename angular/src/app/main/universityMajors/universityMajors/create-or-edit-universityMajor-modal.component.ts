import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { UniversityMajorsServiceProxy, CreateOrEditUniversityMajorDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajor-universityDepartment-lookup-table-modal.component';

@Component({
    selector: 'createOrEditUniversityMajorModal',
    templateUrl: './create-or-edit-universityMajor-modal.component.html',
})
export class CreateOrEditUniversityMajorModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('universityMajorUniversityDepartmentLookupTableModal', { static: true })
    universityMajorUniversityDepartmentLookupTableModal: UniversityMajorUniversityDepartmentLookupTableModalComponent;
    @ViewChild('universityMajorUniversityDepartmentLookupTableModal2', { static: true })
    universityMajorUniversityDepartmentLookupTableModal2: UniversityMajorUniversityDepartmentLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityMajor: CreateOrEditUniversityMajorDto = new CreateOrEditUniversityMajorDto();

    universityDepartmentUniversityDepartmentName = '';
    universityDepartmentUniversityDepartmentName2 = '';

    constructor(
        injector: Injector,
        private _universityMajorsServiceProxy: UniversityMajorsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(universityMajorId?: number): void {
        if (!universityMajorId) {
            this.universityMajor = new CreateOrEditUniversityMajorDto();
            this.universityMajor.id = universityMajorId;
            this.universityDepartmentUniversityDepartmentName = '';
            this.universityDepartmentUniversityDepartmentName2 = '';

            this.active = true;
            this.modal.show();
        } else {
            this._universityMajorsServiceProxy.getUniversityMajorForEdit(universityMajorId).subscribe((result) => {
                this.universityMajor = result.universityMajor;

                this.universityDepartmentUniversityDepartmentName = result.universityDepartmentUniversityDepartmentName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._universityMajorsServiceProxy
            .createOrEdit(this.universityMajor)
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

    openSelectUniversityDepartmentModal() {
        this.universityMajorUniversityDepartmentLookupTableModal.id = this.universityMajor.universityDepartmentId;
        this.universityMajorUniversityDepartmentLookupTableModal.displayName = this.universityDepartmentUniversityDepartmentName;
        this.universityMajorUniversityDepartmentLookupTableModal.show();
    }
    openSelectUniversityDepartmentModal2() {
        this.universityMajorUniversityDepartmentLookupTableModal2.id = this.universityMajor.universityDepartmentId;
        this.universityMajorUniversityDepartmentLookupTableModal2.displayName = this.universityDepartmentUniversityDepartmentName;
        this.universityMajorUniversityDepartmentLookupTableModal2.show();
    }

    setUniversityDepartmentIdNull() {
        this.universityMajor.universityDepartmentId = null;
        this.universityDepartmentUniversityDepartmentName = '';
    }


    getNewUniversityDepartmentId() {
        this.universityMajor.universityDepartmentId = this.universityMajorUniversityDepartmentLookupTableModal.id;
        this.universityDepartmentUniversityDepartmentName = this.universityMajorUniversityDepartmentLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
