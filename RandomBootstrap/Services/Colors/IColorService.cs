using System.Threading.Tasks;

namespace RandomBootstrap.Services.Colors
{
    public interface IColorService
    {
        Task<MaterialDesignColors> GetMaterialDesignColors();
    }
}