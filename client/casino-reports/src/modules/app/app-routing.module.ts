import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from '@app/components/home/home.component';
import { LogoutComponent } from '@app/components/logout/logout.component';
import { NotAuthenticatedGuard } from '@core/guards/not-authenticated.guard';

const appRoutes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'logout',
    component: LogoutComponent,
    canActivate: [NotAuthenticatedGuard],
  },
  {
    path: 'admin',
    loadChildren: '../admin/admin.module#AdminModule',
  },
  // not found route
  {
    path: '**',
    redirectTo: '/',
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {
      paramsInheritanceStrategy: 'always',
    }),
  ],
  exports: [
    RouterModule,
  ]
})
export class AppRoutingModule { }
