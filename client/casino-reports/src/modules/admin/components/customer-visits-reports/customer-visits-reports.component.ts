import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSnackBar } from '@angular/material';
import { BehaviorSubject } from 'rxjs';

import { DeleteItemDialogComponent } from '@admin/components/delete-item-dialog/delete-item-dialog.component';
import { CustomerVisitsReport } from '@admin/models/customer-visits-report';
import { ServerError } from '@admin/models/server-error';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';

export class CustomerVisitsReportsDataSource extends MatTableDataSource<CustomerVisitsReport> {
  private readonly customerVisitsReports$: BehaviorSubject<CustomerVisitsReport[]>;

  public constructor(private customerVisitsReportService: CustomerVisitsReportService) {
    super();

    this.customerVisitsReports$ = new BehaviorSubject([]);
    this.getData();
  }

  public connect(): BehaviorSubject<CustomerVisitsReport[]> {
    return this.customerVisitsReports$;
  }

  public getData() {
    this.customerVisitsReportService.getAll()
      .subscribe((customerVisitsReports: CustomerVisitsReport[]) => {
        this.customerVisitsReports$.next(customerVisitsReports);
      });
  }

  public disconnect() {
    this.customerVisitsReports$.complete();
  }
}

@Component({
  selector: 'app-customer-visits-reports',
  templateUrl: './customer-visits-reports.component.html',
})
export class CustomerVisitsReportsComponent implements OnInit {
  public dataSource: CustomerVisitsReportsDataSource;
  public columnsToDisplay = ['name', 'createdOn', 'actions'];

  public constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private customerVisitsReportService: CustomerVisitsReportService) {
  }

  public ngOnInit() {
    this.dataSource = new CustomerVisitsReportsDataSource(this.customerVisitsReportService);
  }

  public delete(id: number) {
    const dialogRef = this.dialog.open(DeleteItemDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result !== true) {
        return;
      }

      this.customerVisitsReportService.delete(id)
        .subscribe(() => {
          this.dataSource.getData();

          this.snackBar.open('Item is deleted.', '', {
            duration: 3000,
          });
        },
          (error: ServerError) => {
          if (error.status === 404) {
            this.snackBar.open(`Item with id ${id} is not found.`, '', {
              duration: 3000,
            });

            return;
          }

          this.snackBar.open('Server error occurred. Please try again later.', '', {
            duration: 3000,
          });
        });
    });
  }
}
