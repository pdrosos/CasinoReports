import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { BehaviorSubject, Subscription } from 'rxjs';

import { CustomerVisitsImport } from '@admin/models/customer-visits-import';
import { CustomerVisitsImportService } from '@admin/services/customer-visits-import.service';

export class CustomerVisitsImportsDataSource extends MatTableDataSource<CustomerVisitsImport> {
  private subscriptions: Subscription;

  private readonly customerVisitsImports$: BehaviorSubject<CustomerVisitsImport[]>;

  public constructor(private customerVisitsImportService: CustomerVisitsImportService) {
    super();

    this.subscriptions = new Subscription();
    this.customerVisitsImports$ = new BehaviorSubject([]);

    this.subscriptions.add(
      this.customerVisitsImportService.getAll()
        .subscribe((customerVisitsImports: CustomerVisitsImport[]) => {
          this.customerVisitsImports$.next(customerVisitsImports);
        })
    );
  }

  public connect(): BehaviorSubject<CustomerVisitsImport[]> {
    return this.customerVisitsImports$;
  }

  disconnect() {
    this.subscriptions.unsubscribe();
  }
}

@Component({
  selector: 'app-customer-visits-imports',
  templateUrl: './customer-visits-imports.component.html',
})
export class CustomerVisitsImportsComponent implements OnInit {
  public dataSource: CustomerVisitsImportsDataSource;
  public columnsToDisplay = ['name', 'collections', 'createdOn', 'actions'];

  public constructor(private customerVisitsImportService: CustomerVisitsImportService) {
  }

  public ngOnInit() {
    this.dataSource = new CustomerVisitsImportsDataSource(this.customerVisitsImportService);
  }

  public edit(id: number) {
    // todo
  }

  public delete(id: number) {
    // todo
  }
}
