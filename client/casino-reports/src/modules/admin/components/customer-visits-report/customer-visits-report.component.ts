import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { FlexmonsterPivot } from 'ng-flexmonster';

import { IModuleConfig, moduleConfigToken } from '@admin/configs/module.config';
import { CustomerVisitsReport } from '@admin/models/customer-visits-report';
import {
  CustomerVisitsReportDialogComponent
} from '@admin/components/customer-visits-report-dialog/customer-visits-report-dialog.component';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';

@Component({
  selector: 'app-customer-visits-report',
  templateUrl: './customer-visits-report.component.html',
})
export class CustomerVisitsReportComponent implements OnInit {
  @ViewChild('pivot') private pivot: FlexmonsterPivot;
  private readonly report: object;

  public readonly licenseKey: string;
  public customerVisitsReport: CustomerVisitsReport;

  public constructor(
    private router: Router,
    private route: ActivatedRoute,
    @Inject(moduleConfigToken) private config: IModuleConfig,
    private dialog: MatDialog,
    private customerVisitsReportService: CustomerVisitsReportService) {
    this.licenseKey = this.config.flexmonster.licenseKey;
    this.report = {
      dataSource: {
        dataSourceType: 'microsoft analysis services',
        /* URL to Flexmonster Accelerator */
        proxyUrl: this.config.flexmonster.apiUrl,
        /* Catalog name */
        catalog: this.config.flexmonster.catalog,
        /* Cube name */
        cube: this.config.flexmonster.cube,
        // Flag to use the Accelerator instead of XMLA protocol
        binary: true
      },
      formats: [
        {
          name: 'Decimal Places',
          maxDecimalPlaces: 5,
        }
      ],
    };
  }

  public ngOnInit() {
  }

  public onFlexmonsterReady() {
    this.route.paramMap
      .pipe(switchMap((params) => {
        const id = +params.get('id');
        if (!id) {
          return of(null);
        }

        return this.customerVisitsReportService.getById(id);
      }))
      .subscribe((customerVisitsReport) => {
        this.customerVisitsReport = customerVisitsReport;

        if (!customerVisitsReport) {
          this.pivot.flexmonster.setReport(this.report);

          return;
        }

        this.pivot.flexmonster.setReport(customerVisitsReport.settings);
      },
      () => {
        this.router.navigate(['/admin/customer-visits-reports']);
      });
  }

  public onBeforeToolbarCreated(toolbar) {
    const tabs = toolbar.getTabs();

    const disallowedTabsIds = [
      'fm-tab-connect',
      'fm-tab-open',
    ];
    const allowedTabs = tabs.filter(el => !disallowedTabsIds.includes(el.id));
    allowedTabs.forEach(tab => {
      if (tab.id === 'fm-tab-save') {
        tab.handler = () => this.openCustomerVisitsReportDialog();
      }
    });

    toolbar.getTabs = function () {
      return allowedTabs;
    };
  }

  private openCustomerVisitsReportDialog() {
    const settings = Object.assign({}, this.pivot.flexmonster.getReport());

    const dialogConfig = new MatDialogConfig();
    dialogConfig.hasBackdrop = true;
    // dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = {customerVisitsReport: this.customerVisitsReport, settings: settings};

    const dialogRef = this.dialog.open(CustomerVisitsReportDialogComponent, dialogConfig);
  }
}
