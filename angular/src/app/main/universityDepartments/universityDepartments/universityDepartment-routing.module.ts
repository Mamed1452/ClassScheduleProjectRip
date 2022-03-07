import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UniversityDepartmentsComponent } from './universityDepartments.component';

const routes: Routes = [
    {
        path: '',
        component: UniversityDepartmentsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UniversityDepartmentRoutingModule {}
