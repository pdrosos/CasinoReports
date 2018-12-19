import { InjectionToken } from '@angular/core';

export interface IAppConfig {
  apiUrl: string;
  roles: {
    administrator: string,
    chiefManager: string,
    casinoManager: string,
  };
}

export const appConfig: IAppConfig = {
  apiUrl: 'https://localhost:44300/api',
  roles: {
    administrator: 'Administrator',
    chiefManager: 'ChiefManager',
    casinoManager: 'CasinoManager',
  }
};

export const appConfigToken = new InjectionToken<IAppConfig>(
'app.module.config',
{
  providedIn: 'root',
  factory: () => {
    return appConfig;
  }
});
