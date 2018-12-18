import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthenticatedGuard } from '@core/guards/authenticated.guard';

import { AdminLayoutComponent } from '@admin/components/admin-layout/admin-layout.component';
import { DashboardComponent } from '@admin/components/dashboard/dashboard.component';
import { CustomerVisitsImportsComponent } from '@admin/components/customer-visits-imports/customer-visits-imports.component';
import { CustomerVisitsImportComponent } from '@admin/components/customer-visits-import/customer-visits-import.component';
import { CustomerVisitsReportsComponent } from '@admin/components/customer-visits-reports/customer-visits-reports.component';
import { CustomerVisitsReportComponent } from '@admin/components/customer-visits-report/customer-visits-report.component';

const adminRoutes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  }, {
    path: '',
    component: AdminLayoutComponent,
    canActivate: [AuthenticatedGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'customer-visits-imports',
        component: CustomerVisitsImportsComponent,
      },
      {
        path: 'customer-visits-imports/new',
        component: CustomerVisitsImportComponent,
      },
      {
        path: 'customer-visits-reports',
        component: CustomerVisitsReportsComponent,
      },
      {
        path: 'customer-visits-reports/new',
        component: CustomerVisitsReportComponent,
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(adminRoutes)
  ],
  exports: [
    RouterModule,
  ]
})
export class AdminRoutingModule { }
