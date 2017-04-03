using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomBootstrap.Services.Colors
{
    public interface IColorService
    {
        Task<MaterialDesignColors> GetMaterialDesignColors();
        Task<HarmonyColor[]> GetHarmonyColors();

        (HarmonyColor[] Lights, HarmonyColor[] Darks, IDictionary<string, HarmonyColor> ColorsByNumber) SpiltIntoLightAndDark(HarmonyColor[] input);
    }
}