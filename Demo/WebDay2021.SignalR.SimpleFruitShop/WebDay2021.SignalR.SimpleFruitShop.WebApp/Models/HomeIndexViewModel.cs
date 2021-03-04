using System.Collections.Generic;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Models
{
    public class HomeIndexViewModel
    {
        public List<ChartItem> AvailableProducts { get; set; }

        public class ChartItem
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal PricePerKg { get; set; }

            public int QuantityInKg { get; set; }
        }
    }
}
