import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { BehaviorSubject, Subscription } from 'rxjs';

import { CustomerVisitsReport } from '@admin/models/customer-visits-report';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';

export class CustomerVisitsReportsDataSource extends MatTableDataSource<CustomerVisitsReport> {
  private subscriptions: Subscription;

  private readonly customerVisitsReports$: BehaviorSubject<CustomerVisitsReport[]>;

  public constructor(private customerVisitsReportService: CustomerVisitsReportService) {
    super();

    this.subscriptions = new Subscription();
    this.customerVisitsReports$ = new BehaviorSubject([]);

    this.subscriptions.add(
      this.customerVisitsReportService.getAll()
        .subscribe((customerVisitsReports: CustomerVisitsReport[]) => {
          this.customerVisitsReports$.next(customerVisitsReports);
        })
    );
  }

  public connect(): BehaviorSubject<CustomerVisitsReport[]> {
    return this.customerVisitsReports$;
  }

  disconnect() {
    this.subscriptions.unsubscribe();
  }
}

@Component({
  selector: 'app-customer-visits-reports',
  templateUrl: './customer-visits-reports.component.html',
})
export class CustomerVisitsReportsComponent implements OnInit {
  public dataSource: CustomerVisitsReportsDataSource;
  public columnsToDisplay = ['name', 'createdOn', 'actions'];

  public constructor(private customerVisitsReportService: CustomerVisitsReportService) {
  }

  public ngOnInit() {
    this.dataSource = new CustomerVisitsReportsDataSource(this.customerVisitsReportService);
  }

  public edit(id: number) {
    // todo
  }

  public delete(id: number) {
    // todo
  }
}
