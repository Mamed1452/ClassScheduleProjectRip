import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';

import { MasterDetailChild_UniversityDepartment_UniversityMajorsComponent } from './masterDetailChild_UniversityDepartment_universityMajors.component';
import { MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent } from './masterDetailChild_UniversityDepartment_create-or-edit-universityMajor-modal.component';
import { MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent } from './masterDetailChild_UniversityDepartment_view-universityMajor-modal.component';

@NgModule({
    declarations: [
        MasterDetailChild_UniversityDepartment_UniversityMajorsComponent,
        MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent,
        MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent,
    ],
    imports: [AppSharedModule, AdminSharedModule],

    exports: [
        MasterDetailChild_UniversityDepartment_UniversityMajorsComponent,
        MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent,
        MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent,
    ],
})
export class MasterDetailChild_UniversityDepartment_UniversityMajorModule {}
