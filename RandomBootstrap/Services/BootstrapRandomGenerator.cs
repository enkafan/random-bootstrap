using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibSassHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using RandomBootstrap.Services.Colors;
using RandomBootstrap.Services.Fonts;

namespace RandomBootstrap.Services
{
    public class BootstrapRandomGenerator : IBootstrapRandomGenerator
    {
        private static string _bootstrapPath;
        private readonly IMemoryCache _memoryCache;
        private readonly IFontService _fontService;
        private readonly IColorService _colorService;

        private static readonly Func<MaterialDesignColors, MaterialDesignColors.IColor>[] ColorLookups = new Func<MaterialDesignColors, MaterialDesignColors.IColor>[]
        {
            c => c.amber,
            c => c.blue,
            c => c.bluegrey,
            c => c.brown,
            c => c.cyan,
            c => c.deeporange,
            c => c.deeppurple,
            c => c.green,
            c => c.grey,
            c => c.indigo,
            c => c.lightblue,
            c => c.lightgreen,
            c => c.lime,
            c => c.orange,
            c => c.pink,
            c => c.purple,
            c => c.red,
            c => c.teal,
            c => c.yellow
        };
        private static readonly Func<MaterialDesignColors.IColor, string>[] Hues = new Func<MaterialDesignColors.IColor, string>[]
        {
            color => color._700,
            color => color._800,
            color => color._600
        };

        private static readonly string[] BaseFontSizes = new[]
        {
            "14px",
            "15px",
            "16px"
        };

        public BootstrapRandomGenerator(IHostingEnvironment env, IMemoryCache memoryCache, IFontService fontService, IColorService colorService)
        {
            if (_bootstrapPath == null)
            {
                _bootstrapPath = System.IO.Path.Combine(env.WebRootPath, "lib/bootstrap/scss");
            }

            _memoryCache = memoryCache;
            _fontService = fontService;
            _colorService = colorService;
        }

        public async Task<string> CreateRandomAsync(int seed)
        {
            var fontPairs = await _memoryCache.GetOrCreateAsync("font-pairs", async entry => await _fontService.GetPairs());
            var materialColors = await _memoryCache.GetOrCreateAsync("material-colors", async entry => await _colorService.GetMaterialDesignColors());

            var random = new Random(seed);
            var stringBuilder = new StringBuilder();

            var rounded = random.Bool();
            var shadows = random.Bool();
            var gradients = random.Bool();
            var fontPair = random.PickItem(fontPairs);
            var hue = random.PickItem(Hues);
            var primary = hue.Invoke(random.PickItem(ColorLookups).Invoke(materialColors));
            var secondary = hue.Invoke(random.PickItem(ColorLookups).Invoke(materialColors));
            var borderRadius = 20 + random.Next(0, 5) * 5;

            stringBuilder.AppendLine("// set the base font for the site. rem sizing will adjust the rest");
            stringBuilder.AppendLine($"html {{font-size: {random.PickItem(BaseFontSizes)};}}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// global settings");
            stringBuilder.AppendLine($"$enable-rounded: {rounded.ToString().ToLower()} !default;");
            stringBuilder.AppendLine($"$enable-shadows: {shadows.ToString().ToLower()} !default;");
            stringBuilder.AppendLine($"$enable-gradients: {gradients.ToString().ToLower()} !default;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// do some random font stuff. obviously you'll want fallback fonts here too");
            stringBuilder.AppendLine($"@import url('https://fonts.googleapis.com/css?family={fontPair.BodyForUrl}:400,400i,700,700i|{fontPair.HeadingForUrl}:400,400i,700,700i');");
            stringBuilder.AppendLine($"$font-family-base: {fontPair.BodyForCss} !default;");
            stringBuilder.AppendLine($"$headings-font-family: {fontPair.HeadingForCss} !default;");
            stringBuilder.AppendLine("$headings-font-weight: 700 !default;");
            stringBuilder.AppendLine($"button, input, optgroup, select, textarea {{font-family:{fontPair.BodyForCss} !important;}} // hack until next release of Bootstrap 4");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// pick two random material design colors and set the greys to the material design pallette");
            stringBuilder.AppendLine($@"$gray-dark: {materialColors.grey._900} !default;
$gray: {materialColors.grey._700} !default;
$gray-light: {materialColors.grey._500} !default;
$gray-lighter: {materialColors.grey._300} !default;
$gray-lightest: {materialColors.grey._100} !default;");

            stringBuilder.AppendLine($@"$brand-primary: {primary} !default;
$brand-success: {secondary} !default;
$brand-info: {secondary} !default;
$brand-warning: {secondary} !default;
$brand-danger: {secondary} !default;
$brand-inverse: $gray-dark !default;");

            stringBuilder.AppendLine("// slight changes to things like border radi");
            stringBuilder.AppendLine($@"$border-radius: .{borderRadius}rem !default;
$border-radius-lg: .{borderRadius + 5}rem !default;
$border-radius-sm: .{borderRadius - 5}rem !default;");
            return stringBuilder.ToString();
        }

        public async Task<string> GetBootstrapAsync(int seed)
        {
            var cacheKey = "bootstrap-" + seed;
            return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(5);

                var scss = await CreateRandomAsync(seed);
                var options = new CompilationOptions { IncludePaths = new[] { _bootstrapPath } };
                var input = scss + @"
@import 'variables';
@import 'bootstrap';
";
                var result = SassCompiler.Compile(input, options);
                return result.CompiledContent;
            });
        }
    }
}
