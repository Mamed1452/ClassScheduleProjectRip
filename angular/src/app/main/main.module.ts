import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UniversityDepartmentsComponent } from './universityDepartments/universityDepartments/universityDepartments.component';
import { ViewUniversityDepartmentModalComponent } from './universityDepartments/universityDepartments/view-universityDepartment-modal.component';
import { CreateOrEditUniversityDepartmentModalComponent } from './universityDepartments/universityDepartments/create-or-edit-universityDepartment-modal.component';

import { MasterDetailChild_UniversityDepartment_UniversityMajorsComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_universityMajors.component';
import { MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_view-universityMajor-modal.component';
import { MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_create-or-edit-universityMajor-modal.component';
import { MasterDetailChild_UniversityDepartment_UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajors/universityMajors/masterDetailChild_UniversityDepartment_universityMajor-universityDepartment-lookup-table-modal.component';

import { UniversityMajorsComponent } from './universityMajors/universityMajors/universityMajors.component';
import { ViewUniversityMajorModalComponent } from './universityMajors/universityMajors/view-universityMajor-modal.component';
import { CreateOrEditUniversityMajorModalComponent } from './universityMajors/universityMajors/create-or-edit-universityMajor-modal.component';
import { UniversityMajorUniversityDepartmentLookupTableModalComponent } from './universityMajors/universityMajors/universityMajor-universityDepartment-lookup-table-modal.component';


import { ClassroomBuildingsComponent } from './classroomBuildings/classroomBuildings/classroomBuildings.component';
import { ViewClassroomBuildingModalComponent } from './classroomBuildings/classroomBuildings/view-classroomBuilding-modal.component';
import { CreateOrEditClassroomBuildingModalComponent } from './classroomBuildings/classroomBuildings/create-or-edit-classroomBuilding-modal.component';


import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';

import { BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,
        TableModule,

        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot()
    ],
    declarations: [
		UniversityDepartmentsComponent,
		ViewUniversityDepartmentModalComponent,
		CreateOrEditUniversityDepartmentModalComponent,
		MasterDetailChild_UniversityDepartment_UniversityMajorsComponent,
		MasterDetailChild_UniversityDepartment_ViewUniversityMajorModalComponent,
		MasterDetailChild_UniversityDepartment_CreateOrEditUniversityMajorModalComponent,
    MasterDetailChild_UniversityDepartment_UniversityMajorUniversityDepartmentLookupTableModalComponent,
		UniversityMajorsComponent,
		ViewUniversityMajorModalComponent,
		CreateOrEditUniversityMajorModalComponent,
    UniversityMajorUniversityDepartmentLookupTableModalComponent,
		ClassroomBuildingsComponent,
		ViewClassroomBuildingModalComponent,
		CreateOrEditClassroomBuildingModalComponent,
        DashboardComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
