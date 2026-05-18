using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public sealed class MyStoreContext : DbContext
{
    public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options)
    {
    }

    public DbSet<AccountMember> AccountMembers => Set<AccountMember>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMember>(entity =>
        {
            entity.ToTable("AccountMember");
            entity.HasKey(e => e.MemberId);
            entity.Property(e => e.MemberId).ValueGeneratedOnAdd();
            entity.Property(e => e.UserName).HasMaxLength(30);
            entity.HasIndex(e => e.UserName).IsUnique();
            entity.Property(e => e.FullName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.EmailAddress).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.EmailAddress).IsUnique();
            entity.Property(e => e.MemberPassword).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        });
    }
}
