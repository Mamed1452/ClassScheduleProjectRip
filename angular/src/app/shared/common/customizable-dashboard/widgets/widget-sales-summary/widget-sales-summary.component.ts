﻿import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { forEach as _forEach } from 'lodash-es';
import { SalesSummaryDatePeriod, TenantDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import { DashboardChartBase } from '../dashboard-chart-base';
import { WidgetComponentBaseComponent } from '../widget-component-base';

class SalesSummaryChart extends DashboardChartBase {
    totalSales = 0;
    totalSalesCounter = 0;
    revenue = 0;
    revenuesCounter = 0;
    expenses = 0;
    expensesCounter = 0;
    growth = 0;
    growthCounter = 0;
    totalFactorsNetFee: number;
    totalPaymentPrice: number;

    selectedDatePeriod: SalesSummaryDatePeriod;

    data = [];

    constructor(
        private _dashboardService: TenantDashboardServiceProxy) {
        super();
    }

    init(salesSummaryData, totalSales, revenue, expenses, growth) {
        this.totalSales = totalSales;
        this.totalSalesCounter = totalSales;
        this.revenue = revenue;
        this.expenses = expenses;
        this.growth = growth;

        this.setChartData(salesSummaryData);

        this.hideLoading();
    }

    setChartData(items): void {
        let sales = [];
        let payments = [];

        _forEach(items, (item) => {
            payments.push({
                'name': item['period'],
                'value': item['payments']
            });

            sales.push({
                'name': item['period'],
                'value': item['sales']
            });
        });

        this.data = [
            {
                'name': 'فروش',
                'series': sales
            }, {
                'name': 'دریافتی',
                'series': payments
            }
        ];
    }

    reload(datePeriod) {
        if (this.selectedDatePeriod === datePeriod) {
            this.hideLoading();
            return;
        }

        this.selectedDatePeriod = datePeriod;

        this.showLoading();
        this._dashboardService
            .getSalesSummary(datePeriod)
            .subscribe(result => {
                this.totalFactorsNetFee = result.totalFactorsNetFee;
                this.totalPaymentPrice = result.totalPaymentPrice;
                this.setChartData(result.salesSummary);
                this.hideLoading();
            });
    }
}


@Component({
    selector: 'app-widget-sales-summary',
    templateUrl: './widget-sales-summary.component.html',
    styleUrls: ['./widget-sales-summary.component.css']
})
export class WidgetSalesSummaryComponent extends WidgetComponentBaseComponent implements OnInit, OnDestroy {

    salesSummaryChart: SalesSummaryChart;
    appSalesSummaryDateInterval = SalesSummaryDatePeriod;

    constructor(injector: Injector,
                private _tenantDashboardServiceProxy: TenantDashboardServiceProxy) {
        super(injector);
        this.salesSummaryChart = new SalesSummaryChart(this._tenantDashboardServiceProxy);
    }

    ngOnInit(): void {
        this.subDateRangeFilter();

        this.runDelayed(() => {
            this.salesSummaryChart.reload(SalesSummaryDatePeriod.Daily);
        });
    }

    onDateRangeFilterChange = (dateRange) => {
        this.runDelayed(() => {
            this.salesSummaryChart.reload(SalesSummaryDatePeriod.Daily);

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
