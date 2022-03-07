import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    
                    {
                        path: 'universityDepartments/universityDepartments',
                        loadChildren: () => import('./universityDepartments/universityDepartments/universityDepartment.module').then(m => m.UniversityDepartmentModule),
                        data: { permission: 'Pages.UniversityDepartments' }
                    },
                
                    
                    {
                        path: 'universityMajors/universityMajors',
                        loadChildren: () => import('./universityMajors/universityMajors/universityMajor.module').then(m => m.UniversityMajorModule),
                        data: { permission: 'Pages.UniversityMajors' }
                    },
                
                    
                    {
                        path: 'classroomBuildings/classroomBuildings',
                        loadChildren: () => import('./classroomBuildings/classroomBuildings/classroomBuilding.module').then(m => m.ClassroomBuildingModule),
                        data: { permission: 'Pages.ClassroomBuildings' }
                    },
                
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
