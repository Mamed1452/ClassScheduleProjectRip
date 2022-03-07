import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { UniversityMajorsServiceProxy, CreateOrEditUniversityMajorDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'masterDetailChild_UniversityDepartment_createOrEditUniversityMajorModal',
    templateUrl: './masterDetailChild_UniversityDepartment_create-or-edit-universityMajor-modal.component.html',
})
export class MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent
    extends AppComponentBase
    implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityMajor: CreateOrEditUniversityMajorDto = new CreateOrEditUniversityMajorDto();

    constructor(
        injector: Injector,
        private _universityMajorsServiceProxy: UniversityMajorsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    universityDepartmentId: any;

    show(universityDepartmentId: any, universityMajorId?: number): void {
        this.universityDepartmentId = universityDepartmentId;

        if (!universityMajorId) {
            this.universityMajor = new CreateOrEditUniversityMajorDto();
            this.universityMajor.id = universityMajorId;

            this.active = true;
            this.modal.show();
        } else {
            this._universityMajorsServiceProxy.getUniversityMajorForEdit(universityMajorId).subscribe((result) => {
                this.universityMajor = result.universityMajor;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this.universityMajor.universityDepartmentId = this.universityDepartmentId;

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

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
