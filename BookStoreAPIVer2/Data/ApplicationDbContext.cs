using BookStoreAPIVer2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> InvoiceDetails { get; set; }
        public DbSet<TimeKeeping> TimeKeepings { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Cart> Carts {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeKeeping>().HasKey(c => new { c.AccId, c.StartTime });

            modelBuilder.Entity<OrderDetail>().HasKey(c => new { c.InvoiceId, c.BookId });

            modelBuilder.Entity<TimeKeeping>().HasOne(c => c.Employee).WithMany().HasForeignKey(c => c.AccId);

            modelBuilder.Entity<OrderDetail>().HasOne(c => c.Book).WithMany().HasForeignKey(c => c.BookId);

            modelBuilder.Entity<OrderDetail>().HasOne(c => c.Invoice).WithMany().HasForeignKey(c => c.InvoiceId);

            modelBuilder.Entity<Order>().HasOne(i => i.Employee).WithMany().HasForeignKey(i => i.AccId);

            modelBuilder.Entity<Order>().HasOne(i => i.Customer).WithMany().HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Book>().HasOne(b => b.Category).WithMany().HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Image>().HasOne(i => i.Book).WithMany().HasForeignKey(i => i.BookId);

            modelBuilder.Entity<Cart>().HasOne(c => c.Book).WithMany().HasForeignKey(c => c.BookId);

            modelBuilder.Entity<Cart>().HasOne(c => c.Customer).WithMany().HasForeignKey(c => c.CustomerId);

            base.OnModelCreating((modelBuilder));
        }
    }
}
