# Support Management System

## Overview

The **SaaS Support Management System** is a multi-tenant application built using .NET, designed to serve companies with multiple users who require support. The system allows company users to submit support reports, which can then be accessed and resolved by the company's technical team. Once an issue is resolved, the system notifies the report submitter via email or SMS.

## Features (In Development)

- **Multi-Tenancy:** Implementation of a one-database-per-tenant approach to ensure data isolation and security.
- **Report Submission:** Users will be able to submit detailed support reports.
- **Technical Team Management:** The technical team will have access to view, manage, and resolve reports.
- **Notifications:** The system will notify users via email or SMS about issue resolutions.
- **Scalable SaaS Architecture:** Designed to efficiently manage multiple companies within the platform.

## Multi-Tenancy Implementation (In Progress)

This project uses a **one-database-per-tenant** approach for multi-tenancy. Each tenant (company) has its own separate database, which is configured and managed through the `appsettings.json` file.
Here's an example of how tenant-specific configurations will be handled in the `appsettings.json`:

```json
{
.....
  "Tenants": {
    {
      "Name": "Abraji",
      "TenantId": "abraji",
      "ConnectionString": "Data Source=(localdb)\\ProjectModels;Initial Catalog=abraji;Trusted_connection=true;Encrypt=false"
    },
    {
      "Name": "ElBoss",
      "TenantId": "elboss",
      "ConnectionString": "Data Source=(localdb)\\ProjectModels;Initial Catalog=elboss;Trusted_connection=true;Encrypt=false"
    }
    .....
  }
}
```

## Prerequisites

- .NET SDK 8.0 or later
- SQL Server or compatible database system (it will support many different db providers soon)

## Notifications (Planned)
The system will include functionality to notify users via email or SMS once a report is resolved. This feature is under development.

## Contributing

Contributions are welcome as the project is still under development. Feel free to fork the repository, submit issues, or create pull requests.

## License

This project will be licensed under the MIT License once complete. See the (LICENSE)[LICENSE] file for more information.
