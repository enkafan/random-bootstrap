using System.Threading.Tasks;

namespace RandomBootstrap.Services.Fonts
{
    public interface IFontService
    {
        Task<FontPair[]> GetPairs();
    }
}