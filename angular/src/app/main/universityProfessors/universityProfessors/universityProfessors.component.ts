import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UniversityProfessorsServiceProxy, UniversityProfessorDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditUniversityProfessorModalComponent } from './create-or-edit-universityProfessor-modal.component';

import { ViewUniversityProfessorModalComponent } from './view-universityProfessor-modal.component';
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
    templateUrl: './universityProfessors.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class UniversityProfessorsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditUniversityProfessorModal', { static: true })
    createOrEditUniversityProfessorModal: CreateOrEditUniversityProfessorModalComponent;
    @ViewChild('viewUniversityProfessorModalComponent', { static: true })
    viewUniversityProfessorModal: ViewUniversityProfessorModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    universityProfessorNameFilter = '';
    isActiveFilter = -1;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.UniversityProfessor';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _universityProfessorsServiceProxy: UniversityProfessorsServiceProxy,
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

    getUniversityProfessors(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._universityProfessorsServiceProxy
            .getAll(
                this.filterText,
                this.universityProfessorNameFilter,
                this.isActiveFilter,
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

    createUniversityProfessor(): void {
        this.createOrEditUniversityProfessorModal.show();
    }

    showHistory(universityProfessor: UniversityProfessorDto): void {
        this.entityTypeHistoryModal.show({
            entityId: universityProfessor.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteUniversityProfessor(universityProfessor: UniversityProfessorDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._universityProfessorsServiceProxy.delete(universityProfessor.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._universityProfessorsServiceProxy
            .getUniversityProfessorsToExcel(this.filterText, this.universityProfessorNameFilter, this.isActiveFilter)
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
