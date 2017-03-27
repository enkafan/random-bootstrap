using System.Linq;
using System.Threading.Tasks;
using AngleSharp;

namespace RandomBootstrap.Services.Fonts
{
    public class FontService : IFontService
    {
        public async Task<FontPair[]> GetPairs()
        {
            const string address = "http://fontpair.co/index.html";

            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var groupings = document.QuerySelectorAll(".unit");

            // unit is being used for almost everything on the page so make sure we filter
            // out the ones that aren't font pairs
            var pairs = groupings.Select(grouping => grouping.QuerySelectorAll(".small").ToArray())
                .Where(items => items.Length == 3 && items[0].TextContent.StartsWith("Heading: ") && items[1].TextContent.StartsWith("Body: "))
                .Select(items => new FontPair(items[0].TextContent.Replace("Heading: ", ""),items[1].TextContent.Replace("Body: ", ""))).ToList();

            return pairs.ToArray();
        }
    }
}
