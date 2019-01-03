import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { appConfigToken, IAppConfig } from '@app/configs/app.config';
import { CustomerVisitsReport } from '@admin/models/customer-visits-report';

@Injectable()
export class CustomerVisitsReportService {
  private readonly baseResourceUrl: string;

  public constructor(@Inject(appConfigToken) private config: IAppConfig, private httpClient: HttpClient) {
    this.baseResourceUrl = config.apiUrl + '/CustomerVisitsReport';
  }

  public getAll(): Observable<CustomerVisitsReport[]> {
    return of([]);

    return this.httpClient.get<CustomerVisitsReport[]>(this.baseResourceUrl)
      .pipe(map((data) => {
        let items = [];

        data.forEach((item) => {
          const customerVisitsReport = new CustomerVisitsReport(item);
          items = [ ...items, customerVisitsReport ];
        });

        return items;
      }));
  }

  public create(formData: FormData): Observable<object> {
    return this.httpClient.post(this.baseResourceUrl, formData);
  }
}
