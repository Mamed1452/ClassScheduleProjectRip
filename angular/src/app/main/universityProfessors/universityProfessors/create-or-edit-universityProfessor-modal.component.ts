import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    UniversityProfessorsServiceProxy,
    CreateOrEditUniversityProfessorDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditUniversityProfessorModal',
    templateUrl: './create-or-edit-universityProfessor-modal.component.html',
})
export class CreateOrEditUniversityProfessorModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    universityProfessor: CreateOrEditUniversityProfessorDto = new CreateOrEditUniversityProfessorDto();

    constructor(
        injector: Injector,
        private _universityProfessorsServiceProxy: UniversityProfessorsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(universityProfessorId?: number): void {
        if (!universityProfessorId) {
            this.universityProfessor = new CreateOrEditUniversityProfessorDto();
            this.universityProfessor.id = universityProfessorId;

            this.active = true;
            this.modal.show();
        } else {
            this._universityProfessorsServiceProxy
                .getUniversityProfessorForEdit(universityProfessorId)
                .subscribe((result) => {
                    this.universityProfessor = result.universityProfessor;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._universityProfessorsServiceProxy
            .createOrEdit(this.universityProfessor)
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
