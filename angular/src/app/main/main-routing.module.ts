import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
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
