import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { DashboardChartBase } from '../dashboard-chart-base';
import { TenantDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import { curveBasis } from 'd3-shape';
import { WidgetComponentBaseComponent } from '../widget-component-base';
import { DateTime } from 'luxon';

class RegionalStatsTable extends DashboardChartBase {
  stats: Array<any>;
  colors = ['#00c5dc', '#f4516c', '#34bfa3', '#ffb822'];
  customColors = [
    { name: '1', value: '#00c5dc' },
    { name: '2', value: '#f4516c' },
    { name: '3', value: '#34bfa3' },
    { name: '4', value: '#ffb822' },
    { name: '5', value: '#00c5dc' }
  ];

  curve: any = curveBasis;

  constructor(private _dashboardService: TenantDashboardServiceProxy) {
    super();
  }

  init() {
    // this.reload();
  }

  formatData(): any {
    for (let j = 0; j < this.stats.length; j++) {
      let stat = this.stats[j];

      let series = [];
      // for (let i = 0; i < stat.length; i++) {
      //   series.push({
      //     name: i + 1,
      //     value: stat.change[i]
      //   });
      // }

      // stat = [
      //   {
      //     'name': j + 1,
      //     'series': series
      //   }
      // ];

    }
  }

  // reload() {
  //   this.showLoading();
  //   this._dashboardService
  //     .getContractTypeState()
  //     .subscribe(result => {
  //       this.stats = result.stats;
  //       this.formatData();
  //       this.hideLoading();
  //     });
  // }
}

@Component({
  selector: 'app-widget-regional-stats',
  templateUrl: './widget-regional-stats.component.html',
  styleUrls: ['./widget-regional-stats.component.css']
})
export class WidgetRegionalStatsComponent extends WidgetComponentBaseComponent implements OnInit,OnDestroy {

    stats: Array<any>;
  regionalStatsTable: RegionalStatsTable;

  constructor(injector: Injector,
    private _dashboardService: TenantDashboardServiceProxy,
              private tenantDashboardServiceProxy: TenantDashboardServiceProxy) {
    super(injector);
    this.regionalStatsTable = new RegionalStatsTable(this._dashboardService);
  }

  ngOnInit() {
    this.subDateRangeFilter();
    this.onDateRangeFilterChange(undefined);
  }




  onDateRangeFilterChange = (dateRange) => {
    this._dashboardService.getContractTypeState(
      dateRange?dateRange[0]:undefined,
      dateRange?dateRange[1]:undefined
      ).subscribe(result => {
              this.stats = result.stats;
          });
  }

  subDateRangeFilter() {
    abp.event.on('app.dashboardFilters.dateRangePicker.onDateChange', this.onDateRangeFilterChange);
  }

  unSubDateRangeFilter() {
    abp.event.off('app.dashboardFilters.dateRangePicker.onDateChange', this.onDateRangeFilterChange);
  }

  ngOnDestroy(): void {
    this.unSubDateRangeFilter();
  }
}
