import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { AuthenticationService } from '@core/services/authentication.service';
import { User } from '@core/models/user';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
})
export class AdminLayoutComponent implements OnInit {
  public navigation: any[] = [
    { name: 'Dashboard', route: 'dashboard' },
    { name: 'Customer visits imports', route: 'customer-visits-imports' },
    { name: 'Customer visits reports', route: 'customer-visits-reports' },
  ];

  public user: User;

  public constructor(public title: Title, private authenticationService: AuthenticationService) {
  }

  public ngOnInit() {
    this.title.setTitle('Casino Reports');

    this.user = this.authenticationService.getUser();
  }

  public logout() {
    this.authenticationService.logout();
  }
}
