import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { ClassroomBuildingRoutingModule } from './classroomBuilding-routing.module';
import { ClassroomBuildingsComponent } from './classroomBuildings.component';
import { CreateOrEditClassroomBuildingModalComponent } from './create-or-edit-classroomBuilding-modal.component';
import { ViewClassroomBuildingModalComponent } from './view-classroomBuilding-modal.component';

@NgModule({
    declarations: [
        ClassroomBuildingsComponent,
        CreateOrEditClassroomBuildingModalComponent,
        ViewClassroomBuildingModalComponent,
    ],
    imports: [AppSharedModule, ClassroomBuildingRoutingModule, AdminSharedModule],
})
export class ClassroomBuildingModule {}
