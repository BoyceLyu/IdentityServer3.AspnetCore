using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;

namespace IdentityServer3.EntityFrameworkCore.Extensions
{
    public static class EntitiesMap
    {
        public static IMapper Mapper { get; set; }

        static EntitiesMap()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<Entities.Scope, IdentityServer3.Core.Models.Scope>(MemberList.Destination)
                    .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)))
                    .ForMember(x => x.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets.Select(x => x)));
                config.CreateMap<Entities.ScopeClaim, IdentityServer3.Core.Models.ScopeClaim>(MemberList.Destination);
                config.CreateMap<Entities.ScopeSecret, IdentityServer3.Core.Models.Secret>(MemberList.Destination)
                    .ForMember(dest => dest.Type, opt => opt.Condition(srs => !srs.IsSourceValueNull));

                config.CreateMap<Entities.ClientSecret, IdentityServer3.Core.Models.Secret>(MemberList.Destination)
                     .ForMember(dest => dest.Type, opt => opt.Condition(srs => !srs.IsSourceValueNull));
                config.CreateMap<Entities.Client, IdentityServer3.Core.Models.Client>(MemberList.Destination)
                    .ForMember(x => x.UpdateAccessTokenClaimsOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenOnRefresh))
                    .ForMember(x => x.AllowAccessToAllCustomGrantTypes, opt => opt.MapFrom(src => src.AllowAccessToAllGrantTypes))
                    .ForMember(x => x.AllowedCustomGrantTypes, opt => opt.MapFrom(src => src.AllowedCustomGrantTypes.Select(x => x.GrantType)))
                    .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Uri)))
                    .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Uri)))
                    .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                    .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope)))
                    .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)))
                    .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))));
            }).CreateMapper();
        }

        public static IdentityServer3.Core.Models.Scope ToModel( this Entities.Scope s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.ScopeClaims == null)
            {
                s.ScopeClaims = new List<Entities.ScopeClaim>();
            }
            if (s.ScopeSecrets == null)
            {
                s.ScopeSecrets = new List<Entities.ScopeSecret>();
            }

            return Mapper.Map<Entities.Scope, IdentityServer3.Core.Models.Scope>(s);
        }

        public static IdentityServer3.Core.Models.Client ToModel(this Entities.Client s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.ClientSecrets == null)
            {
                s.ClientSecrets = new List<Entities.ClientSecret>();
            }
            if (s.RedirectUris == null)
            {
                s.RedirectUris = new List<Entities.ClientRedirectUri>();
            }
            if (s.PostLogoutRedirectUris == null)
            {
                s.PostLogoutRedirectUris = new List<Entities.ClientPostLogoutRedirectUri>();
            }
            if (s.AllowedScopes == null)
            {
                s.AllowedScopes = new List<Entities.ClientScope>();
            }
            if (s.IdentityProviderRestrictions == null)
            {
                s.IdentityProviderRestrictions = new List<Entities.ClientIdPRestriction>();
            }
            if (s.Claims == null)
            {
                s.Claims = new List<Entities.ClientClaim>();
            }
            if (s.AllowedCustomGrantTypes == null)
            {
                s.AllowedCustomGrantTypes = new List<Entities.ClientCustomGrantType>();
            }
            if (s.AllowedCorsOrigins == null)
            {
                s.AllowedCorsOrigins = new List<Entities.ClientCorsOrigin>();
            }

            return Mapper.Map<Entities.Client, IdentityServer3.Core.Models.Client>(s);
        }
    }

}