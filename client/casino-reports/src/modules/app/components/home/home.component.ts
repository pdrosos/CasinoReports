import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { skipWhile, take } from 'rxjs/operators';

import { AuthenticationService } from '@core/services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  private subscriptions: Subscription;

  public constructor(private router: Router, private authenticationService: AuthenticationService) {
    this.subscriptions = new Subscription();
  }

  public ngOnInit() {
  }

  public ngAfterViewInit() {
    this.subscriptions.add(
      this.authenticationService.getIsLoggedInStream()
        .pipe(skipWhile(isLoggedIn => isLoggedIn === false))
        .pipe(take(1))
        .subscribe((isLoggedIn: boolean) => {
          // https://github.com/manfredsteyer/angular-oauth2-oidc/issues/424
          setTimeout(() => this.router.navigate(['/admin']), 200);
        })
    );
  }

  public login() {
    this.authenticationService.login();
  }

  public ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
