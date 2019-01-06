import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { FlexmonsterPivot } from 'ng-flexmonster';

import { IModuleConfig, moduleConfigToken } from '@admin/configs/module.config';
import { CustomerVisitsReport } from '@admin/models/customer-visits-report';
import { CustomerVisitsReportService } from '@admin/services/customer-visits-report.service';

@Component({
  selector: 'app-customer-visits-report-view',
  templateUrl: './customer-visits-report-view.component.html',
})
export class CustomerVisitsReportViewComponent implements OnInit {
  @ViewChild('pivot') private pivot: FlexmonsterPivot;

  public licenseKey: string;
  public customerVisitsReport: CustomerVisitsReport;

  public constructor(
    private router: Router,
    private route: ActivatedRoute,
    @Inject(moduleConfigToken) private config: IModuleConfig,
    private customerVisitsReportService: CustomerVisitsReportService) {
    this.licenseKey = this.config.flexmonster.licenseKey;
  }

  public ngOnInit() {
  }

  public onFlexmonsterReady() {
    this.route.paramMap
      .pipe(switchMap((params) => {
        const id = +params.get('id');

        return this.customerVisitsReportService.getById(id);
      }))
      .subscribe((customerVisitsReport) => {
        this.customerVisitsReport = customerVisitsReport;
        const report = customerVisitsReport.settings;

        if (!report.hasOwnProperty('options')) {
          report.options = {};
        }

        report.options.configuratorActive = false;
        report.options.configuratorButton = false;

        if (!report.options.hasOwnProperty('grid')) {
          report.options.grid = {};
        }

        report.options.grid.showFilter = false;

        if (!report.options.hasOwnProperty('chart')) {
          report.options.chart = {};
        }

        report.options.chart.showFilter = false;

        this.pivot.flexmonster.setReport(report);
      },
      () => {
        this.router.navigate(['/admin/customer-visits-reports']);
      });
  }

  public onBeforeToolbarCreated(toolbar) {
    const tabs = toolbar.getTabs();

    const allowedTabsIds = [
      'fm-tab-export',
      'fm-tab-grid',
      'fm-tab-charts',
      'fm-tab-format',
      'fm-tab-fullscreen',
    ];
    const allowedTabs = tabs.filter(el => allowedTabsIds.includes(el.id));

    toolbar.getTabs = function () {
      return allowedTabs;
    };
  }
}
