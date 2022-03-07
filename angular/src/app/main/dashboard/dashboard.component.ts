import { Component, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DashboardCustomizationConst } from '@app/shared/common/customizable-dashboard/DashboardCustomizationConsts';
import { TenantDashboardServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    encapsulation: ViewEncapsulation.None
})

export class DashboardComponent extends AppComponentBase implements OnInit {
    dashboardName = DashboardCustomizationConst.dashboardNames.defaultTenantDashboard;

    constructor(
        injector: Injector,
        private _dashboardService: TenantDashboardServiceProxy) {
        super(injector);
    }
    ngOnInit(): void {

    }
}
