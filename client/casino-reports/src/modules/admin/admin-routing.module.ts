import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { appConfig } from '@app/configs/app.config';
import { AuthenticatedGuard } from '@core/guards/authenticated.guard';
import { IsInRoleGuard } from '@core/guards/is-in-role.guard';

import { AdminLayoutComponent } from '@admin/components/admin-layout/admin-layout.component';
import { DashboardComponent } from '@admin/components/dashboard/dashboard.component';
import { CustomerVisitsImportsComponent } from '@admin/components/customer-visits-imports/customer-visits-imports.component';
import { CustomerVisitsImportComponent } from '@admin/components/customer-visits-import/customer-visits-import.component';
import { CustomerVisitsReportsComponent } from '@admin/components/customer-visits-reports/customer-visits-reports.component';
import { CustomerVisitsReportComponent } from '@admin/components/customer-visits-report/customer-visits-report.component';
import { CustomerVisitsReportViewComponent } from '@admin/components/customer-visits-report-view/customer-visits-report-view.component';

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
        canActivate: [IsInRoleGuard],
        data: {roles: [appConfig.roles.administrator]},
        children: [
          {
            path: '',
            component: CustomerVisitsImportsComponent
          },
          {
            path: 'new',
            component: CustomerVisitsImportComponent,
          }
        ]
      },
      {
        path: 'customer-visits-reports',
        canActivate: [IsInRoleGuard],
        data: {roles: [appConfig.roles.administrator]},
        children: [
          {
            path: '',
            component: CustomerVisitsReportsComponent
          },
          {
            path: 'new',
            component: CustomerVisitsReportComponent,
          },
          {
            path: 'edit/:id',
            component: CustomerVisitsReportComponent,
          },
          {
            path: 'view/:id',
            component: CustomerVisitsReportViewComponent,
          }
        ]
      },
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
