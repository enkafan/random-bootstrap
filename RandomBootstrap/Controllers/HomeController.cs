using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomBootstrap.Models;
using RandomBootstrap.Services;

namespace RandomBootstrap.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Random Random = new Random();
        private readonly IBootstrapRandomGenerator _bootstrapRandomGenerator;

        public HomeController(IBootstrapRandomGenerator bootstrapRandomGenerator)
        {
            _bootstrapRandomGenerator = bootstrapRandomGenerator;
        }

        public async Task<IActionResult> Index(int? seed)
        {
            if (seed.HasValue)
            {
                var content = await _bootstrapRandomGenerator.CreateRandomAsync(seed.Value);
                return View(new RandomViewModel
                {
                    Content = content.Trim(),
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Alerts(int? seed)
        {
            if (seed.HasValue)
            {
                return View(new RandomViewModel
                {
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Breadcrumbs(int? seed)
        {
            if (seed.HasValue)
            {
                return View(new RandomViewModel
                {
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Buttons(int? seed)
        {
            if (seed.HasValue)
            {
                return View(new RandomViewModel
                {
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Modal(int? seed)
        {
            if (seed.HasValue)
            {
                return View(new RandomViewModel
                {
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public IActionResult Navs(int? seed)
        {
            if (seed.HasValue)
            {
                return View(new RandomViewModel
                {
                    Seed = seed.Value
                });
            }

            var newSeed = Random.Next();
            return Redirect(Url.Action("Index", new { Seed = newSeed }));
        }

        public async Task<IActionResult> Bootstrap(int seed)
        {
            var bootstrap = await _bootstrapRandomGenerator.GetBootstrapAsync(seed);
            return Content(bootstrap, "text/css");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
