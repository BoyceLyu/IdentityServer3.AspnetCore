using System.Linq;
using System.Reflection;
using IdentityServer3.EntityFrameworkCore.DbContexts;
using Xunit;

namespace IdentityServer3.EntityFrameworkCore.Tests
{
    public class NameLengthTests
    {
        [Fact]
        public void NamesAreNotMoreThan30Chars()
        {
            var assembly = Assembly.GetAssembly(typeof(ClientConfigurationContext));
            var query =
                from t in assembly.GetTypes()
                where t.Namespace == "IdentityServer3.EntityFrameworkCore.Entities"
                select t;
            foreach (var type in query)
            {
                Assert.True(type.Name.Length <= 30, type.Name);

                foreach (var prop in type.GetProperties())
                {
                    Assert.True(prop.Name.Length <= 30, type.Name + ". " + prop.Name);
                }
            }

        }
    }
}
