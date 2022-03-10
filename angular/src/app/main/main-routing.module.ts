import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListOfMainDomainsComponent } from './listOfMainDomains/listOfMainDomains/listOfMainDomains.component';
import { MainDomainsComponent } from './mainDomains/mainDomains/mainDomains.component';
import { ListOfAllCalculatedResultsComponent } from './listOfAllCalculatedResults/listOfAllCalculatedResults/listOfAllCalculatedResults.component';
import { ClassScheduleModeSpacesComponent } from './classScheduleModeSpaces/classScheduleModeSpaces/classScheduleModeSpaces.component';
import { ClassScheduleResultsComponent } from './classScheduleResults/classScheduleResults/classScheduleResults.component';
import { LessonsOfUniversityProfessorsComponent } from './lessonsOfUniversityProfessors/lessonsOfUniversityProfessors/lessonsOfUniversityProfessors.component';
import { LessonsOfSemestersComponent } from './lessonsOfSemesters/lessonsOfSemesters/lessonsOfSemesters.component';
import { LessonsComponent } from './lessons/lessons/lessons.component';
import { UniversityProfessorWorkingTimesComponent } from './universityProfessorWorkingTimes/universityProfessorWorkingTimes/universityProfessorWorkingTimes.component';
import { UniversityProfessorsComponent } from './universityProfessors/universityProfessors/universityProfessors.component';
import { WorkTimeInDaysComponent } from './workTimeInDays/workTimeInDays/workTimeInDays.component';
import { AssigningGradeToUniversityMajorsComponent } from './assigningGradeToUniversityMajors/assigningGradeToUniversityMajors/assigningGradeToUniversityMajors.component';
import { SemestersComponent } from './semesters/semesters/semesters.component';
import { GradesComponent } from './grades/grades/grades.component';
import { AssigningUniversityMajorToClassroomBuildingsComponent } from './assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings.component';
import { UniversityDepartmentsComponent } from './universityDepartments/universityDepartments/universityDepartments.component';
import { UniversityMajorsComponent } from './universityMajors/universityMajors/universityMajors.component';
import { ClassroomBuildingsComponent } from './classroomBuildings/classroomBuildings/classroomBuildings.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'listOfMainDomains/listOfMainDomains', component: ListOfMainDomainsComponent, data: { permission: 'Pages.ListOfMainDomains' }  },
                    { path: 'mainDomains/mainDomains', component: MainDomainsComponent, data: { permission: 'Pages.MainDomains' }  },
                    { path: 'listOfAllCalculatedResults/listOfAllCalculatedResults', component: ListOfAllCalculatedResultsComponent, data: { permission: 'Pages.ListOfAllCalculatedResults' }  },
                    { path: 'classScheduleModeSpaces/classScheduleModeSpaces', component: ClassScheduleModeSpacesComponent, data: { permission: 'Pages.ClassScheduleModeSpaces' }  },
                    { path: 'classScheduleResults/classScheduleResults/:listOfAllCalculatedResultsId/:isAlocate', component: ClassScheduleResultsComponent, data: { permission: 'Pages.ClassScheduleResults' }  },
                    { path: 'lessonsOfUniversityProfessors/lessonsOfUniversityProfessors', component: LessonsOfUniversityProfessorsComponent, data: { permission: 'Pages.LessonsOfUniversityProfessors' }  },
                    { path: 'lessonsOfSemesters/lessonsOfSemesters', component: LessonsOfSemestersComponent, data: { permission: 'Pages.LessonsOfSemesters' }  },
                    { path: 'lessons/lessons', component: LessonsComponent, data: { permission: 'Pages.Lessons' }  },
                    { path: 'universityProfessorWorkingTimes/universityProfessorWorkingTimes', component: UniversityProfessorWorkingTimesComponent, data: { permission: 'Pages.UniversityProfessorWorkingTimes' }  },
                    { path: 'universityProfessors/universityProfessors', component: UniversityProfessorsComponent, data: { permission: 'Pages.UniversityProfessors' }  },
                    { path: 'workTimeInDays/workTimeInDays', component: WorkTimeInDaysComponent, data: { permission: 'Pages.WorkTimeInDays' }  },
                    { path: 'assigningGradeToUniversityMajors/assigningGradeToUniversityMajors', component: AssigningGradeToUniversityMajorsComponent, data: { permission: 'Pages.AssigningGradeToUniversityMajors' }  },
                    { path: 'semesters/semesters', component: SemestersComponent, data: { permission: 'Pages.Semesters' }  },
                    { path: 'grades/grades', component: GradesComponent, data: { permission: 'Pages.Grades' }  },
                    { path: 'assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings', component: AssigningUniversityMajorToClassroomBuildingsComponent, data: { permission: 'Pages.AssigningUniversityMajorToClassroomBuildings' }  },
                    { path: 'universityDepartments/universityDepartments', component: UniversityDepartmentsComponent, data: { permission: 'Pages.UniversityDepartments' }  },
                    { path: 'universityMajors/universityMajors', component: UniversityMajorsComponent, data: { permission: 'Pages.UniversityMajors' }  },
                    { path: 'classroomBuildings/classroomBuildings', component: ClassroomBuildingsComponent, data: { permission: 'Pages.ClassroomBuildings' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
