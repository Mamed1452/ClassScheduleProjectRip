import { V } from '@angular/cdk/keycodes';
import { Component, OnInit, Injector } from '@angular/core';
import { GetTopStatsOutput, TenantDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import { DateTime } from 'luxon';
import { DashboardChartBase } from '../dashboard-chart-base';
import { WidgetComponentBaseComponent } from '../widget-component-base';


// class DashboardTopStats extends DashboardChartBase {


//     payToSalesRatio: number;
//     totalContractConut: number;
//     totalContractLiveConut: number;
//     totalContratCtost: number;
//     totalContratLiveCtost: number;
//     totalContratNotExchangeCount: number;
//     totalContratNotExchangeCtost: number;
//     totalContratNotExchangeErrorCount: number;
//     totalContratNotExchangeFatalCount: number;
//     totalContratNotExchangeWarnCount: number;
//     totalFactorCost: number;
//     totalFactorCount: number;
//     totalSalesOpportunityCost: number;
//     totalSalesOpportunityCount: number;
//     totalUsanceConut: number;
//     totalUsanceCost: number;

//     totalProfit = 0;
//     totalProfitCounter = 0;
//     newFeedbacks = 0;
//     newFeedbacksCounter = 0;
//     newOrders = 0;
//     newOrdersCounter = 0;
//     newUsers = 0;
//     newUsersCounter = 0;

//     data: any;

//     totalProfitChange = 76;
//     totalProfitChangeCounter = 0;
//     newFeedbacksChange = 85;
//     newFeedbacksChangeCounter = 0;
//     newOrdersChange = 45;
//     newOrdersChangeCounter = 0;
//     newUsersChange = 57;
//     newUsersChangeCounter = 0;

//     init(data: any) {
//         this.payToSalesRatio = data.payToSalesRatio;
//         this.totalContractConut = data.totalContractConut;
//         this.totalContractLiveConut = data.totalContractLiveConut;
//         this.totalContratCtost = data.totalContratCtost;
//         this.totalContratLiveCtost = data.totalContratLiveCtost;
//         this.totalContratNotExchangeCount = data.totalContratNotExchangeCount;
//         this.totalContratNotExchangeCtost = data.totalContratNotExchangeCtost;
//         this.totalContratNotExchangeErrorCount = data.totalContratNotExchangeErrorCount;
//         this.totalContratNotExchangeFatalCount = data.totalContratNotExchangeFatalCount;
//         this.totalContratNotExchangeWarnCount = data.totalContratNotExchangeWarnCount;
//         this.totalFactorCost = data.totalFactorCost;
//         this.totalFactorCount = data.totalFactorCount;
//         this.totalSalesOpportunityCost = data.totalSalesOpportunityCost;
//         this.totalSalesOpportunityCount = data.totalSalesOpportunityCount;
//         this.totalUsanceConut = data.totalUsanceConut;
//         this.totalUsanceCost = data.totalUsanceCost;
//         this.hideLoading();
//     }


// }

@Component({
    selector: 'app-widget-top-stats',
    templateUrl: './widget-top-stats.component.html',
    styleUrls: ['./widget-top-stats.component.css']
})
export class WidgetTopStatsComponent extends WidgetComponentBaseComponent implements OnInit {
    stats:GetTopStatsOutput=new GetTopStatsOutput();
    selectedDateRange: DateTime[] = [];
    // dashboardTopStats: DashboardTopStats;
    obj: any;

    constructor(injector: Injector,
                private _tenantDashboardServiceProxy: TenantDashboardServiceProxy,
    ) {
        super(injector);
        // this.dashboardTopStats = new DashboardTopStats();
    }

    ngOnInit() {
      this.subDateRangeFilter();
      this.onDateRangeFilterChange(undefined);
    }



    onDateRangeFilterChange = (dateRange) => {
      this._tenantDashboardServiceProxy.getTopStats(
        dateRange?dateRange[0]:undefined,
        dateRange?dateRange[1]:undefined,
        undefined,
        undefined,
        undefined,
        undefined,
        undefined,
        undefined).subscribe(result => {
                this.stats = result;
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
