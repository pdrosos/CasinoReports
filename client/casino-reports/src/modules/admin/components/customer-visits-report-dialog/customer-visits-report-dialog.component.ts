import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef, MatSnackBar } from '@angular/material';
import { BehaviorSubject, Observable } from 'rxjs';

import { CustomerVisitsReport } from '@admin/models/customer-visits-report';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';
import { finalize } from 'rxjs/operators';
import { ServerError } from '@admin/models/server-error';

@Component({
  selector: 'app-customer-visits-report-dialog',
  templateUrl: './customer-visits-report-dialog.component.html',
})
export class CustomerVisitsReportDialogComponent implements OnInit {
  private saving: BehaviorSubject<boolean>;
  private serverErrors: BehaviorSubject<string[]>;

  public customerVisitsReport: CustomerVisitsReport;

  public constructor(
    private dialogRef: MatDialogRef<CustomerVisitsReportDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: {customerVisitsReport?: CustomerVisitsReport, settings: object},
    private router: Router,
    private snackBar: MatSnackBar,
    private customerVisitsReportService: CustomerVisitsReportService) {
    this.customerVisitsReport = this.data.customerVisitsReport;
    this.saving = new BehaviorSubject(false);
    this.serverErrors = new BehaviorSubject([]);
  }

  public ngOnInit() {
  }

  get saving$(): Observable<boolean> {
    return this.saving.asObservable();
  }

  get serverErrors$(): Observable<string[]> {
    return this.serverErrors.asObservable();
  }

  public onSubmit(customerVisitsReport: CustomerVisitsReport) {
    this.saving.next(true);
    this.serverErrors.next([]);

    customerVisitsReport.settings = this.data.settings;

    if (this.customerVisitsReport) {
      this.customerVisitsReportService.update(this.customerVisitsReport.id, customerVisitsReport)
        .pipe(finalize(() => {
          this.saving.next(false);
        }))
        .subscribe(
        () => {
          this.snackBar.open('Report is updated.', '', {
            duration: 3000,
          });
          this.dialogRef.close();
          this.router.navigate(['/admin/customer-visits-reports']);
        },
        (error: ServerError) => {
          this.handleServerError(error);
        });
    } else {
      this.customerVisitsReportService.create(customerVisitsReport)
        .pipe(finalize(() => {
          this.saving.next(false);
        }))
        .subscribe(
        () => {
          this.snackBar.open('Report is created.', '', {
            duration: 3000,
          });
          this.dialogRef.close();
          this.router.navigate(['/admin/customer-visits-reports']);
        },
        (error: ServerError) => {
          this.handleServerError(error);
        });
    }
  }

  public onCancel() {
    this.dialogRef.close();
  }

  private handleServerError(error: ServerError) {
    if (error.status !== 400 || !error.hasOwnProperty('error')) {
      this.serverErrors.next(
        [...this.serverErrors.getValue(),
        'Server error occurred. Please try again later.']);

      return;
    }

    for (const field in error.error.errors) {
      if (!error.error.errors.hasOwnProperty(field)) {
        continue;
      }

      error.error.errors[field].forEach((err) => {
        this.serverErrors.next([...this.serverErrors.getValue(), err]);
      });
    }
  }
}
