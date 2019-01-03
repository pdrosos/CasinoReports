import { InjectionToken } from '@angular/core';

export interface IModuleConfig {
  flexmonster: {
    apiUrl: string,
    catalog: string,
    cube: string,
  };
  customerVisitsImport: {
    maxFileSize: number,
  };
}

export const moduleConfig: IModuleConfig = {
  flexmonster: {
    apiUrl: 'http://localhost:54998/api/Accelerator/',
    catalog: 'CasinoReportsReportCustomerVisits',
    cube: 'CustomerVisits',
  },
  customerVisitsImport: {
    maxFileSize: 2 * 1024 * 1024,
  },
};

export const moduleConfigToken = new InjectionToken<IModuleConfig>(
  'app.module.config',
  {
    providedIn: 'root',
    factory: () => {
      return moduleConfig;
    }
  });
