import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { appConfigToken, IAppConfig } from '@app/configs/app.config';
import { CustomerVisitsCollection } from '@admin/models/customer-visits-collection';

@Injectable()
export class CustomerVisitsCollectionService {
  private readonly baseResourceUrl: string;

  public constructor(@Inject(appConfigToken) private config: IAppConfig, private httpClient: HttpClient) {
    this.baseResourceUrl = config.apiUrl + '/CustomerVisitsCollection';
  }

  public getAll(): Observable<CustomerVisitsCollection[]> {
    return this.httpClient.get<CustomerVisitsCollection[]>(this.baseResourceUrl);
  }
}
