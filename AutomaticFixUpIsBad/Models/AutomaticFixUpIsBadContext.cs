using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace AutomaticFixUpIsBad.Models
{
    public partial class AutomaticFixUpIsBadContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }

        private static readonly LoggerFactory Logger = new LoggerFactory(new[] { new DebugLoggerProvider() });


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=AutomaticFixUpIsBad;Trusted_Connection=True";

            optionsBuilder.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(Logger);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerFk).HasColumnName("customer_fk");

                entity.HasOne(d => d.CustomerFkNavigation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Customer");
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InvoiceFk).HasColumnName("invoice_fk");

                entity.HasOne(d => d.InvoiceFkNavigation)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.InvoiceFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLine_Invoice");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
