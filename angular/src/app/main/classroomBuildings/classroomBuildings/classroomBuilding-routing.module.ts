import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClassroomBuildingsComponent } from './classroomBuildings.component';

const routes: Routes = [
    {
        path: '',
        component: ClassroomBuildingsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ClassroomBuildingRoutingModule {}
