import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    ClassScheduleModeSpacesServiceProxy,
    CreateOrEditClassScheduleModeSpaceDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent } from './classScheduleModeSpace-universityProfessor-lookup-table-modal.component';
import { ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent } from './classScheduleModeSpace-workTimeInDay-lookup-table-modal.component';
import { ClassScheduleModeSpaceLessonLookupTableModalComponent } from './classScheduleModeSpace-lesson-lookup-table-modal.component';

@Component({
    selector: 'createOrEditClassScheduleModeSpaceModal',
    templateUrl: './create-or-edit-classScheduleModeSpace-modal.component.html',
})
export class CreateOrEditClassScheduleModeSpaceModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('classScheduleModeSpaceUniversityProfessorLookupTableModal', { static: true })
    classScheduleModeSpaceUniversityProfessorLookupTableModal: ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent;
    @ViewChild('classScheduleModeSpaceWorkTimeInDayLookupTableModal', { static: true })
    classScheduleModeSpaceWorkTimeInDayLookupTableModal: ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent;
    @ViewChild('classScheduleModeSpaceLessonLookupTableModal', { static: true })
    classScheduleModeSpaceLessonLookupTableModal: ClassScheduleModeSpaceLessonLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    classScheduleModeSpace: CreateOrEditClassScheduleModeSpaceDto = new CreateOrEditClassScheduleModeSpaceDto();

    universityProfessorUniversityProfessorName = '';
    workTimeInDayNameWorkTimeInDay = '';
    lessonNameLesson = '';

    constructor(
        injector: Injector,
        private _classScheduleModeSpacesServiceProxy: ClassScheduleModeSpacesServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(classScheduleModeSpaceId?: number): void {
        if (!classScheduleModeSpaceId) {
            this.classScheduleModeSpace = new CreateOrEditClassScheduleModeSpaceDto();
            this.classScheduleModeSpace.id = classScheduleModeSpaceId;
            this.universityProfessorUniversityProfessorName = '';
            this.workTimeInDayNameWorkTimeInDay = '';
            this.lessonNameLesson = '';

            this.active = true;
            this.modal.show();
        } else {
            this._classScheduleModeSpacesServiceProxy
                .getClassScheduleModeSpaceForEdit(classScheduleModeSpaceId)
                .subscribe((result) => {
                    this.classScheduleModeSpace = result.classScheduleModeSpace;

                    this.universityProfessorUniversityProfessorName = result.universityProfessorUniversityProfessorName;
                    this.workTimeInDayNameWorkTimeInDay = result.workTimeInDayNameWorkTimeInDay;
                    this.lessonNameLesson = result.lessonNameLesson;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._classScheduleModeSpacesServiceProxy
            .createOrEdit(this.classScheduleModeSpace)
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

    openSelectUniversityProfessorModal() {
        this.classScheduleModeSpaceUniversityProfessorLookupTableModal.id = this.classScheduleModeSpace.universityProfessorId;
        this.classScheduleModeSpaceUniversityProfessorLookupTableModal.displayName = this.universityProfessorUniversityProfessorName;
        this.classScheduleModeSpaceUniversityProfessorLookupTableModal.show();
    }
    openSelectWorkTimeInDayModal() {
        this.classScheduleModeSpaceWorkTimeInDayLookupTableModal.id = this.classScheduleModeSpace.workTimeInDayId;
        this.classScheduleModeSpaceWorkTimeInDayLookupTableModal.displayName = this.workTimeInDayNameWorkTimeInDay;
        this.classScheduleModeSpaceWorkTimeInDayLookupTableModal.show();
    }
    openSelectLessonModal() {
        this.classScheduleModeSpaceLessonLookupTableModal.id = this.classScheduleModeSpace.lessonId;
        this.classScheduleModeSpaceLessonLookupTableModal.displayName = this.lessonNameLesson;
        this.classScheduleModeSpaceLessonLookupTableModal.show();
    }

    setUniversityProfessorIdNull() {
        this.classScheduleModeSpace.universityProfessorId = null;
        this.universityProfessorUniversityProfessorName = '';
    }
    setWorkTimeInDayIdNull() {
        this.classScheduleModeSpace.workTimeInDayId = null;
        this.workTimeInDayNameWorkTimeInDay = '';
    }
    setLessonIdNull() {
        this.classScheduleModeSpace.lessonId = null;
        this.lessonNameLesson = '';
    }

    getNewUniversityProfessorId() {
        this.classScheduleModeSpace.universityProfessorId = this.classScheduleModeSpaceUniversityProfessorLookupTableModal.id;
        this.universityProfessorUniversityProfessorName = this.classScheduleModeSpaceUniversityProfessorLookupTableModal.displayName;
    }
    getNewWorkTimeInDayId() {
        this.classScheduleModeSpace.workTimeInDayId = this.classScheduleModeSpaceWorkTimeInDayLookupTableModal.id;
        this.workTimeInDayNameWorkTimeInDay = this.classScheduleModeSpaceWorkTimeInDayLookupTableModal.displayName;
    }
    getNewLessonId() {
        this.classScheduleModeSpace.lessonId = this.classScheduleModeSpaceLessonLookupTableModal.id;
        this.lessonNameLesson = this.classScheduleModeSpaceLessonLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
