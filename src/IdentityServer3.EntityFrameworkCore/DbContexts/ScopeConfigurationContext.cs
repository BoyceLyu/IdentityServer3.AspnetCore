using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using IdentityServer3.EntityFrameworkCore.Entities;

namespace IdentityServer3.EntityFrameworkCore.DbContexts
{
    public class ScopeConfigurationContext : BaseContext
    {
        public ScopeConfigurationContext(DbContextOptions options)
            : base(options)
        { }

        public ScopeConfigurationContext(DbContextOptions<ScopeConfigurationContext> options)
            : base(options)
        { }

        public DbSet<Scope> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScopeClaim>(b =>
            {
                b.ToTable(EfConstants.TableNames.ScopeClaim);
                b.Property(e => e.Name).IsRequired().HasMaxLength(200);
                b.Property(e => e.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<ScopeSecret>(b =>
            {
                b.ToTable(EfConstants.TableNames.ScopeSecrets);
                b.Property(e => e.Description).HasMaxLength(1000);
                b.Property(e => e.Type).HasMaxLength(250);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
            });

            modelBuilder.Entity<Scope>(b =>
            {
                b.ToTable(EfConstants.TableNames.Scope);
                b.HasMany(e => e.ScopeClaims).WithOne(e => e.Scope).OnDelete(DeleteBehavior.Cascade);                
                b.HasMany(e => e.ScopeSecrets).WithOne(e => e.Scope).OnDelete(DeleteBehavior.Cascade);
                b.Property(e => e.Name).IsRequired().HasMaxLength(200);
                b.Property(e => e.DisplayName).HasMaxLength(200);
                b.Property(e => e.Description).HasMaxLength(1000);
                b.Property(e => e.ClaimsRule).HasMaxLength(200);
            });
        }
        //protected override void ConfigureChildCollections()
        //{
        //    this.Set<Scope>().Local.CollectionChanged +=
        //        delegate (object sender, NotifyCollectionChangedEventArgs e)
        //        {
        //            if (e.Action == NotifyCollectionChangedAction.Add)
        //            {
        //                foreach (Scope item in e.NewItems)
        //                {
        //                    RegisterDeleteOnRemove(item.ScopeClaims);
        //                }
        //            }
        //        };
        //}        
    }
}