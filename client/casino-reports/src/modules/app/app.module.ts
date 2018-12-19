import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { OAuthModule } from 'angular-oauth2-oidc';

import { CoreModule } from '@core/core.module';
import { SharedModule } from '@shared/shared.module';

import { AppRoutingModule } from '@app/app-routing.module';

import { appConfig, appConfigToken } from '@app/configs/app.config';
import { HomeComponent } from '@app/components/home/home.component';
import { RootComponent } from '@app/components/root/root.component';
import { LogoutComponent } from '@app/components/logout/logout.component';

@NgModule({
  declarations: [
    RootComponent,
    HomeComponent,
    LogoutComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: [appConfig.apiUrl],
        sendAccessToken: true
      }
    }),
    CoreModule,
    SharedModule,
    AppRoutingModule,
  ],
  providers: [
    { provide: appConfigToken, useValue: appConfig },
  ],
  bootstrap: [RootComponent]
})
export class AppModule { }
