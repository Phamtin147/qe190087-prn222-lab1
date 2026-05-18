# MySQL/MariaDB setup for Lab 1 and Lab 2

Both labs use EF Core with Pomelo MySQL provider. They auto-create and seed their database on startup with `Database.EnsureCreated()`.

Default connection strings use your DBeaver/MySQL account:

- User: `root`
- Password: `1`
- Host: `localhost`
- Port: `3306`

Databases:

- Lab 1 MVC: `MyStoreDB_Lab01`
- Lab 2 Razor Pages: `MyStoreDB_Lab02`

Run Lab 1:

```bash
cd Lab01_ProductManagementMVC
dotnet run --project ProductManagementMVC/ProductManagementMVC.csproj
```

Run Lab 2:

```bash
cd Lab02_ProductManagementRazorPages
dotnet run --project ProductManagementRazorPages/ProductManagementRazorPages.csproj
```

On first startup, each app creates tables:

- `AccountMember`
- `Categories`
- `Products`

Seed accounts:

- Lab 1 MVC: `admin@store.com / 123`, `staff@store.com / 123`
- Lab 2 Razor Pages: `admin / 123`, `staff / 123`
