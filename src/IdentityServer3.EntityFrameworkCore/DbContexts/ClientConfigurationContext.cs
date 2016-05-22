using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using IdentityServer3.EntityFrameworkCore.Entities;

namespace IdentityServer3.EntityFrameworkCore.DbContexts
{
 
    public class ClientConfigurationContext : BaseContext
    {
        public ClientConfigurationContext(DbContextOptions options)
            : base(options)
        { }


        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(b =>
            {
                b.ToTable(EfConstants.TableNames.Client);
                b.HasMany(e => e.ClientSecrets).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.RedirectUris).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.PostLogoutRedirectUris).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedScopes).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.IdentityProviderRestrictions).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedCustomGrantTypes).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.ClientSecrets).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedCorsOrigins).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasIndex(e => e.ClientId).IsUnique();
                b.Property(e => e.ClientId).IsRequired().HasMaxLength(200);
                b.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
                b.Property(e => e.ClientUri).HasMaxLength(2000);
            });                

            modelBuilder.Entity<ClientClaim>(b =>
            {
                b.ToTable(EfConstants.TableNames.ClientClaim);
                b.Property(e => e.Type).IsRequired().HasMaxLength(250);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
            });                

            modelBuilder.Entity<ClientCorsOrigin>()
                .ToTable(EfConstants.TableNames.ClientCorsOrigin)
                .Property(e => e.Origin).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<ClientCustomGrantType>()
                .ToTable(EfConstants.TableNames.ClientCustomGrantType)
                .Property(e => e.GrantType).IsRequired().HasMaxLength(250);

            modelBuilder.Entity<ClientPostLogoutRedirectUri>()
                .ToTable(EfConstants.TableNames.ClientPostLogoutRedirectUri)
                .Property(e => e.Uri).IsRequired().HasMaxLength(2000);

            modelBuilder.Entity<ClientIdPRestriction>()
                .ToTable(EfConstants.TableNames.ClientProviderRestriction)
                .Property(e => e.Provider).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ClientRedirectUri>()
                .ToTable(EfConstants.TableNames.ClientRedirectUri)
                .Property(e => e.Uri).IsRequired().HasMaxLength(2000);

            modelBuilder.Entity<ClientScope>()
                .ToTable(EfConstants.TableNames.ClientScopes)
                .Property(e => e.Scope).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ClientSecret>(b =>
            {
                b.ToTable(EfConstants.TableNames.ClientSecret);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
                b.Property(e => e.Type).HasMaxLength(250);
                b.Property(e => e.Description).HasMaxLength(2000);
            });                
        }

        //protected override void ConfigureChildCollections()
        //{
        //    this.Set<Client>().Local.CollectionChanged +=
        //        delegate (object sender, NotifyCollectionChangedEventArgs e)
        //        {
        //            if (e.Action == NotifyCollectionChangedAction.Add)
        //            {
        //                foreach (Client item in e.NewItems)
        //                {
        //                    RegisterDeleteOnRemove(item.ClientSecrets);
        //                    RegisterDeleteOnRemove(item.RedirectUris);
        //                    RegisterDeleteOnRemove(item.PostLogoutRedirectUris);
        //                    RegisterDeleteOnRemove(item.AllowedScopes);
        //                    RegisterDeleteOnRemove(item.IdentityProviderRestrictions);
        //                    RegisterDeleteOnRemove(item.Claims);
        //                    RegisterDeleteOnRemove(item.AllowedCustomGrantTypes);
        //                    RegisterDeleteOnRemove(item.AllowedCorsOrigins);
        //                }
        //            }
        //        };
        //}
    }
}