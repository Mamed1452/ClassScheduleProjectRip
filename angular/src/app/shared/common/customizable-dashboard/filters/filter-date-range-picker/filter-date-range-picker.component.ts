import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TenantDashboardServiceProxy ,HostDashboardServiceProxy} from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { PermissionCheckerService, RefreshTokenService } from 'abp-ng2-module';

@Component({
  selector: 'app-filter-date-range-picker',
  templateUrl: './filter-date-range-picker.component.html',
  styleUrls: ['./filter-date-range-picker.component.css']
})
export class FilterDateRangePickerComponent extends AppComponentBase implements OnInit {
  selectedDateRange: DateTime[] = [];
  loading=false;
  constructor(
    injector: Injector,
    private _dateTimeService: DateTimeService,
    private tenantDashboardService: TenantDashboardServiceProxy,
    public dialogRef: MatDialogRef<FilterDateRangePickerComponent>,
    private _permissionChecker: PermissionCheckerService,
    private hostDashboard: HostDashboardServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getFilterDates();

  }

  onChange() {
    abp.event.trigger('app.dashboardFilters.dateRangePicker.onDateChange',
      this.selectedDateRange);
  }

  getFilterDates() {
    this.loading=true;
    if (this._permissionChecker.isGranted('Pages.Administration.Host.Dashboard')) {
        this.hostDashboard.getHostFilterDates().subscribe(response => {
            this.loading=false;
            this.selectedDateRange = [];
            this.selectedDateRange.push(response.startDate);
            this.selectedDateRange.push(response.endDate);
          })

    }

    if (this._permissionChecker.isGranted('Pages.Tenant.Dashboard')) {
        this.tenantDashboardService.getFilterDates().subscribe(response => {
            this.loading=false;
            this.selectedDateRange = [];
            this.selectedDateRange.push(response.startDate);
            this.selectedDateRange.push(response.endDate);
          })
    }

  }
  close(){
    this.dialogRef.close();

  }


  ok(){
    this.onChange();
    this.dialogRef.close();
  }
}
