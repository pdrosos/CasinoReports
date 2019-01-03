export class CustomerVisitsReport {
  public id: number;
  public name: string;
  public createdOn: Date;

  constructor(obj: CustomerVisitsReport) {
    if (obj instanceof Object) {
      Object.assign(this, obj);
    }
  }
}
