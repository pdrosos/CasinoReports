import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { moduleConfigToken, IModuleConfig } from '@admin/configs/module.config';
import { CustomerVisitsCollection } from '@admin/models/customer-visits-collection';
import { ServerError } from '@admin/models/server-error';
import { CustomerVisitsCollectionService } from '@admin/services/customer-visits-collection.service';
import { CustomerVisitsImportService } from '@admin/services/customer-visits-import.service';

@Component({
  selector: 'app-customer-visits-import',
  templateUrl: './customer-visits-import.component.html',
})
export class CustomerVisitsImportComponent implements OnInit {
  public customerVisitsCollections$: Observable<CustomerVisitsCollection[]>;
  public maxFileSize: number;

  private uploading: BehaviorSubject<boolean>;
  private serverErrors: BehaviorSubject<string[]>;

  public constructor(
    @Inject(moduleConfigToken) private config: IModuleConfig,
    private router: Router,
    private snackBar: MatSnackBar,
    private customerVisitsCollectionService: CustomerVisitsCollectionService,
    private customerVisitsImportService: CustomerVisitsImportService) {

    this.maxFileSize = config.customerVisitsImport.maxFileSize;
    this.uploading = new BehaviorSubject(false);
    this.serverErrors = new BehaviorSubject([]);
  }

  public ngOnInit() {
    this.customerVisitsCollections$ = this.customerVisitsCollectionService.getAll();
  }

  get uploading$(): Observable<boolean> {
    return this.uploading.asObservable();
  }

  get serverErrors$(): Observable<string[]> {
    return this.serverErrors.asObservable();
  }

  public onSubmit(formData: FormData) {
    this.uploading.next(true);
    this.serverErrors.next([]);

    this.customerVisitsImportService.create(formData)
      .pipe(finalize(() => {
        this.uploading.next(false);
      }))
      .subscribe(
      () => {
        this.snackBar.open('Import completed.', '', {
          duration: 3000,
        });
        this.router.navigate(['/admin/customer-visits-imports']);
      },
      (error: ServerError) => {
        this.handleServerError(error);
      });
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
