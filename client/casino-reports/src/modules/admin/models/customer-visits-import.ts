import { CustomerVisitsCollection } from '@admin/models/customer-visits-collection';

export class CustomerVisitsImport {
  public id: number;
  public name: string;
  public collections: CustomerVisitsCollection[];
  public createdOn: Date;

  constructor(obj: CustomerVisitsImport) {
    if (obj instanceof Object) {
      Object.assign(this, obj);
    }
  }

  public getCollectionsNames(): string {
    return this.collections.map(c => c.name).join(', ');
  }
}
