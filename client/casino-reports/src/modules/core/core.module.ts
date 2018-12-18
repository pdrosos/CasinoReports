import { NgModule, Optional, SkipSelf } from '@angular/core';

import { AuthenticatedGuard } from '@core/guards/authenticated.guard';
import { IsInRoleGuard } from '@core/guards/is-in-role.guard';
import { NotAuthenticatedGuard } from '@core/guards/not-authenticated.guard';
import { AuthenticationService } from '@core/services/authentication.service';

@NgModule({
  imports: [],
  providers: [
    AuthenticatedGuard,
    IsInRoleGuard,
    NotAuthenticatedGuard,
    AuthenticationService,
  ],
})
export class CoreModule {
  constructor (@Optional() @SkipSelf() parentModule: CoreModule) {
      if (parentModule) {
          throw new Error('CoreModule is already loaded. Import it in the AppModule only');
      }
  }
}
