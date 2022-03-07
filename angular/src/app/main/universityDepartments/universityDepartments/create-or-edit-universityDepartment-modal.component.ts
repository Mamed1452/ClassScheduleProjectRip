import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    UniversityDepartmentsServiceProxy,
    CreateOrEditUniversityDepartmentDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditUniversityDepartmentModal',
    templateUrl: './create-or-edit-universityDepartment-modal.component.html',
})
export class CreateOrEditUniversityDepartmentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityDepartment: CreateOrEditUniversityDepartmentDto = new CreateOrEditUniversityDepartmentDto();

    constructor(
        injector: Injector,
        private _universityDepartmentsServiceProxy: UniversityDepartmentsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(universityDepartmentId?: number): void {
        if (!universityDepartmentId) {
            this.universityDepartment = new CreateOrEditUniversityDepartmentDto();
            this.universityDepartment.id = universityDepartmentId;

            this.active = true;
            this.modal.show();
        } else {
            this._universityDepartmentsServiceProxy
                .getUniversityDepartmentForEdit(universityDepartmentId)
                .subscribe((result) => {
                    this.universityDepartment = result.universityDepartment;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._universityDepartmentsServiceProxy
            .createOrEdit(this.universityDepartment)
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
