import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
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
