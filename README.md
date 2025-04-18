# Products API

This is a simple Products API built with ASP.NET Core.

## Prerequisites

*   .NET 8.0 SDK
*   Docker (optional)

## Running Locally

1.  Clone the repository:

    ```bash
    git clone <repository\_url>
    cd <repository\_directory>
    ```

2.  Update the database connection string in `src/API/appsettings.json` to match your local SQL Server instance.

3.  Apply EF Core migrations:

    ```bash
    dotnet ef migrations add InitialMigration -p src/Infrastructure -s src/API
    dotnet ef database update -p src/Infrastructure -s src/API
    ```

4.  Run the application:

    ```bash
    dotnet run --project src/API
    ```

    The API will be available at `https://localhost:<port>`.

## Running with Docker

1.  Build the Docker image:

    ```bash
    docker build -t products-api .
    ```

2.  Run the Docker container:

    ```bash
    docker run -p 8080:80 products-api
    ```

    The API will be available at `http://localhost:8080`.

    **Note:** You may need to configure the database connection string in the Docker container. This can be done by setting environment variables.

## Authentication

The API uses JWT Bearer authentication. To register a new user, send a POST request to `/api/User/register` with the following JSON payload:

```json
{
    "username": "<username>",
    "password": "<password>"
}
```

After successful registration, you can obtain a JWT token by logging in. This token can then be used to access the protected endpoints.
