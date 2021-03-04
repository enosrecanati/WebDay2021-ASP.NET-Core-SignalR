using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebDay2021.SignalR.SimpleFruitShop.WebApp.Data;
using WebDay2021.SignalR.SimpleFruitShop.WebApp.Hubs;
using WebDay2021.SignalR.SimpleFruitShop.WebApp.Models;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHubContext<MonitorHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<MonitorHub> hubContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel()
            {
                AvailableProducts = Inventory.AvailableItems.Select(i => new HomeIndexViewModel.ChartItem()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PricePerKg = i.PricePerKg
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexViewModel model)
        {
            foreach (var soldProduct in model.AvailableProducts.Where(p => p.QuantityInKg > 0))
            {
                await _hubContext.Clients.All.SendAsync("ProductSold", soldProduct.Id, soldProduct.QuantityInKg, soldProduct.PricePerKg);
            }

            return RedirectToAction(nameof(HomeController.Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}