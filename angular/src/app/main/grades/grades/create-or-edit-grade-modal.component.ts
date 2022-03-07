import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { GradesServiceProxy, CreateOrEditGradeDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditGradeModal',
    templateUrl: './create-or-edit-grade-modal.component.html',
})
export class CreateOrEditGradeModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    grade: CreateOrEditGradeDto = new CreateOrEditGradeDto();

    constructor(
        injector: Injector,
        private _gradesServiceProxy: GradesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(gradeId?: number): void {
        if (!gradeId) {
            this.grade = new CreateOrEditGradeDto();
            this.grade.id = gradeId;

            this.active = true;
            this.modal.show();
        } else {
            this._gradesServiceProxy.getGradeForEdit(gradeId).subscribe((result) => {
                this.grade = result.grade;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._gradesServiceProxy
            .createOrEdit(this.grade)
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
