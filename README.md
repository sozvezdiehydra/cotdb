# 🏥 SickLeaveApp (cotdb)
**Automated software application for temporary disability benefits calculation.**

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![WPF](https://img.shields.io/badge/UI-WPF-orange.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![PostgreSQL](https://img.shields.io/badge/DB-PostgreSQL%20%2F%20SQLite-lightgrey.svg)](https://www.postgresql.org/)

This project is designed to automate financial calculations in compliance with **Federal Law No. 255-FZ** "On Compulsory Social Insurance against Temporary Disability and in Connection with Maternity."

---

## Key Features

-  **Automated Calculation**: Computes benefit amounts based on insurance coverage years (60%, 80%, 100% rates).
-  **Smart Period Handling**: Automatically calculates disability days and splits the payment sources (first 3 days paid by the employer, the remainder by the Social Fund).
-  **Limit Management**: Takes into account the annual maximum earnings limits (Social Fund caps) for accurate benefit assessment.
-  **Data Export**:
  - **PDF Generation**: Creates professional printable calculation reports using QuestPDF.
  - **XML Export**: Generates data files formatted for transmission to the Social Fund (SFR) systems.
-  **Hybrid Database Support**: Smart auto-switching between **PostgreSQL** (for networked office environments) and **SQLite** (for local/portable use).


---

## Project Architecture

The application follows **Clean Architecture** principles and the **MVVM** (Model-View-ViewModel) pattern:

1.  **SickLeaveApp.Domain**: The Core layer. Contains business entities (`Employee`, `SickLeavePeriod`), core interfaces, and legal constants.
2.  **SickLeaveApp.Application**: Business Logic layer. Contains calculation services and use-case handlers.
3.  **SickLeaveApp.Infrastructure**: Data Access & External Services. Implementation of repositories (EF Core), database context, and PDF/XML export services.
4.  **SickLeaveApp.UI**: Presentation layer (WPF). Contains the user interface, styles, and ViewModels.

---

## Technology Stack

- **Language**: C# 12 (.NET 8)
- **UI Framework**: WPF + CommunityToolkit.Mvvm
- **ORM**: Entity Framework Core
- **Databases**: PostgreSQL / SQLite
- **Reporting**: QuestPDF (Fluent API)
- **Libraries**: Npgsql, Microsoft.EntityFrameworkCore.Sqlite

---

## Getting Started

### For Users (Portable Release):
1. Navigate to the **[Releases](https://github.com/sozvezdiehydra/cotdb/releases)** section.
2. Download the latest `sickleaveapp_v1.0_Portable.rar` archive.
3. Extract the archive and run `SickLeaveApp.UI.exe`.
   *Note: If a PostgreSQL server is not detected locally, the app will automatically initialize a local SQLite database (`sickleave.db`) in the application folder.*

## Database Configuration

- By default, the application looks for a PostgreSQL instance at localhost:5432. Connection settings are managed in App.xaml.cs. If a connection cannot be established within a 2-second timeout, the app seamlessly falls back to a local SQLite file.