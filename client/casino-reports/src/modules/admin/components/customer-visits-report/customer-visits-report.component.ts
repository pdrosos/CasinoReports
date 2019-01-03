import { Component, Inject, OnInit } from '@angular/core';

import { IModuleConfig, moduleConfigToken } from '@admin/configs/module.config';

@Component({
  selector: 'app-customer-visits-report',
  templateUrl: './customer-visits-report.component.html',
})
export class CustomerVisitsReportComponent implements OnInit {
  public report: object;

  public constructor(@Inject(moduleConfigToken) private config: IModuleConfig) {
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

}
