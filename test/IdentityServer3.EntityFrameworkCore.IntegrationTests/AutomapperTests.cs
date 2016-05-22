using AutoMapper;
using System.Collections.Generic;
using IdentityServer3.EntityFrameworkCore.Entities;
using IdentityServer3.EntityFrameworkCore.Extensions;
using Xunit;
using Models = IdentityServer3.Core.Models;

namespace IdentityServer3.EntityFrameworkCore.IntegrationTests
{
    public class AutomapperTests
    {
        [Fact]
        public void AutomapperConfigurationIsValid()
        {
            Models.Scope s = new Models.Scope() { };

            var e = ModelsMap.ToEntity(s);

            var s2 = new Scope()
            {
                ScopeClaims = new HashSet<ScopeClaim>()
            };
            var m = s2.ToModel();

            Mapper.AssertConfigurationIsValid();
        }
    }
}