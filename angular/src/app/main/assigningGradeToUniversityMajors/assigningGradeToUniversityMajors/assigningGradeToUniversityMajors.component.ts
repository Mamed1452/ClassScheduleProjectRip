import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    AssigningGradeToUniversityMajorsServiceProxy,
    AssigningGradeToUniversityMajorDto,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAssigningGradeToUniversityMajorModalComponent } from './create-or-edit-assigningGradeToUniversityMajor-modal.component';

import { ViewAssigningGradeToUniversityMajorModalComponent } from './view-assigningGradeToUniversityMajor-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './assigningGradeToUniversityMajors.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class AssigningGradeToUniversityMajorsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditAssigningGradeToUniversityMajorModal', { static: true })
    createOrEditAssigningGradeToUniversityMajorModal: CreateOrEditAssigningGradeToUniversityMajorModalComponent;
    @ViewChild('viewAssigningGradeToUniversityMajorModalComponent', { static: true })
    viewAssigningGradeToUniversityMajorModal: ViewAssigningGradeToUniversityMajorModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameAssignedGradeToUniversityMajorFilter = '';
    gradeGradeNameFilter = '';
    universityMajorUniversityMajorNameFilter = '';

    _entityTypeFullName =
        'Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.AssigningGradeToUniversityMajor';
    entityHistoryEnabled = false;

    semesterRowSelection: boolean[] = [];

    childEntitySelection: {} = {};

    constructor(
        injector: Injector,
        private _assigningGradeToUniversityMajorsServiceProxy: AssigningGradeToUniversityMajorsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return (
            this.isGrantedAny('Pages.Administration.AuditLogs') &&
            customSettings.EntityHistory &&
            customSettings.EntityHistory.isEnabled &&
            _filter(
                customSettings.EntityHistory.enabledEntities,
                (entityType) => entityType === this._entityTypeFullName
            ).length === 1
        );
    }

    getAssigningGradeToUniversityMajors(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._assigningGradeToUniversityMajorsServiceProxy
            .getAll(
                this.filterText,
                this.nameAssignedGradeToUniversityMajorFilter,
                this.gradeGradeNameFilter,
                this.universityMajorUniversityMajorNameFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createAssigningGradeToUniversityMajor(): void {
        this.createOrEditAssigningGradeToUniversityMajorModal.show();
    }

    showHistory(assigningGradeToUniversityMajor: AssigningGradeToUniversityMajorDto): void {
        this.entityTypeHistoryModal.show({
            entityId: assigningGradeToUniversityMajor.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteAssigningGradeToUniversityMajor(assigningGradeToUniversityMajor: AssigningGradeToUniversityMajorDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._assigningGradeToUniversityMajorsServiceProxy
                    .delete(assigningGradeToUniversityMajor.id)
                    .subscribe(() => {
                        this.reloadPage();
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        });
    }

    exportToExcel(): void {
        this._assigningGradeToUniversityMajorsServiceProxy
            .getAssigningGradeToUniversityMajorsToExcel(
                this.filterText,
                this.nameAssignedGradeToUniversityMajorFilter,
                this.gradeGradeNameFilter,
                this.universityMajorUniversityMajorNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    selectEditTable(table) {
        this.childEntitySelection = {};
        this.childEntitySelection[table] = true;
    }

    openChildRowForSemester(index, table) {
        let isAlreadyOpened = this.semesterRowSelection[index];
        this.closeAllChildRows();
        this.semesterRowSelection = [];
        if (!isAlreadyOpened) {
            this.semesterRowSelection[index] = true;
        }
        this.selectEditTable(table);
    }

    closeAllChildRows(): void {
        this.semesterRowSelection = [];
    }
}
