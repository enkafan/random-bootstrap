using System.Threading.Tasks;
using RandomBootstrap.Services.Fonts;
using Xunit;

namespace RandomBootstrap.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Can_get_font_pairs()
        {
            var fs = new FontService();
            var pairs = await fs.GetPairs();
            Assert.NotEmpty(pairs);

        }
    }
}
