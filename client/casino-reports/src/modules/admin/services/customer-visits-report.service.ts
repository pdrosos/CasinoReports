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

  public getById(id: number): Observable<CustomerVisitsReport> {
    const url = `${this.baseResourceUrl}/${id}`;

    return this.httpClient.get<CustomerVisitsReport>(url)
      .pipe(map((item) => {
        return new CustomerVisitsReport(item);
      }));
  }

  public create(customerVisitsReport: CustomerVisitsReport): Observable<any> {
    return this.httpClient.post(this.baseResourceUrl, customerVisitsReport);
  }

  public update(id: number, customerVisitsReport: CustomerVisitsReport): Observable<any> {
    const url = `${this.baseResourceUrl}/${id}`;

    return this.httpClient.put(url, customerVisitsReport);
  }

  public delete(id: number): Observable<any> {
    const url = `${this.baseResourceUrl}/${id}`;

    return this.httpClient.delete(url);
  }
}
