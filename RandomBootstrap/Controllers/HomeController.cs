using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibSassHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RandomBootstrap.Models;

namespace RandomBootstrap.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Random Random = new Random();
        private readonly IHostingEnvironment _env;
        private readonly IBootstrapRandomGenerator _bootstrapRandomGenerator;

        public HomeController(IHostingEnvironment env, IBootstrapRandomGenerator bootstrapRandomGenerator)
        {
            _env = env;
            _bootstrapRandomGenerator = bootstrapRandomGenerator;
        }

        public IActionResult Index(int? seed)
        {
            if (seed.HasValue)
            {
                var content = _bootstrapRandomGenerator.CreateRandom(seed.Value);
                return View(new RandomViewModel
                {
                    Content = content.Trim(),
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Bootstrap(int seed)
        {
            var bootstrap = _bootstrapRandomGenerator.GetBootstrap(seed);
            return Content(bootstrap, "text/css");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
