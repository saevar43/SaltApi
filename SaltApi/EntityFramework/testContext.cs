using Microsoft.EntityFrameworkCore;

namespace SaltApi
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("transactions");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("numeric(12,2)");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Merchant)
                    .HasColumnName("merchant")
                    .HasMaxLength(7);

                entity.Property(e => e.Pan)
                    .HasColumnName("pan")
                    .HasMaxLength(24);

                entity.Property(e => e.Transdate).HasColumnName("transdate");

                entity.Property(e => e.Voided)
                    .HasColumnName("voided")
                    .HasDefaultValueSql("false");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
