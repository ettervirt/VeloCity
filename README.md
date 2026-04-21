# VeloCity API Documentation
VeloCity is a C# .NET-based backend application. This guide provides instructions on how to set up the development environment, configure the database, and run the application.

---
## Dependencies & Prerequisites
Before running the project, ensure you have the following installed:

* **Database**: [Docker Desktop](https://www.docker.com/products/docker-desktop/) or [Docker Engine](https://docs.docker.com/desktop/setup/install/windows-install/)
* **IDE**: [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
* **Tools**: `dotnet-ef` global tool (for database migrations)

---

## 🚀 Getting Started

### 1. Start the Database (Docker)

The project uses PostgreSQL as the primary database. A `docker-compose.yml` file is provided to spin up the database container quickly.

Open your terminal in the project root and run:

```bash
docker-compose up -d
```

This will start a PostgreSQL 18.3 instance named `velocity_db` on `localhost:5432`.

---

### 2. Install Dependencies (NuGet)

You need to restore the required NuGet packages before building the project.

#### Via CLI:

```bash
dotnet restore
```

#### Via IDE:

* **Visual Studio**:
  Right-click on the Solution in Solution Explorer and select **Restore NuGet Packages**.

* **JetBrains Rider**:
  The IDE usually detects missing packages automatically and shows a notification to "Restore".
  Alternatively, go to the **NuGet** tab at the bottom and click the **Restore** icon.

---

### 3. Run Database Migrations

To create the tables and schema in your local PostgreSQL instance, use the Entity Framework Core CLI.

Ensure you are in the project folder containing the `.csproj` file and run:

```bash
dotnet ef database update
```

> **Note:** If you don't have the EF tool installed, run:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

### 4. Running the Application

The application is configured to run on:

```
http://localhost:8081
```

(as defined in `appsettings.Development.json`)

#### Using IDE:

1. Open the solution file (`.sln`)
2. Set the main project as the **Startup Project**
3. Ensure the run profile is set to the project itself (usually named after your project) or **Kestrel**
4. Press:

   * **F5** (Visual Studio)
   * **Shift + F10** (Rider)

#### Using CLI:

```bash
dotnet run
```
