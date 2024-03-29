﻿import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { ListOfMainDomainsComponent } from './listOfMainDomains/listOfMainDomains/listOfMainDomains.component';
import { ViewListOfMainDomainModalComponent } from './listOfMainDomains/listOfMainDomains/view-listOfMainDomain-modal.component';
import { CreateOrEditListOfMainDomainModalComponent } from './listOfMainDomains/listOfMainDomains/create-or-edit-listOfMainDomain-modal.component';

import { MasterDetailChild_ListOfMainDomain_MainDomainsComponent } from './mainDomains/mainDomains/masterDetailChild_ListOfMainDomain_mainDomains.component';
import { MasterDetailChild_ListOfMainDomain_ViewMainDomainModalComponent } from './mainDomains/mainDomains/masterDetailChild_ListOfMainDomain_view-mainDomain-modal.component';
import { MasterDetailChild_ListOfMainDomain_CreateOrEditMainDomainModalComponent } from './mainDomains/mainDomains/masterDetailChild_ListOfMainDomain_create-or-edit-mainDomain-modal.component';
import { MasterDetailChild_ListOfMainDomain_MainDomainLessonsOfSemesterLookupTableModalComponent } from './mainDomains/mainDomains/masterDetailChild_ListOfMainDomain_mainDomain-lessonsOfSemester-lookup-table-modal.component';

import { MainDomainListOfMainDomainLookupTableModalComponent } from './mainDomains/mainDomains/mainDomain-listOfMainDomain-lookup-table-modal.component';

import { MainDomainsComponent } from './mainDomains/mainDomains/mainDomains.component';
import { ViewMainDomainModalComponent } from './mainDomains/mainDomains/view-mainDomain-modal.component';
import { CreateOrEditMainDomainModalComponent } from './mainDomains/mainDomains/create-or-edit-mainDomain-modal.component';
import { MainDomainLessonsOfSemesterLookupTableModalComponent } from './mainDomains/mainDomains/mainDomain-lessonsOfSemester-lookup-table-modal.component';

import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpacesComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpaces.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ViewClassScheduleModeSpaceModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_view-classScheduleModeSpace-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_CreateOrEditClassScheduleModeSpaceModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_create-or-edit-classScheduleModeSpace-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-universityProfessor-lookup-table-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-workTimeInDay-lookup-table-modal.component';
import { MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceLessonLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/masterDetailChild_ListOfClassScheduleModeSpace_classScheduleModeSpace-lesson-lookup-table-modal.component';

import { ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpace-listOfClassScheduleModeSpace-lookup-table-modal.component';

import { MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ClassScheduleModeSpace_classScheduleResult-listOfAllCalculatedResult-lookup-table-modal.component';
import { MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ClassScheduleModeSpace_classScheduleResult-classScheduleModeSpace-lookup-table-modal.component';

import { ListOfAllCalculatedResultsComponent } from './listOfAllCalculatedResults/listOfAllCalculatedResults/listOfAllCalculatedResults.component';
import { ViewListOfAllCalculatedResultModalComponent } from './listOfAllCalculatedResults/listOfAllCalculatedResults/view-listOfAllCalculatedResult-modal.component';
import { CreateOrEditListOfAllCalculatedResultModalComponent } from './listOfAllCalculatedResults/listOfAllCalculatedResults/create-or-edit-listOfAllCalculatedResult-modal.component';

import { MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultsComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ListOfAllCalculatedResult_classScheduleResults.component';
import { MasterDetailChild_ListOfAllCalculatedResult_ViewClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ListOfAllCalculatedResult_view-classScheduleResult-modal.component';
import { MasterDetailChild_ListOfAllCalculatedResult_CreateOrEditClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ListOfAllCalculatedResult_create-or-edit-classScheduleResult-modal.component';
import { MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ListOfAllCalculatedResult_classScheduleResult-classScheduleModeSpace-lookup-table-modal.component';

import { ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent } from './classScheduleResults/classScheduleResults/classScheduleResult-listOfAllCalculatedResult-lookup-table-modal.component';

import { ClassScheduleModeSpacesComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpaces.component';
import { ViewClassScheduleModeSpaceModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/view-classScheduleModeSpace-modal.component';
import { CreateOrEditClassScheduleModeSpaceModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/create-or-edit-classScheduleModeSpace-modal.component';
import { ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpace-universityProfessor-lookup-table-modal.component';
import { ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpace-workTimeInDay-lookup-table-modal.component';
import { ClassScheduleModeSpaceLessonLookupTableModalComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpace-lesson-lookup-table-modal.component';

import { MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultsComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ClassScheduleModeSpace_classScheduleResults.component';
import { MasterDetailChild_ClassScheduleModeSpace_ViewClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ClassScheduleModeSpace_view-classScheduleResult-modal.component';
import { MasterDetailChild_ClassScheduleModeSpace_CreateOrEditClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/masterDetailChild_ClassScheduleModeSpace_create-or-edit-classScheduleResult-modal.component';

import { ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent } from './classScheduleResults/classScheduleResults/classScheduleResult-classScheduleModeSpace-lookup-table-modal.component';

import { ClassScheduleResultsComponent } from './classScheduleResults/classScheduleResults/classScheduleResults.component';
import { ViewClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/view-classScheduleResult-modal.component';
import { CreateOrEditClassScheduleResultModalComponent } from './classScheduleResults/classScheduleResults/create-or-edit-classScheduleResult-modal.component';

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
import {NgxMatTimepickerModule} from 'ngx-mat-timepicker';
import { MaterialModule } from '../../core/material/material.module';;
import { CreateGroupUniversityProfessorWorkingTimeComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/create-group-university-professor-working-time/create-group-university-professor-working-time.component';
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
        PopoverModule.forRoot(),
        NgxMatTimepickerModule.setLocale("en-GB"),
        MaterialModule
    ],
    declarations: [
		ListOfMainDomainsComponent,

		ViewListOfMainDomainModalComponent,
		CreateOrEditListOfMainDomainModalComponent,
		MasterDetailChild_ListOfMainDomain_MainDomainsComponent,

		MasterDetailChild_ListOfMainDomain_ViewMainDomainModalComponent,
		MasterDetailChild_ListOfMainDomain_CreateOrEditMainDomainModalComponent,
    MasterDetailChild_ListOfMainDomain_MainDomainLessonsOfSemesterLookupTableModalComponent,
    MainDomainListOfMainDomainLookupTableModalComponent,
		MainDomainsComponent,

		ViewMainDomainModalComponent,
		CreateOrEditMainDomainModalComponent,
    MainDomainLessonsOfSemesterLookupTableModalComponent,
		MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpacesComponent,

		MasterDetailChild_ListOfClassScheduleModeSpace_ViewClassScheduleModeSpaceModalComponent,
		MasterDetailChild_ListOfClassScheduleModeSpace_CreateOrEditClassScheduleModeSpaceModalComponent,
    MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent,
    MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent,
    MasterDetailChild_ListOfClassScheduleModeSpace_ClassScheduleModeSpaceLessonLookupTableModalComponent,
    ClassScheduleModeSpaceListOfClassScheduleModeSpaceLookupTableModalComponent,
    MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent,
    MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent,
		ListOfAllCalculatedResultsComponent,

		ViewListOfAllCalculatedResultModalComponent,
		CreateOrEditListOfAllCalculatedResultModalComponent,
		MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultsComponent,

		MasterDetailChild_ListOfAllCalculatedResult_ViewClassScheduleResultModalComponent,
		MasterDetailChild_ListOfAllCalculatedResult_CreateOrEditClassScheduleResultModalComponent,
    MasterDetailChild_ListOfAllCalculatedResult_ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent,
    ClassScheduleResultListOfAllCalculatedResultLookupTableModalComponent,
		ClassScheduleModeSpacesComponent,

		ViewClassScheduleModeSpaceModalComponent,
		CreateOrEditClassScheduleModeSpaceModalComponent,
    ClassScheduleModeSpaceUniversityProfessorLookupTableModalComponent,
    ClassScheduleModeSpaceWorkTimeInDayLookupTableModalComponent,
    ClassScheduleModeSpaceLessonLookupTableModalComponent,
		MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultsComponent,

		MasterDetailChild_ClassScheduleModeSpace_ViewClassScheduleResultModalComponent,
		MasterDetailChild_ClassScheduleModeSpace_CreateOrEditClassScheduleResultModalComponent,
    ClassScheduleResultClassScheduleModeSpaceLookupTableModalComponent,
		ClassScheduleResultsComponent,

		ViewClassScheduleResultModalComponent,
		CreateOrEditClassScheduleResultModalComponent,
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
        DashboardComponent,
        CreateGroupUniversityProfessorWorkingTimeComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
