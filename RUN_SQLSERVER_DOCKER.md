# SQL Server Docker setup for Lab 1 MVC

This lab uses EF Core with the SQL Server provider. It auto-creates and seeds `MyStoreDB_Lab01` on startup with `Database.EnsureCreated()`.

Expected SQL Server connection:

- Host: `localhost`
- Port: `14330`
- User: `sa`
- Password: `123`
- Database: `MyStoreDB_Lab01`

Start SQL Server with Docker:

```bash
docker run -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 14330:1433 \
  --name sqlserver-lab \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

If this is a fresh container, change `sa` to `123` after startup:

```sql
ALTER LOGIN sa WITH PASSWORD = '123', CHECK_POLICY = OFF;
```

Run the lab:

```bash
dotnet run --project ProductManagementMVC/ProductManagementMVC.csproj
```

Seed accounts:

- `admin@store.com / 123`
- `staff@store.com / 123`
