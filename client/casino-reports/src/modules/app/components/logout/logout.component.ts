import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '@core/services/authentication.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
})
export class LogoutComponent implements OnInit {
  public constructor(private authenticationService: AuthenticationService) {
  }

  public ngOnInit() {
  }

  public login() {
    this.authenticationService.login();
  }
}
