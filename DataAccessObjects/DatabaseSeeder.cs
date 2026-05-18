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
        var userNameColumns = context.Database
            .SqlQueryRaw<int>("SELECT COUNT(*) AS Value FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'AccountMember' AND COLUMN_NAME = 'UserName'")
            .Single();

        if (userNameColumns > 0)
        {
            return;
        }

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    private static void MakeUserNameOptional(MyStoreContext context)
    {
        var userNameColumns = context.Database
            .SqlQueryRaw<int>("SELECT COUNT(*) AS Value FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'AccountMember' AND COLUMN_NAME = 'UserName'")
            .Single();

        if (userNameColumns == 0)
        {
            return;
        }

        context.Database.ExecuteSqlRaw("ALTER TABLE AccountMember MODIFY UserName VARCHAR(30) NULL");
    }
}
