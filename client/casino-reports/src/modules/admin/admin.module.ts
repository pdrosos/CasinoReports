import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AdminRoutingModule } from '@admin/admin-routing.module';

import { moduleConfig, moduleConfigToken } from '@admin/configs/module.config';
import { AdminLayoutComponent } from '@admin/components/admin-layout/admin-layout.component';
import { DashboardComponent } from '@admin/components/dashboard/dashboard.component';
import { CustomerVisitsImportsComponent } from '@admin/components/customer-visits-imports/customer-visits-imports.component';
import { CustomerVisitsImportComponent } from '@admin/components/customer-visits-import/customer-visits-import.component';
import { CustomerVisitsImportFormComponent } from '@admin/forms/customer-visits-import-form/customer-visits-import-form.component';
import { CustomerVisitsReportsComponent } from '@admin/components/customer-visits-reports/customer-visits-reports.component';
import { CustomerVisitsReportComponent } from '@admin/components/customer-visits-report/customer-visits-report.component';
import { CustomerVisitsReportViewComponent } from './components/customer-visits-report-view/customer-visits-report-view.component';
import {
  CustomerVisitsReportDialogComponent
} from '@admin/components/customer-visits-report-dialog/customer-visits-report-dialog.component';
import { CustomerVisitsReportFormComponent } from '@admin/forms/customer-visits-report-form/customer-visits-report-form.component';
import { DeleteItemDialogComponent } from './components/delete-item-dialog/delete-item-dialog.component';
import { CustomerVisitsCollectionService } from '@admin/services/customer-visits-collection.service';
import { CustomerVisitsImportService } from '@admin/services/customer-visits-import.service';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    DashboardComponent,
    CustomerVisitsImportsComponent,
    CustomerVisitsImportComponent,
    CustomerVisitsImportFormComponent,
    CustomerVisitsReportsComponent,
    CustomerVisitsReportComponent,
    CustomerVisitsReportViewComponent,
    CustomerVisitsReportDialogComponent,
    CustomerVisitsReportFormComponent,
    DeleteItemDialogComponent,
  ],
  entryComponents: [
    CustomerVisitsReportDialogComponent,
    DeleteItemDialogComponent,
  ],
  imports: [
    SharedModule,
    AdminRoutingModule,
  ],
  providers: [
    { provide: moduleConfigToken, useValue: moduleConfig },
    CustomerVisitsCollectionService,
    CustomerVisitsImportService,
    CustomerVisitsReportService,
  ],
})

export class AdminModule {}
