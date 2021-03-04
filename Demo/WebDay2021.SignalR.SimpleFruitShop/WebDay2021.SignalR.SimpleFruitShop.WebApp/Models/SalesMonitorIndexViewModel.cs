using System.Collections.Generic;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Models
{
    public class SalesMonitorIndexViewModel
    {
        public List<SoldProduct> Products { get; set; }

        public class SoldProduct
        {
            public int Id { get; set; }
            
            public string Name { get; set; }

            public int TotalKgSold { get; set; }

            public int TotalRevenue { get; set; }
        }
    }
}
