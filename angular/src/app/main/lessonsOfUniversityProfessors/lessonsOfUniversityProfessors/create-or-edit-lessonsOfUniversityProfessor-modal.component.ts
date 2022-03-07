import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    LessonsOfUniversityProfessorsServiceProxy,
    CreateOrEditLessonsOfUniversityProfessorDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { LessonsOfUniversityProfessorLessonLookupTableModalComponent } from './lessonsOfUniversityProfessor-lesson-lookup-table-modal.component';
import { LessonsOfUniversityProfessorUniversityProfessorLookupTableModalComponent } from './lessonsOfUniversityProfessor-universityProfessor-lookup-table-modal.component';

@Component({
    selector: 'createOrEditLessonsOfUniversityProfessorModal',
    templateUrl: './create-or-edit-lessonsOfUniversityProfessor-modal.component.html',
})
export class CreateOrEditLessonsOfUniversityProfessorModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('lessonsOfUniversityProfessorLessonLookupTableModal', { static: true })
    lessonsOfUniversityProfessorLessonLookupTableModal: LessonsOfUniversityProfessorLessonLookupTableModalComponent;
    @ViewChild('lessonsOfUniversityProfessorUniversityProfessorLookupTableModal', { static: true })
    lessonsOfUniversityProfessorUniversityProfessorLookupTableModal: LessonsOfUniversityProfessorUniversityProfessorLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    lessonsOfUniversityProfessor: CreateOrEditLessonsOfUniversityProfessorDto = new CreateOrEditLessonsOfUniversityProfessorDto();

    lessonNameLesson = '';
    universityProfessorUniversityProfessorName = '';

    constructor(
        injector: Injector,
        private _lessonsOfUniversityProfessorsServiceProxy: LessonsOfUniversityProfessorsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(lessonsOfUniversityProfessorId?: number): void {
        if (!lessonsOfUniversityProfessorId) {
            this.lessonsOfUniversityProfessor = new CreateOrEditLessonsOfUniversityProfessorDto();
            this.lessonsOfUniversityProfessor.id = lessonsOfUniversityProfessorId;
            this.lessonNameLesson = '';
            this.universityProfessorUniversityProfessorName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._lessonsOfUniversityProfessorsServiceProxy
                .getLessonsOfUniversityProfessorForEdit(lessonsOfUniversityProfessorId)
                .subscribe((result) => {
                    this.lessonsOfUniversityProfessor = result.lessonsOfUniversityProfessor;

                    this.lessonNameLesson = result.lessonNameLesson;
                    this.universityProfessorUniversityProfessorName = result.universityProfessorUniversityProfessorName;

                    this.active = true;
                    this.modal.show();
                });
        }
    }

    save(): void {
        this.saving = true;

        this._lessonsOfUniversityProfessorsServiceProxy
            .createOrEdit(this.lessonsOfUniversityProfessor)
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
        this.lessonsOfUniversityProfessorLessonLookupTableModal.id = this.lessonsOfUniversityProfessor.lessonId;
        this.lessonsOfUniversityProfessorLessonLookupTableModal.displayName = this.lessonNameLesson;
        this.lessonsOfUniversityProfessorLessonLookupTableModal.show();
    }
    openSelectUniversityProfessorModal() {
        this.lessonsOfUniversityProfessorUniversityProfessorLookupTableModal.id = this.lessonsOfUniversityProfessor.universityProfessorId;
        this.lessonsOfUniversityProfessorUniversityProfessorLookupTableModal.displayName = this.universityProfessorUniversityProfessorName;
        this.lessonsOfUniversityProfessorUniversityProfessorLookupTableModal.show();
    }

    setLessonIdNull() {
        this.lessonsOfUniversityProfessor.lessonId = null;
        this.lessonNameLesson = '';
    }
    setUniversityProfessorIdNull() {
        this.lessonsOfUniversityProfessor.universityProfessorId = null;
        this.universityProfessorUniversityProfessorName = '';
    }

    getNewLessonId() {
        this.lessonsOfUniversityProfessor.lessonId = this.lessonsOfUniversityProfessorLessonLookupTableModal.id;
        this.lessonNameLesson = this.lessonsOfUniversityProfessorLessonLookupTableModal.displayName;
    }
    getNewUniversityProfessorId() {
        this.lessonsOfUniversityProfessor.universityProfessorId = this.lessonsOfUniversityProfessorUniversityProfessorLookupTableModal.id;
        this.universityProfessorUniversityProfessorName = this.lessonsOfUniversityProfessorUniversityProfessorLookupTableModal.displayName;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
