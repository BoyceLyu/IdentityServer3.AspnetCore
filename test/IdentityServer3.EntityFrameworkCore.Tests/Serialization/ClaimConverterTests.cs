using IdentityServer3.Core;
using Newtonsoft.Json;
using System.Security.Claims;
using IdentityServer3.EntityFrameworkCore.Serialization;
using Xunit;

namespace IdentityServer3.EntityFrameworkCore.Tests.Serialization
{
    public class ClaimConverterTests
    {
        [Fact]
        public void CanSerializeAndDeserializeAClaim()
        {
            var claim = new Claim(Constants.ClaimTypes.Subject, "alice");

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            var json = JsonConvert.SerializeObject(claim, settings);

            claim = JsonConvert.DeserializeObject<Claim>(json, settings);
            Assert.Equal(Constants.ClaimTypes.Subject, claim.Type);
            Assert.Equal("alice", claim.Value);
        }
    }
}