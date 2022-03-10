import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    LessonsOfSemestersServiceProxy,
    CreateOrEditLessonsOfSemesterDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { LessonsOfSemesterLessonLookupTableModalComponent } from './lessonsOfSemester-lesson-lookup-table-modal.component';
import { LessonsOfSemesterSemesterLookupTableModalComponent } from './lessonsOfSemester-semester-lookup-table-modal.component';

@Component({
    selector: 'createOrEditLessonsOfSemesterModal',
    templateUrl: './create-or-edit-lessonsOfSemester-modal.component.html',
})
export class CreateOrEditLessonsOfSemesterModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('lessonsOfSemesterLessonLookupTableModal', { static: true })
    lessonsOfSemesterLessonLookupTableModal: LessonsOfSemesterLessonLookupTableModalComponent;
    @ViewChild('lessonsOfSemesterSemesterLookupTableModal', { static: true })
    lessonsOfSemesterSemesterLookupTableModal: LessonsOfSemesterSemesterLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    lessonsOfSemester: CreateOrEditLessonsOfSemesterDto = new CreateOrEditLessonsOfSemesterDto();

    lessonNameLesson = '';
    semesterSemesterName = '';

    constructor(
        injector: Injector,
        private _lessonsOfSemestersServiceProxy: LessonsOfSemestersServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(lessonsOfSemesterId?: number): void {
        if (!lessonsOfSemesterId) {
            this.lessonsOfSemester = new CreateOrEditLessonsOfSemesterDto();
            this.lessonsOfSemester.id = lessonsOfSemesterId;
            this.lessonNameLesson = '';
            this.semesterSemesterName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._lessonsOfSemestersServiceProxy
                .getLessonsOfSemesterForEdit(lessonsOfSemesterId)
                .subscribe((result) => {
                    this.lessonsOfSemester = result.lessonsOfSemester;

                    this.lessonNameLesson = result.lessonNameLesson;
                    this.semesterSemesterName = result.semesterSemesterName;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._lessonsOfSemestersServiceProxy
            .createOrEdit(this.lessonsOfSemester)
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

    openSelectLessonModal() {
        this.lessonsOfSemesterLessonLookupTableModal.id = this.lessonsOfSemester.lessonId;
        this.lessonsOfSemesterLessonLookupTableModal.displayName = this.lessonNameLesson;
        this.lessonsOfSemesterLessonLookupTableModal.show();
    }
    openSelectSemesterModal() {
        this.lessonsOfSemesterSemesterLookupTableModal.id = this.lessonsOfSemester.semesterId;
        this.lessonsOfSemesterSemesterLookupTableModal.displayName = this.semesterSemesterName;
        this.lessonsOfSemesterSemesterLookupTableModal.show();
    }

    setLessonIdNull() {
        this.lessonsOfSemester.lessonId = null;
        this.lessonNameLesson = '';
        this.lessonsOfSemester.lessonsOfSemesterName= this.l('lesson') + " " + this.lessonNameLesson +" "+ this.l('for') + " " +  this.semesterSemesterName;
    }
    setSemesterIdNull() {
        this.lessonsOfSemester.semesterId = null;
        this.semesterSemesterName = '';
        this.lessonsOfSemester.lessonsOfSemesterName= this.l('lesson') + " " + this.lessonNameLesson +" "+ this.l('for') + " " +  this.semesterSemesterName;
    }

    getNewLessonId() {
        this.lessonsOfSemester.lessonId = this.lessonsOfSemesterLessonLookupTableModal.id;
        this.lessonNameLesson = this.lessonsOfSemesterLessonLookupTableModal.displayName;
        this.lessonsOfSemester.lessonsOfSemesterName= this.l('lesson') + " " + this.lessonNameLesson +" "+ this.l('for') + " " +  this.semesterSemesterName;
    }
    getNewSemesterId() {
        this.lessonsOfSemester.semesterId = this.lessonsOfSemesterSemesterLookupTableModal.id;
        this.semesterSemesterName = this.lessonsOfSemesterSemesterLookupTableModal.displayName;
        this.lessonsOfSemester.lessonsOfSemesterName= this.l('lesson') + " " + this.lessonNameLesson +" "+ this.l('for') + " " +  this.semesterSemesterName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
