using System;

using UIKit;
using MapKit;
using CoreLocation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BananaFinder
{
	public partial class ViewController : UIViewController
	{
		public static CLLocationCoordinate2D currentLocation = new CLLocationCoordinate2D (49.28275, -123.12); 

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			map.Camera.CenterCoordinate = currentLocation;
			map.Camera.Altitude = 10000;

			var mapDelegate = new GroceryMapDelegate ();

			mapDelegate.MapViewChanged = () => SearchAsync (); 

			map.Delegate = mapDelegate;
		}

		public async Task SearchAsync ()
		{
			map.RemoveAnnotations (map.Annotations);//clear any existing results

			var request = new MKLocalSearchRequest ();
			request.NaturalLanguageQuery = "Grocery stores";
			request.Region = map.Region; //limit search to the current viewport

			var response = await new MKLocalSearch(request).StartAsync ();

			if (response != null && response.MapItems.Length > 0) 
			{
				var stores = new List<GroceryStore> ();

				foreach (var item in response.MapItems) {
					stores.Add(new GroceryStore()
						{
							Name = item.Name, 
							PhoneNumber = item.PhoneNumber, 
							Address = item.Placemark.Title,
							Longitude = item.Placemark.Location.Coordinate.Longitude, 
							Latitude = item.Placemark.Location.Coordinate.Latitude
						});
				}
				AddStoreAnnotations(stores);
			}
		}

		void AddStoreAnnotations (List<GroceryStore> stores)
		{
			foreach (var store in stores) {
				var annotation = new StoreAnnotation (store);
				map.AddAnnotation (annotation);
			}
		}
	}
}

