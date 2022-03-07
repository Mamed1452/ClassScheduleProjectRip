import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { UniversityMajorRoutingModule } from './universityMajor-routing.module';
import { UniversityMajorsComponent } from './universityMajors.component';
import { CreateOrEditUniversityMajorModalComponent } from './create-or-edit-universityMajor-modal.component';
import { ViewUniversityMajorModalComponent } from './view-universityMajor-modal.component';
import { UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajor-universityDepartment-lookup-table-modal.component';

@NgModule({
    declarations: [
        UniversityMajorsComponent,
        CreateOrEditUniversityMajorModalComponent,
        ViewUniversityMajorModalComponent,

        UniversityMajorUniversityDepartmentLookupTableModalComponent,
    ],
    imports: [AppSharedModule, UniversityMajorRoutingModule, AdminSharedModule],
})
export class UniversityMajorModule {}
