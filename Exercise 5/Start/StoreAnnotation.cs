using System;
using MapKit;
using CoreLocation;

namespace BananaFinder
{
	public class StoreAnnotation : MKPointAnnotation
	{
		GroceryStore store;

		public string Address { get {return store.Address; } }

		public override string Title { get { return store.Name; } }
		public override string Subtitle { get { return store.PhoneNumber; } }
		public override CLLocationCoordinate2D Coordinate 
		{
			get {return new CLLocationCoordinate2D(store.Latitude, store.Longitude); }
		}

		public StoreAnnotation (GroceryStore store)
		{
			this.store = store;
		}
	}
}

