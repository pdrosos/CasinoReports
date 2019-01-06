import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';

import { CustomerVisitsImport } from '@admin/models/customer-visits-import';
import { CustomerVisitsImportService } from '@admin/services/customer-visits-import.service';

export class CustomerVisitsImportsDataSource extends MatTableDataSource<CustomerVisitsImport> {
  private readonly customerVisitsImports$: BehaviorSubject<CustomerVisitsImport[]>;

  public constructor(private customerVisitsImportService: CustomerVisitsImportService) {
    super();

    this.customerVisitsImports$ = new BehaviorSubject([]);
    this.getData();
  }

  public connect(): BehaviorSubject<CustomerVisitsImport[]> {
    return this.customerVisitsImports$;
  }

  public getData() {
    this.customerVisitsImportService.getAll()
      .subscribe((customerVisitsImports: CustomerVisitsImport[]) => {
        this.customerVisitsImports$.next(customerVisitsImports);
      });
  }

  public disconnect() {
    this.customerVisitsImports$.complete();
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

  public delete(id: number) {
    // todo
  }
}
