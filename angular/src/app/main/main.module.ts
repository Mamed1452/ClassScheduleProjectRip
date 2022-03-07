import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { LessonsOfUniversityProfessorsComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/lessonsOfUniversityProfessors.component';
import { ViewLessonsOfUniversityProfessorModalComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/view-lessonsOfUniversityProfessor-modal.component';
import { CreateOrEditLessonsOfUniversityProfessorModalComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/create-or-edit-lessonsOfUniversityProfessor-modal.component';
import { LessonsOfUniversityProfessorLessonLookupTableModalComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/lessonsOfUniversityProfessor-lesson-lookup-table-modal.component';
import { LessonsOfUniversityProfessorUniversityProfessorLookupTableModalComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/lessonsOfUniversityProfessor-universityProfessor-lookup-table-modal.component';

import { LessonsOfSemestersComponent } from './lessonsOfSemesters/lessonsOfSemesters/lessonsOfSemesters.component';
import { ViewLessonsOfSemesterModalComponent } from './lessonsOfSemesters/lessonsOfSemesters/view-lessonsOfSemester-modal.component';
import { CreateOrEditLessonsOfSemesterModalComponent } from './lessonsOfSemesters/lessonsOfSemesters/create-or-edit-lessonsOfSemester-modal.component';
import { LessonsOfSemesterLessonLookupTableModalComponent } from './lessonsOfSemesters/lessonsOfSemesters/lessonsOfSemester-lesson-lookup-table-modal.component';
import { LessonsOfSemesterSemesterLookupTableModalComponent } from './lessonsOfSemesters/lessonsOfSemesters/lessonsOfSemester-semester-lookup-table-modal.component';

import { LessonsComponent } from './lessons/lessons/lessons.component';
import { ViewLessonModalComponent } from './lessons/lessons/view-lesson-modal.component';
import { CreateOrEditLessonModalComponent } from './lessons/lessons/create-or-edit-lesson-modal.component';
import { LessonClassroomBuildingLookupTableModalComponent } from './lessons/lessons/lesson-classroomBuilding-lookup-table-modal.component';

import { UniversityProfessorWorkingTimesComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/universityProfessorWorkingTimes.component';
import { ViewUniversityProfessorWorkingTimeModalComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/view-universityProfessorWorkingTime-modal.component';
import { CreateOrEditUniversityProfessorWorkingTimeModalComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/create-or-edit-universityProfessorWorkingTime-modal.component';
import { UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/universityProfessorWorkingTime-universityProfessor-lookup-table-modal.component';
import { UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/universityProfessorWorkingTime-workTimeInDay-lookup-table-modal.component';

import { UniversityProfessorsComponent } from './universityProfessors/universityProfessors/universityProfessors.component';
import { ViewUniversityProfessorModalComponent } from './universityProfessors/universityProfessors/view-universityProfessor-modal.component';
import { CreateOrEditUniversityProfessorModalComponent } from './universityProfessors/universityProfessors/create-or-edit-universityProfessor-modal.component';

import { WorkTimeInDaysComponent } from './workTimeInDays/workTimeInDays/workTimeInDays.component';
import { ViewWorkTimeInDayModalComponent } from './workTimeInDays/workTimeInDays/view-workTimeInDay-modal.component';
import { CreateOrEditWorkTimeInDayModalComponent } from './workTimeInDays/workTimeInDays/create-or-edit-workTimeInDay-modal.component';

import { AssigningGradeToUniversityMajorsComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/assigningGradeToUniversityMajors.component';
import { ViewAssigningGradeToUniversityMajorModalComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/view-assigningGradeToUniversityMajor-modal.component';
import { CreateOrEditAssigningGradeToUniversityMajorModalComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/create-or-edit-assigningGradeToUniversityMajor-modal.component';
import { AssigningGradeToUniversityMajorGradeLookupTableModalComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/assigningGradeToUniversityMajor-grade-lookup-table-modal.component';
import { AssigningGradeToUniversityMajorUniversityMajorLookupTableModalComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/assigningGradeToUniversityMajor-universityMajor-lookup-table-modal.component';

import { MasterDetailChild_AssigningGradeToUniversityMajor_SemestersComponent } from './semesters/semesters/masterDetailChild_AssigningGradeToUniversityMajor_semesters.component';
import { MasterDetailChild_AssigningGradeToUniversityMajor_ViewSemesterModalComponent } from './semesters/semesters/masterDetailChild_AssigningGradeToUniversityMajor_view-semester-modal.component';
import { MasterDetailChild_AssigningGradeToUniversityMajor_CreateOrEditSemesterModalComponent } from './semesters/semesters/masterDetailChild_AssigningGradeToUniversityMajor_create-or-edit-semester-modal.component';

import { SemesterAssigningGradeToUniversityMajorLookupTableModalComponent } from './semesters/semesters/semester-assigningGradeToUniversityMajor-lookup-table-modal.component';

import { SemestersComponent } from './semesters/semesters/semesters.component';
import { ViewSemesterModalComponent } from './semesters/semesters/view-semester-modal.component';
import { CreateOrEditSemesterModalComponent } from './semesters/semesters/create-or-edit-semester-modal.component';

import { GradesComponent } from './grades/grades/grades.component';
import { ViewGradeModalComponent } from './grades/grades/view-grade-modal.component';
import { CreateOrEditGradeModalComponent } from './grades/grades/create-or-edit-grade-modal.component';

import { AssigningUniversityMajorToClassroomBuildingsComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings.component';
import { ViewAssigningUniversityMajorToClassroomBuildingModalComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/view-assigningUniversityMajorToClassroomBuilding-modal.component';
import { CreateOrEditAssigningUniversityMajorToClassroomBuildingModalComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/create-or-edit-assigningUniversityMajorToClassroomBuilding-modal.component';
import { AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModalComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuilding-universityMajor-lookup-table-modal.component';
import { AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModalComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuilding-classroomBuilding-lookup-table-modal.component';

import { UniversityDepartmentsComponent } from './universityDepartments/universityDepartments/universityDepartments.component';
import { ViewUniversityDepartmentModalComponent } from './universityDepartments/universityDepartments/view-universityDepartment-modal.component';
import { CreateOrEditUniversityDepartmentModalComponent } from './universityDepartments/universityDepartments/create-or-edit-universityDepartment-modal.component';

import { MasterDetailChild_UniversityDepartment_UniversityMajorsComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_universityMajors.component';
import { MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_view-universityMajor-modal.component';
import { MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_create-or-edit-universityMajor-modal.component';
import { MasterDetailChild_UniversityDepartment_UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_universityMajor-universityDepartment-lookup-table-modal.component';

import { UniversityMajorsComponent } from './universityMajors/universityMajors/universityMajors.component';
import { ViewUniversityMajorModalComponent } from './universityMajors/universityMajors/view-universityMajor-modal.component';
import { CreateOrEditUniversityMajorModalComponent } from './universityMajors/universityMajors/create-or-edit-universityMajor-modal.component';
import { UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajors/universityMajors/universityMajor-universityDepartment-lookup-table-modal.component';


import { ClassroomBuildingsComponent } from './classroomBuildings/classroomBuildings/classroomBuildings.component';
import { ViewClassroomBuildingModalComponent } from './classroomBuildings/classroomBuildings/view-classroomBuilding-modal.component';
import { CreateOrEditClassroomBuildingModalComponent } from './classroomBuildings/classroomBuildings/create-or-edit-classroomBuilding-modal.component';


import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';

import { BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,
        TableModule,

        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot()
    ],
    declarations: [
		LessonsOfUniversityProfessorsComponent,

		ViewLessonsOfUniversityProfessorModalComponent,
		CreateOrEditLessonsOfUniversityProfessorModalComponent,
    LessonsOfUniversityProfessorLessonLookupTableModalComponent,
    LessonsOfUniversityProfessorUniversityProfessorLookupTableModalComponent,
		LessonsOfSemestersComponent,

		ViewLessonsOfSemesterModalComponent,
		CreateOrEditLessonsOfSemesterModalComponent,
    LessonsOfSemesterLessonLookupTableModalComponent,
    LessonsOfSemesterSemesterLookupTableModalComponent,
		LessonsComponent,

		ViewLessonModalComponent,
		CreateOrEditLessonModalComponent,
    LessonClassroomBuildingLookupTableModalComponent,
		UniversityProfessorWorkingTimesComponent,

		ViewUniversityProfessorWorkingTimeModalComponent,
		CreateOrEditUniversityProfessorWorkingTimeModalComponent,
    UniversityProfessorWorkingTimeUniversityProfessorLookupTableModalComponent,
    UniversityProfessorWorkingTimeWorkTimeInDayLookupTableModalComponent,
		UniversityProfessorsComponent,

		ViewUniversityProfessorModalComponent,
		CreateOrEditUniversityProfessorModalComponent,
		WorkTimeInDaysComponent,

		ViewWorkTimeInDayModalComponent,
		CreateOrEditWorkTimeInDayModalComponent,
		AssigningGradeToUniversityMajorsComponent,

		ViewAssigningGradeToUniversityMajorModalComponent,
		CreateOrEditAssigningGradeToUniversityMajorModalComponent,
    AssigningGradeToUniversityMajorGradeLookupTableModalComponent,
    AssigningGradeToUniversityMajorUniversityMajorLookupTableModalComponent,
		MasterDetailChild_AssigningGradeToUniversityMajor_SemestersComponent,

		MasterDetailChild_AssigningGradeToUniversityMajor_ViewSemesterModalComponent,
		MasterDetailChild_AssigningGradeToUniversityMajor_CreateOrEditSemesterModalComponent,
    SemesterAssigningGradeToUniversityMajorLookupTableModalComponent,
		SemestersComponent,

		ViewSemesterModalComponent,
		CreateOrEditSemesterModalComponent,
		GradesComponent,

		ViewGradeModalComponent,
		CreateOrEditGradeModalComponent,
		AssigningUniversityMajorToClassroomBuildingsComponent,

		ViewAssigningUniversityMajorToClassroomBuildingModalComponent,
		CreateOrEditAssigningUniversityMajorToClassroomBuildingModalComponent,
    AssigningUniversityMajorToClassroomBuildingUniversityMajorLookupTableModalComponent,
    AssigningUniversityMajorToClassroomBuildingClassroomBuildingLookupTableModalComponent,
		UniversityDepartmentsComponent,
		ViewUniversityDepartmentModalComponent,
		CreateOrEditUniversityDepartmentModalComponent,
		MasterDetailChild_UniversityDepartment_UniversityMajorsComponent,
		MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent,
		MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent,
    MasterDetailChild_UniversityDepartment_UniversityMajorUniversityDepartmentLookupTableModalComponent,
		UniversityMajorsComponent,
		ViewUniversityMajorModalComponent,
		CreateOrEditUniversityMajorModalComponent,
    UniversityMajorUniversityDepartmentLookupTableModalComponent,
		ClassroomBuildingsComponent,
		ViewClassroomBuildingModalComponent,
		CreateOrEditClassroomBuildingModalComponent,
        DashboardComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
