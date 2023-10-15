using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Models;

public partial class ShoppingDbContext : DbContext
{
    public ShoppingDbContext()
    {
    }

    public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ListItem> ListItems { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ShoppingList;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B3FE3E6B5");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E83EB205AC55B");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Imagepath).HasMaxLength(255);
            entity.Property(e => e.ItemName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Items__CategoryI__3C69FB99");
        });

        modelBuilder.Entity<ListItem>(entity =>
        {
            entity.HasKey(e => e.ListItemId).HasName("PK__ListItem__D93C69676E0B0D0D");

            entity.Property(e => e.ListItemId).HasColumnName("ListItemID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ListId).HasColumnName("ListID");

            entity.HasOne(d => d.Item).WithMany(p => p.ListItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__ListItems__ItemI__4316F928");

            entity.HasOne(d => d.List).WithMany(p => p.ListItems)
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK__ListItems__ListI__4222D4EF");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("PK__Shopping__E3832865F6EC2E58");

            entity.Property(e => e.ListId).HasColumnName("ListID");
            entity.Property(e => e.ListName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ShoppingL__UserI__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACDDFDEB06");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348DF548CF").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
