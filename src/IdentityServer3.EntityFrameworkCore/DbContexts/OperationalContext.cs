using Microsoft.EntityFrameworkCore;
using IdentityServer3.EntityFrameworkCore.Entities;

namespace IdentityServer3.EntityFrameworkCore.DbContexts
{
    public class OperationalContext : BaseContext
    {
        public OperationalContext(DbContextOptions options)
            : base(options)
        { }


        public DbSet<Consent> Consents { get; set; }

        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consent>(b =>
            {
                b.ToTable(EfConstants.TableNames.Consent);
                b.Property(e => e.SubjectId).HasMaxLength(200);
                b.Property(e => e.ClientId).HasMaxLength(200);
                b.Property(e => e.Scopes).IsRequired().HasMaxLength(2000);
                b.HasKey(e => new { e.SubjectId, e.ClientId });
            });
            
            modelBuilder.Entity<Token>(b =>
            {
                b.ToTable(EfConstants.TableNames.Token);
                b.Property(e => e.SubjectId).HasMaxLength(200);
                b.Property(e => e.ClientId).IsRequired().HasMaxLength(200);
                b.Property(e => e.JsonCode).IsRequired();
                b.Property(e => e.Expiry).IsRequired();
                b.HasKey(e => new { e.Key, e.TokenType });
            });                
        }
    }
}