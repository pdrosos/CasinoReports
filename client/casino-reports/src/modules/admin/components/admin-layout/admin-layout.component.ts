import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '@core/services/authentication.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {
  public constructor(private authenticationService: AuthenticationService) {
  }

  public ngOnInit() {
  }

  public logout() {
    this.authenticationService.logout();
  }
}
