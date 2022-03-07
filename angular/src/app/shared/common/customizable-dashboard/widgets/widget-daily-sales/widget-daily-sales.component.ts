import { Component, OnInit, Injector } from '@angular/core';
import { TenantDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import { DashboardChartBase } from '../dashboard-chart-base';
import { WidgetComponentBaseComponent } from '../widget-component-base';

class DailySalesLineChart extends DashboardChartBase {

    chartData: any[];
    scheme: any = {
        name: 'green',
        selectable: true,
        group: 'Ordinal',
        domain: [
            '#34bfa3'
        ]
    };
    constructor(private _dashboardService: TenantDashboardServiceProxy) {
        super();
    }

    init(data) {
        this.chartData = [];

        if (data) {
            for (let i = 0; i < data.length; i++) {
                this.chartData.push({
                    name: new Date(data[i].dailySaleTime.toISODate()).toLocaleDateString('fa-Ir'),
                    value: data[i].dailySale
                });
            }
        }
    }

    reload() {
        this.showLoading();
        this._dashboardService
            .getDailySales()
            .subscribe(result => {
                this.init(result.dailySalesWithDate);
                this.hideLoading();
            });
    }
}

@Component({
    selector: 'app-widget-daily-sales',
    templateUrl: './widget-daily-sales.component.html',
    styleUrls: ['./widget-daily-sales.component.css']
})
export class WidgetDailySalesComponent extends WidgetComponentBaseComponent implements OnInit {

    dailySalesLineChart: DailySalesLineChart;
    showXAxis = true;
    showYAxis = true;
    gradient = true;
    showLegend = true;
    showXAxisLabel = true;
    xAxisLabel = 'Country';
    showYAxisLabel = true;
    yAxisLabel = 'Population';

    constructor(injector: Injector,
                private _tenantdashboardService: TenantDashboardServiceProxy) {
        super(injector);
        this.dailySalesLineChart = new DailySalesLineChart(this._tenantdashboardService);
    }

    // subDateRangeFilter() {
    //     abp.event.on('app.dashboardFilters.dateRangePicker.onDateChange', this.onDateRangeFilterChange);
    //   }

    //   unSubDateRangeFilter() {
    //     abp.event.off('app.dashboardFilters.dateRangePicker.onDateChange', this.onDateRangeFilterChange);
    //   }

    ngOnInit() {
        this.dailySalesLineChart.reload();
    }
}
