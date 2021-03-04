using System.Collections.Generic;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Data
{
    public static class Inventory
    {
        public static IEnumerable<InventoryItem> AvailableItems
        {
            get
            {
                return new List<InventoryItem>()
                {
                    new InventoryItem()
                    {
                        Id = 1,
                        Name="Banane",
                        PricePerKg = 1.00M
                    },
                    new InventoryItem()
                    {
                        Id = 2,
                        Name="Cigliegie",
                        PricePerKg = 3.00M
                    },
                    new InventoryItem()
                    {
                        Id = 3,
                        Name="Fragole",
                        PricePerKg = 3.50M
                    },
                    new InventoryItem()
                    {
                        Id = 4,
                        Name="Mele",
                        PricePerKg = 2.50M
                    },
                    new InventoryItem()
                    {
                        Id = 5,
                        Name = "Pere",
                        PricePerKg = 2.50M
                    },
                };
            }
        }

        public class InventoryItem
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal PricePerKg { get; set; }
        }
    }
}
