import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AdminRoutingModule } from '@admin/admin-routing.module';
import { AdminLayoutComponent } from '@admin/components/admin-layout/admin-layout.component';
import { DashboardComponent } from '@admin/components/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    DashboardComponent,
  ],
  imports: [
    SharedModule,
    AdminRoutingModule,
  ],
})

export class AdminModule {}
