using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ToysAndGames.Tests.UnitTests.Fixtures
{
    [CollectionDefinition("Product")]
    public class ProductFixtureCollection : ICollectionFixture<ProductFixture>
    {
    }
}
