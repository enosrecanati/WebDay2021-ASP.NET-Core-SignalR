using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebDay2021.SignalR.SimpleFruitShop.WebApp.Data;
using WebDay2021.SignalR.SimpleFruitShop.WebApp.Models;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Controllers
{
    [Authorize]
    public class SalesMonitorController : Controller
    {
        public IActionResult Index()
        {
            var model = new SalesMonitorIndexViewModel()
            {
                Products = Inventory.AvailableItems.Select(i => new SalesMonitorIndexViewModel.SoldProduct()
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList()
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult IndexAnonymous()
        {
            var model = new SalesMonitorIndexViewModel()
            {
                Products = Inventory.AvailableItems.Select(i => new SalesMonitorIndexViewModel.SoldProduct()
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList()
            };

            return View("Index", model);
        }
    }
}
