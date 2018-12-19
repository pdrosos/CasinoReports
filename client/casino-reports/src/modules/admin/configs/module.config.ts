import { InjectionToken } from '@angular/core';

export interface IModuleConfig {
  customerVisitsImport: {
    maxFileSize: number,
  };
}

export const moduleConfig: IModuleConfig = {
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
