using System;
using System.Collections.Generic;

namespace BananaFinder
{
	public class GroceryStore
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public double TimeOpen { get; set; }
		public double TimeClosed { get; set; }

		public GroceryStore ()
		{
		}
	}

	public class StoreFactory 
	{
		static List<GroceryStore> stores;

		public static List<GroceryStore> GetStores ()
		{
			if (stores != null)
				return stores;

			stores = new List<GroceryStore> ();

			stores.Add (new GroceryStore() {Name = "Dave's Grocery", Description = "Great groceries and a great price", Latitude = 49.28, Longitude=-123.11, TimeOpen = 9, TimeClosed = 5});
				
			stores.Add (new GroceryStore() {Name = "Mary's Produce", Description = "The finest produce this side of the Rockies", Latitude = 49.28, Longitude=-123.12, TimeOpen = 10, TimeClosed = 6});

			stores.Add (new GroceryStore() {Name = "Fruitland", Description = "We do fruit, only better", Latitude = 49.28, Longitude=-123.13, TimeOpen = 8, TimeClosed = 4});

			stores.Add (new GroceryStore() {Name = "Wonder Veggies", Description = "Never compromise on the quality of your veggies", Latitude = 49.275, Longitude=-123.11, TimeOpen = 8, TimeClosed = 8});
	
			stores.Add (new GroceryStore() {Name = "Bananarama", Description = "The biggest banana selection in the west", Latitude = 49.275, Longitude=-123.12, TimeOpen = 10, TimeClosed = 10});

			stores.Add (new GroceryStore() {Name = "Quicky Veggy", Description = "Get your shopping done - quick", Latitude = 49.275, Longitude=-123.13, TimeOpen = 0, TimeClosed = 24});

			return stores;
		}
	}
}

