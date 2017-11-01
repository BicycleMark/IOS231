using System;
using MapKit;
using CoreLocation;

namespace BananaFinder
{
	public class StoreAnnotation : MKPointAnnotation
	{
		GroceryStore store;

		public double TimeOpen {get { return store.TimeOpen; } }
		public double TimeClosed {get { return store.TimeClosed; } }

		public override string Title { get { return store.Name; } }
		public override string Subtitle { get { return store.Description; } }
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

