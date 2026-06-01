using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public static class DatabaseSeeder
{
    public static void Seed(MyStoreContext context)
    {
        context.Database.EnsureCreated();
        RecreateIfOldSchema(context);
        MakeUserNameOptional(context);

        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { CategoryName = "Beverages" },
                new Category { CategoryName = "Food" },
                new Category { CategoryName = "Stationery" });
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { ProductName = "Coffee", CategoryId = 1, UnitsInStock = 40, UnitPrice = 4.5m },
                new Product { ProductName = "Notebook", CategoryId = 3, UnitsInStock = 120, UnitPrice = 2.2m },
                new Product { ProductName = "Sandwich", CategoryId = 2, UnitsInStock = 25, UnitPrice = 3.8m });
            context.SaveChanges();
        }

        if (!context.AccountMembers.Any())
        {
            context.AccountMembers.AddRange(
                new AccountMember { UserName = "admin", FullName = "Store Admin", EmailAddress = "admin@store.com", MemberPassword = "123", MemberRole = 1 },
                new AccountMember { UserName = "staff", FullName = "Store Staff", EmailAddress = "staff@store.com", MemberPassword = "123", MemberRole = 2 });
            context.SaveChanges();
        }
    }

    private static void RecreateIfOldSchema(MyStoreContext context)
    {
        var userNameColumns = CountUserNameColumns(context);

        if (userNameColumns > 0)
        {
            return;
        }

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    private static void MakeUserNameOptional(MyStoreContext context)
    {
        var userNameColumns = CountUserNameColumns(context);

        if (userNameColumns == 0)
        {
            return;
        }

        DropUserNameIndex(context);
        context.Database.ExecuteSqlRaw("ALTER TABLE AccountMember ALTER COLUMN UserName NVARCHAR(30) NULL");
    }

    private static int CountUserNameColumns(MyStoreContext context) => context.Database
        .SqlQueryRaw<int>("SELECT COUNT(*) AS Value FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccountMember' AND COLUMN_NAME = 'UserName'")
        .Single();

    private static void DropUserNameIndex(MyStoreContext context)
    {
        context.Database.ExecuteSqlRaw("IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AccountMember_UserName' AND object_id = OBJECT_ID('AccountMember')) DROP INDEX IX_AccountMember_UserName ON AccountMember");
    }
}
