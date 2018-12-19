import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { appConfigToken, IAppConfig } from '@app/configs/app.config';
import { CustomerVisitsImport } from '@admin/models/customer-visits-import';

@Injectable()
export class CustomerVisitsImportService {
  private baseResourceUrl: string;

  public constructor(@Inject(appConfigToken) private config: IAppConfig, private httpClient: HttpClient) {
    this.baseResourceUrl = config.apiUrl + '/CustomerVisitsImport';
  }

  public getAll(): Observable<CustomerVisitsImport[]> {
    return this.httpClient.get<CustomerVisitsImport[]>(this.baseResourceUrl);
  }

  public create(formData: FormData): Observable<object> {
    return this.httpClient.post(this.baseResourceUrl, formData);
  }
}
