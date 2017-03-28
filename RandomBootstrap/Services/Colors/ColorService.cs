using System.IO;
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
    }
}
