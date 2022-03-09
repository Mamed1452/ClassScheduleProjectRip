import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ClassScheduleModeSpacesServiceProxy, CreateOrEditClassScheduleModeSpaceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent } from './masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-universityProfessor-lookup-table-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent } from './masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-workTimeInDay-lookup-table-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceLessonLookupTableModalComponent } from './masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-lesson-lookup-table-modal.component';



@Component({
    selector: 'masterDetailChild_ListOfClassScheduleModeSpace_createOrEditClassScheduleModeSpaceModal',
    templateUrl: './masterDetailChild_ListOfClassScheduleModeSpace_create-or-edit-classScheduleModeSpace-modal.component.html'
})
export class MasterDetailChild_ListOfClassScheduleModeSpace_CreateOrEditClassScheduleModeSpaceModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('classScheduleModeSpaceUniversityProfessorLookupTableModal', { static: true }) classScheduleModeSpaceUniversityProfessorLookupTableModal: MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent;
    @ViewChild('classScheduleModeSpaceWorkTimeInDayLookupTableModal', { static: true }) classScheduleModeSpaceWorkTimeInDayLookupTableModal: MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent;
    @ViewChild('classScheduleModeSpaceLessonLookupTableModal', { static: true }) classScheduleModeSpaceLessonLookupTableModal: MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceLessonLookupTableModalComponent;

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
    
                 listOfClassScheduleModeSpaceId: any;
             
    show(
                 listOfClassScheduleModeSpaceId: any,
             classScheduleModeSpaceId?: number): void {
    
                 this.listOfClassScheduleModeSpaceId = listOfClassScheduleModeSpaceId;
             

        if (!classScheduleModeSpaceId) {
            this.classScheduleModeSpace = new CreateOrEditClassScheduleModeSpaceDto();
            this.classScheduleModeSpace.id = classScheduleModeSpaceId;
            this.universityProfessorUniversityProfessorName = '';
            this.workTimeInDayNameWorkTimeInDay = '';
            this.lessonNameLesson = '';


            this.active = true;
            this.modal.show();
        } else {
            this._classScheduleModeSpacesServiceProxy.getClassScheduleModeSpaceForEdit(classScheduleModeSpaceId).subscribe(result => {
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
            
			
			
                this.classScheduleModeSpace.listOfClassScheduleModeSpaceId = this.listOfClassScheduleModeSpaceId;
            
            this._classScheduleModeSpacesServiceProxy.createOrEdit(this.classScheduleModeSpace)
             .pipe(finalize(() => { this.saving = false;}))
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
    
     ngOnInit(): void {
        
     }    
}
