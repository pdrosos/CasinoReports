import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AdminRoutingModule } from '@admin/admin-routing.module';
import { AdminLayoutComponent } from '@admin/components/admin-layout/admin-layout.component';
import { DashboardComponent } from '@admin/components/dashboard/dashboard.component';
import { CustomerVisitsImportsComponent } from '@admin/components/customer-visits-imports/customer-visits-imports.component';
import { CustomerVisitsImportComponent } from '@admin/components/customer-visits-import/customer-visits-import.component';
import { CustomerVisitsReportsComponent } from '@admin/components/customer-visits-reports/customer-visits-reports.component';
import { CustomerVisitsReportComponent } from '@admin/components/customer-visits-report/customer-visits-report.component';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    DashboardComponent,
    CustomerVisitsImportsComponent,
    CustomerVisitsImportComponent,
    CustomerVisitsReportComponent,
    CustomerVisitsReportsComponent,
  ],
  imports: [
    SharedModule,
    AdminRoutingModule,
  ],
})

export class AdminModule {}
