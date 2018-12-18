import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '@core/services/authentication.service';

@Injectable()
export class NotAuthenticatedGuard implements CanActivate {
  public constructor(private router: Router, private authenticationService: AuthenticationService) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.authenticationService.getIsLoggedInStream().pipe(
      map((signedIn: boolean) => {
        if (!signedIn) {
          return true;
        }

        this.router.navigate(['/']);

        return false;
      })
    );
  }
}
