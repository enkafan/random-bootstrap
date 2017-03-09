using System;
using System.Text;
using LibSassHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;

namespace RandomBootstrap
{
    public interface IBootstrapRandomGenerator
    {
        /// <summary>
        /// Creates a random _theme.scss type string with variable overrides
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        string CreateRandom(int seed);

        /// <summary>
        /// Applies the generated SCSS created with <see cref="CreateRandom"/>
        /// to the bootstrap scss
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        string GetBootstrap(int seed);
    }

    public class BootstrapRandomGenerator : IBootstrapRandomGenerator
    {
        private readonly IHostingEnvironment _env;
        private readonly IMemoryCache _memoryCache;

        public BootstrapRandomGenerator(IHostingEnvironment env, IMemoryCache memoryCache)
        {
            _env = env;
            _memoryCache = memoryCache;
        }

        public string CreateRandom(int seed)
        {
            var random = new Random(seed);
            var stringBuilder = new StringBuilder();

            var rounded = random.Bool();
            var shadows = random.Bool();
            var gradients = random.Bool();

            stringBuilder.AppendLine($"$enable-rounded: {rounded.ToString().ToLower()} !default;");
            stringBuilder.AppendLine($"$enable-shadows: {shadows.ToString().ToLower()} !default;");
            stringBuilder.AppendLine($"$enable-gradients: {gradients.ToString().ToLower()} !default;");

            return stringBuilder.ToString();
        }

        public string GetBootstrap(int seed)
        {
            var bootstrapPath = _memoryCache.GetOrCreate("bootstrapPath", _ =>
            {
                var webRoot = _env.WebRootPath;
                return System.IO.Path.Combine(webRoot, "lib/bootstrap/scss");
            });

            var cacheKey = "bootstrap-" + seed;
            return _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                var scss = CreateRandom(seed);
                var options = new CompilationOptions { IncludePaths = new[] { bootstrapPath } };
                var input = scss + @"
@import 'variables';
@import 'bootstrap';
";
                var result = SassCompiler.Compile(input, options);

                return result.CompiledContent;
            });
        }
    }

    public static class RandomExtensions
    {
        public static bool Bool(this Random random, int probability = 50)
        {
            return random.Next(100) <= probability;
        }
    }
}
