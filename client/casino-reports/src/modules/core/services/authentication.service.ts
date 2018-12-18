import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { filter, mergeMap  } from 'rxjs/operators';

import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

import { authConfig } from '@app/configs/auth.config';
import { User } from '@core/models/user';

@Injectable()
export class AuthenticationService {
  private isLoggedIn$: BehaviorSubject<boolean>;
  private user$: BehaviorSubject<User>;

  constructor(private oAuthService: OAuthService) {
    this.isLoggedIn$ = new BehaviorSubject<boolean>(this.hasValidTokens());
    this.user$ = new BehaviorSubject<User>(this.getUser());
  }

  public configure() {
    this.oAuthService.configure(authConfig);
    // this.oAuthService.tokenValidationHandler = new JwksValidationHandler();
    this.oAuthService.setupAutomaticSilentRefresh();

    this.oAuthService.loadDiscoveryDocumentAndTryLogin();

    // this.oAuthService.events
    //   .subscribe(e => {
    //     // tslint:disable-next-line:no-console
    //     console.debug('oauth/oidc event', e);
    //   });

    this.oAuthService.events
      .pipe(filter(e => e.type === 'session_terminated'))
      .subscribe(e => {
        this.setLoggedOut();
      });

    this.oAuthService.events
      .pipe(filter(e => e.type === 'logout'))
      .subscribe(e => {
        this.setLoggedOut();
      });

    this.oAuthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .pipe(mergeMap(() => this.oAuthService.loadUserProfile()))
      .subscribe(() => {
        this.setLoggedIn();
        // this.oAuthService.loadUserProfile()
        //   .then((userProfile) => {
        //     // tslint:disable:no-console
        //     console.debug('User profile', userProfile);
        //     console.debug('Granted scopes', this.oAuthService.getGrantedScopes());
        //   })
        //   .catch((err) => {
        //     // tslint:disable-next-line:no-console
        //     console.debug('User profile error', err);
        //   });
      });
  }

  public login(additionalState?: string, params?: string | object) {
    this.oAuthService.initImplicitFlow(additionalState, params);
  }

  public logout(noRedirectToLogoutUrl?: boolean): void {
    this.oAuthService.logOut(noRedirectToLogoutUrl);
  }

  public getIsLoggedInStream(): Observable<boolean> {
    return this.isLoggedIn$.asObservable();
  }

  public getUserStream(): Observable<User> {
    return this.user$.asObservable();
  }

  public getUser(): null|User {
    if (!this.hasValidTokens()) {
      return null;
    }

    const userInfo: any = this.oAuthService.getIdentityClaims();

    let roles: string[] = [];
    if (Array.isArray(userInfo.role)) {
      roles = userInfo.role;
    } else if (typeof userInfo.role === 'string' || userInfo.role instanceof String) {
      roles.push(userInfo.role);
    }

    const user: User = new User(userInfo.name, userInfo.preferred_username, userInfo.email, roles);

    return user;
  }

  public isInRole(role: string): boolean {
    const user: User = this.getUser();
    if (!user || !Array.isArray(user.roles)) {
      return false;
    }

    return user.roles.indexOf(role) !== -1;
  }

  private setLoggedIn() {
    this.isLoggedIn$.next(true);
    this.user$.next(this.getUser());
  }

  private setLoggedOut() {
    this.isLoggedIn$.next(false);
    this.user$.next(null);
  }

  private hasValidTokens(): boolean {
    return this.oAuthService.hasValidIdToken() && this.oAuthService.hasValidAccessToken();
  }

  private hasIdentityClaims(): boolean {
    const claims = this.oAuthService.getIdentityClaims();
    if (!claims) {
      return false;
    }

    return true;
  }
}
