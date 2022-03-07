import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { LessonsServiceProxy, CreateOrEditLessonDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { LessonClassroomBuildingLookupTableModalComponent } from './lesson-classroomBuilding-lookup-table-modal.component';

@Component({
    selector: 'createOrEditLessonModal',
    templateUrl: './create-or-edit-lesson-modal.component.html',
})
export class CreateOrEditLessonModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('lessonClassroomBuildingLookupTableModal', { static: true })
    lessonClassroomBuildingLookupTableModal: LessonClassroomBuildingLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    lesson: CreateOrEditLessonDto = new CreateOrEditLessonDto();

    classroomBuildingClassroomBuildingName = '';

    constructor(
        injector: Injector,
        private _lessonsServiceProxy: LessonsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(lessonId?: number): void {
        if (!lessonId) {
            this.lesson = new CreateOrEditLessonDto();
            this.lesson.id = lessonId;
            this.classroomBuildingClassroomBuildingName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._lessonsServiceProxy.getLessonForEdit(lessonId).subscribe((result) => {
                this.lesson = result.lesson;

                this.classroomBuildingClassroomBuildingName = result.classroomBuildingClassroomBuildingName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._lessonsServiceProxy
            .createOrEdit(this.lesson)
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

    openSelectClassroomBuildingModal() {
        this.lessonClassroomBuildingLookupTableModal.id = this.lesson.classroomBuildingId;
        this.lessonClassroomBuildingLookupTableModal.displayName = this.classroomBuildingClassroomBuildingName;
        this.lessonClassroomBuildingLookupTableModal.show();
    }

    setClassroomBuildingIdNull() {
        this.lesson.classroomBuildingId = null;
        this.classroomBuildingClassroomBuildingName = '';
    }

    getNewClassroomBuildingId() {
        this.lesson.classroomBuildingId = this.lessonClassroomBuildingLookupTableModal.id;
        this.classroomBuildingClassroomBuildingName = this.lessonClassroomBuildingLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
