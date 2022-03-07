import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { UniversityDepartmentRoutingModule } from './universityDepartment-routing.module';
import { UniversityDepartmentsComponent } from './universityDepartments.component';
import { CreateOrEditUniversityDepartmentModalComponent } from './create-or-edit-universityDepartment-modal.component';
import { ViewUniversityDepartmentModalComponent } from './view-universityDepartment-modal.component';

import { MasterDetailChild_UniversityDepartment_UniversityMajorModule } from '@app/main/universityMajors/universityMajors/masterDetailChild_UniversityDepartment_universityMajor.module';

@NgModule({
    declarations: [
        UniversityDepartmentsComponent,
        CreateOrEditUniversityDepartmentModalComponent,
        ViewUniversityDepartmentModalComponent,
    ],
    imports: [
        AppSharedModule,
        UniversityDepartmentRoutingModule,
        AdminSharedModule,
        MasterDetailChild_UniversityDepartment_UniversityMajorModule,
    ],
})
export class UniversityDepartmentModule {}
