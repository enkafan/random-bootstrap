using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace RandomBootstrap.Services.Colors
{
    public class ColorService : IColorService
    {
        private readonly IHostingEnvironment _env;

        public ColorService (IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<MaterialDesignColors> GetMaterialDesignColors()
        {
            var filePath = Path.Combine(_env.WebRootPath, "material_design.json");

            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int) fileStream.Length);
                return JsonConvert.DeserializeObject<MaterialDesignColors>(Encoding.ASCII.GetString(result));
            }
        }

        public async Task<HarmonyColor[]> GetHarmonyColors()
        {
            var filePath = Path.Combine(_env.WebRootPath, "colors.json");

            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length);
                return JsonConvert.DeserializeObject<HarmonyColor[]>(Encoding.ASCII.GetString(result));
            }
        }

        public (HarmonyColor[] Lights, HarmonyColor[] Darks, IDictionary<string, HarmonyColor> ColorsByNumber) SpiltIntoLightAndDark(HarmonyColor[] input)
        {
            var colorsByNumber = input.ToDictionary(i => i.Number, i => i);

            var lights = input
                .Where(i => i.ContrastColor == "dark" && i.Shades.Any(c => colorsByNumber[c].ContrastColor == "light") && i.AllHarmonyColors().Any(c => colorsByNumber[c].ContrastColor == "light"))
                .ToArray();

            var darks = input
                .Where(i => i.ContrastColor == "light" && i.AllHarmonyColors().Any(c => colorsByNumber[c].ContrastColor == "light"))
                .ToArray();

            return (lights, darks, colorsByNumber);
        }
    }
}
