namespace LibraryManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationContextDb : DbContext
    {
        public ApplicationContextDb()
            : base("name=LibraryEntity")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Publisher)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Available)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Transactions)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Transactions1)
                .WithOptional(e => e.Book1)
                .HasForeignKey(e => e.BookId);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Tfn)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);
        }
    }
}
