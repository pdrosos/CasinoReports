import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '@core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './root.component.html',
})
export class RootComponent implements OnInit {
  public constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.configure();
  }

  public ngOnInit() {
  }
}
