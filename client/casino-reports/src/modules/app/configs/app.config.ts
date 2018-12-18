import { InjectionToken } from '@angular/core';

export interface IAppConfig {
  apiBaseUrl: string;
}

export const appConfig: IAppConfig = {
  apiBaseUrl: 'https://localhost:44300/api'
};

export const appConfigToken = new InjectionToken<IAppConfig>(
'app.module.config',
{
  providedIn: 'root',
  factory: () => {
    return appConfig;
  }
});
