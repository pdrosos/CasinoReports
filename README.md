# Casino Reports

Casino Reports is a demo project for creating casino customer visits reports, based on customer visits data from CSV files, which uses [SQL Server Analysis Services Tabular Modeling](https://docs.microsoft.com/en-us/sql/analysis-services/tabular-models/tabular-models-ssas?view=sql-server-2017) and [Flexmonster Pivot Table & Charts](https://www.flexmonster.com).

Customer visits CSV import can be added to multiple Customer visits collections. Customer visits collections can be later used to filter the data in the reports.

## Server APIs

[The server](./server/CasinoReports) consists of 2 applications - **Casino Reports Identity Server, API and SSAS Tabular Model** and **Flexmonster Accelerator API**.

### Casino Reports Identity Server, API and SSAS Tabular Model

This application is [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/) OpenID Connect and OAuth 2.0 server + Casino Reports REST API for the [Angular client](./client/casino-reports), built with:

- [.NET Core 2.2](https://dotnet.microsoft.com/download)
- [ASP.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)
- [Entity Framework 2.2](https://docs.microsoft.com/en-us/ef/core/)
- [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/)
- [CsvHelper](https://joshclose.github.io/CsvHelper/)
- [SQL Server 2017 Developer Edition](https://docs.microsoft.com/en-us/sql/sql-server/sql-server-technical-documentation?view=sql-server-2017)
- [SQL Server Analysis Services Tabular Model](https://docs.microsoft.com/en-us/sql/analysis-services/tabular-models/tabular-models-ssas?view=sql-server-2017)
- [Visual Studio 2017 Community](https://visualstudio.microsoft.com/vs/)
- [Visual Studio 2017 Data Tools](https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017)
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)

The IdentityServer4 server is configured with OpenID Connect and OAuth 2.0 Implicit flow for the Angular client.

The SQL Server Analysis Services Tabular Model is based on the API's relational database and is in the [CasinoReports.Report.CustomerVisits](./server/CasinoReports/Report/CasinoReports.Report.CustomerVisits) project. It requires SQL Server 2017 with SSAS Tabular Model and Visual Studio 2017 Data Tools installed. When some customer visits CSV data is uploaded via the Angular client, the Tabular Model must be deployed to SSAS via Visual Studio Data Tools, so the data to be available for the Flexmonster component.

The application runs on https://localhost:44300. The SQL Server relational database is initialized and seeded on the first application start.

### Flexmonster Accelerator API

Flexmonster has backend [Accelerator](https://www.flexmonster.com/doc/getting-started-with-accelerator-ssas/), which takes over and speeds up the communication of the JavaScript component with SQL Server Analysis Services (SSAS) and [can be included in your backend application](https://www.flexmonster.com/doc/referring-accelerator-as-a-dll/), when you need application level authentication and authorization (Flexmonster JavaScript component support of custom Authorization Header on each request is comming soon).

The Flexmonster Accelerator API is [.NET 4.7.2](https://dotnet.microsoft.com/download/visual-studio-sdks) application (no current support of .NET Core) is used by the Flexmonster JavaScript component for communication with SSAS. The application runs on http://localhost:54998/api/Accelerator/

## Angular Client

[The client](./client/casino-reports) is [Angular CLI 7.1.4](https://cli.angular.io) and [Angular 7.1.4](https://angular.io) application with simple [Angular Material](https://material.angular.io) UI, which uses the [angular-oauth2-oidc](https://github.com/manfredsteyer/angular-oauth2-oidc) OpenID Connect and OAuth 2.0 Implicit flow library for authentication and authorization.

To start the application go to [its folder](./client/casino-reports) and run `ng serve` from the console. The application runs on http://localhost:5001