using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Func<MaterialDesignColors, (MaterialDesignColors.IColor Color, string Name)>[] ColorLookups = {
            c => (c.amber, "Amber"),
            c => (c.blue, "Blue"),
            c => (c.bluegrey, "Blue Grey"),
            c => (c.brown, "Brown"),
            c => (c.cyan, "Cyan"),
            c => (c.deeporange ,"Deep Orange"),
            c => (c.deeppurple, "Deep Purple"),
            c => (c.green, "Green"),
            c => (c.grey, "Grey"),
            c => (c.indigo, "Indigo"),
            c => (c.lightblue, "Light Blue"),
            c => (c.lightgreen, "Light Green"),
            c => (c.lime, "Lime"),
            c => (c.orange, "Orange"),
            c => (c.pink, "Pink"),
            c => (c.purple, "Purple"),
            c => (c.red, "Red"),
            c => (c.teal, "Teal"),
            c => (c.yellow, "Yellow")
        };

        private static readonly Func<MaterialDesignColors.IColor, (string Color, string Name)>[] Hues = {
            color => (color._700, "700"),
            color => (color._800, "800"),
            color => (color._600, "600"),
            color => (color._900, "900")
        };

        private static readonly string[] BaseFontSizes = {
            "87.5%",
            "93.8%",
            "100.0%"
        };

        private static readonly string[] NavbarShadows =
        {
            "none",
            "0px 2px 3px rgba(0, 0, 0, 0.04)",
            "0px 3px 5px rgba(0, 0, 0, 0.1)"
        };

        private static readonly string[] NavbarPadding =
        {
            ".5rem",
            ".75rem",
            "1rem"
        };

        private static readonly string[] NavbarBottomBorder =
        {
            "1px solid darken($navbar-bg-color, 10%)",
            "1px solid darken($navbar-bg-color, 25%)",
            "none"
        };


        private static readonly string[] PopOverBoxShadow =
        {
            "0 5px 10px rgba(0,0,0,.2)",
            "0 3px 6px rgba(0, 0, 0, 0.1)",
            "0 2px 4px 0 rgba(34,36,38,.12), 0 2px 10px 0 rgba(34,36,38,.15)"
        };

        private static readonly string[] PopOverTitleBackground =
        {
            "darken(#fff, 2%)",
            "darken(#fff, 3%)",
            "#fff",
        };

        private static readonly (int X, int Y)[] PopOverPaddings =
        {
            (14, 8),
            (10, 6)
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
            var harmonyColors = await _memoryCache.GetOrCreateAsync("harmony-colors", async entry => _colorService.SpiltIntoLightAndDark(await _colorService.GetHarmonyColors()));

            var random = new Random(seed);
            var stringBuilder = new StringBuilder();

            var rounded = random.Bool();
            var shadows = random.Bool();
            var gradients = random.Bool();
            var fontPair = random.PickItem(fontPairs);

            var color = random.PickItem(random.Bool() ? harmonyColors.Lights : harmonyColors.Darks);
            var isLight = color.ContrastColor == "dark";

            var navbarColor = color.Hex;

            string primaryColorHex;
            string primaryColorName;
            if (isLight)
            {
                var darkShade = random.PickItem(color.Shades.Select(i => harmonyColors.ColorsByNumber[i]).Where(i => i.ContrastColor == "light"));
                primaryColorHex = "#" + darkShade.Hex;
                primaryColorName = darkShade.Name;
            }
            else
            {
                primaryColorHex = "#" + color.Hex;
                primaryColorName = color.Name;
            }

            var secondaryColor = harmonyColors.ColorsByNumber[random.PickItem(color.AllHarmonyColors().Where(i => harmonyColors.ColorsByNumber[i].ContrastColor == "light"))];
            var secondaryColorHex = "#" + secondaryColor.Hex;
            var secondaryColorName = secondaryColor.Name;

            var borderRadius = 20 + random.Next(0, 5) * 5;
            var popOverPadding = random.PickItem(PopOverPaddings);

            stringBuilder.AppendLine("// set the base font for the site. rem sizing will adjust the rest");
            stringBuilder.AppendLine($"html {{font-size: {random.PickItem(BaseFontSizes)};}}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// global settings");
            stringBuilder.AppendLine($"$enable-rounded: {rounded.ToString().ToLower()};");
            stringBuilder.AppendLine($"$enable-shadows: {shadows.ToString().ToLower()};");
            stringBuilder.AppendLine($"$enable-gradients: {gradients.ToString().ToLower()}; // current version of Bootstrap 4 doesn't support this");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// do some random font stuff. obviously you'll want fallback fonts here too");
            stringBuilder.AppendLine($"@import url('https://fonts.googleapis.com/css?family={fontPair.BodyForUrl}:400,400i,700,700i|{fontPair.HeadingForUrl}:400,400i,700,700i');");
            stringBuilder.AppendLine($"$font-family-base: {fontPair.BodyForCss};");
            stringBuilder.AppendLine($"$headings-font-family: {fontPair.HeadingForCss};");
            stringBuilder.AppendLine("$headings-font-weight: 700;");
            stringBuilder.AppendLine($"button, input, optgroup, select, textarea {{font-family:{fontPair.BodyForCss} !important;}} // hack until next release of Bootstrap 4");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// pick two random colors and use the material design greys");
            stringBuilder.AppendLine($"$gray-dark: {materialColors.grey._900};");
            stringBuilder.AppendLine($"$gray: {materialColors.grey._700};");
            stringBuilder.AppendLine($"$gray-light: {materialColors.grey._500};");
            stringBuilder.AppendLine($"$gray-lighter: {materialColors.grey._300};");
            stringBuilder.AppendLine($"$gray-lightest: {materialColors.grey._100};");

            stringBuilder.AppendLine($@"$brand-primary: {primaryColorHex}; // {primaryColorName}");
            stringBuilder.AppendLine($@"$brand-secondary: {secondaryColorHex}; // {secondaryColorName}");
            stringBuilder.AppendLine("$brand-success: $brand-secondary;");
            stringBuilder.AppendLine("$brand-info: $brand-secondary;");
            stringBuilder.AppendLine("$brand-warning: $brand-secondary;");
            stringBuilder.AppendLine("$brand-danger: $brand-secondary;");
            stringBuilder.AppendLine("$brand-inverse: $gray-dark;");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// tweak the border radius");
            stringBuilder.AppendLine($@"$border-radius: .{borderRadius}rem;");
            stringBuilder.AppendLine($"$border-radius-lg: .{borderRadius + 5}rem;");
            stringBuilder.AppendLine($"$border-radius-sm: .{borderRadius - 5}rem;");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// pop overs");
            stringBuilder.AppendLine($"$popover-arrow-width: {random.Next(8, 12)}px;");
            stringBuilder.AppendLine($"$popover-box-shadow: {random.PickItem(PopOverBoxShadow)};");
            stringBuilder.AppendLine($"$popover-title-bg: {random.PickItem(PopOverTitleBackground)};");
            stringBuilder.AppendLine($"$popover-title-padding-x: {popOverPadding.X}px;");
            stringBuilder.AppendLine($"$popover-title-padding-y: {popOverPadding.Y}px;");
            stringBuilder.AppendLine($"$popover-content-padding-x: {popOverPadding.X}px;");
            stringBuilder.AppendLine($"$popover-content-padding-y: {popOverPadding.Y + 1}px;");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("// the navbar is ubiquitious with bootstrap. at least tweak it a bit just to mix things up");
            stringBuilder.AppendLine($"$navbar-padding-y: {random.PickItem(NavbarPadding)};");

            stringBuilder.AppendLine($"$navbar-bg-color: #{navbarColor};");
            stringBuilder.AppendLine($".navbar {{ border-bottom: {random.PickItem(NavbarBottomBorder)}; box-shadow: {random.PickItem(NavbarShadows)}; }}");

            if (isLight)
            {
                stringBuilder.AppendLine($".navbar-custom {{@extend .navbar-light; background-color: #{navbarColor} !important;}}");
            }
            else
            {
                stringBuilder.AppendLine($".navbar-custom {{@extend .navbar-inverse; @extend .bg-primary;}}");
            }

            return stringBuilder.ToString();
        }

        private static bool IsDark(KeyValuePair<string, HarmonyColor> color,
            Dictionary<string, HarmonyColor> harmonyColors) => color.Value.ContrastColor == "light" && color.Value
                                                                   .AllHarmonyColors()
                                                                   .Select(c => harmonyColors[c])
                                                                   .Any(c => c.ContrastColor == "light");

        private static bool IsLightWithDarkShades(KeyValuePair<string, HarmonyColor> color, Dictionary<string, HarmonyColor> harmonyColors) => color.Value.ContrastColor == "dark" && color.Value.Shades.Any(i => harmonyColors[i].ContrastColor == "light");


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
                SassCompiler.FileManager = CustomFileManager.Instance;
                var result = SassCompiler.Compile(input, options);
                return result.CompiledContent;
            });
        }
    }

    internal class CustomFileManager : IFileManager
    {
        public static CustomFileManager Instance = new CustomFileManager();
        private static readonly IFileManager InnerFileManager = FileManager.Instance;

        public string GetCurrentDirectory()
        {
            return InnerFileManager.GetCurrentDirectory();
        }

        // we are going to to be reusing this over and over so try and cache everything
        // to speed it up
        private static readonly ConcurrentDictionary<string, bool> FileExistsConcurrentDictionary = new ConcurrentDictionary<string, bool>();
        public bool FileExists(string path) => FileExistsConcurrentDictionary.GetOrAdd(path, s => InnerFileManager.FileExists(s));

        private static readonly ConcurrentDictionary<string, bool> IsAbsolutePathDictionary = new ConcurrentDictionary<string, bool>();
        public bool IsAbsolutePath(string path) => IsAbsolutePathDictionary.GetOrAdd(path, s => InnerFileManager.IsAbsolutePath(s));

        private static readonly ConcurrentDictionary<string, string> ToAbsolutePathDictionary = new ConcurrentDictionary<string, string>();
        public string ToAbsolutePath(string path) => ToAbsolutePathDictionary.GetOrAdd(path, s => InnerFileManager.ToAbsolutePath(s));

        private static readonly ConcurrentDictionary<string, string> FileContentDictionary = new ConcurrentDictionary<string, string>();
        public string ReadFile(string path) => FileContentDictionary.GetOrAdd(path, s => InnerFileManager.ReadFile(s));

        public bool SupportsConversionToAbsolutePath => InnerFileManager.SupportsConversionToAbsolutePath;
    }
}
