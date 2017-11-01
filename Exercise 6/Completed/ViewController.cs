using System;

using UIKit;
using MapKit;
using CoreLocation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using System.Diagnostics;
using System.Linq;

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

			var mapDelegate = new GroceryMapDelegate ();
			mapDelegate.MapViewChanged += () => SearchAsync ();

			map.Camera.CenterCoordinate = currentLocation;
			map.Camera.Altitude = 10000;
			map.Delegate = mapDelegate;
		}

		public async Task SearchAsync ()
		{
			var request = new MKLocalSearchRequest ();
			request.NaturalLanguageQuery = "Grocery stores";
			request.Region = map.Region;//we'll search on the current screen

			var local = new MKLocalSearch(request);

			var response = await local.StartAsync ();

			if (response != null && response.MapItems.Length > 0) 
			{
				var stores = new List<GroceryStore> ();

				foreach (var item in response.MapItems) {
					stores.Add(new GroceryStore()
						{
							Name = item.Name, PhoneNumber = item.PhoneNumber, Address = item.Placemark.Title,
							Longitude = item.Placemark.Location.Coordinate.Longitude, Latitude = item.Placemark.Location.Coordinate.Latitude
						});
				}
				AddStoreAnnotations(stores);
			}
		}

        void AddStoreAnnotations(List<GroceryStore> stores)
        {
            foreach (var store in stores)
            {
                var storeAnnotation = new StoreAnnotation(store);
                //make sure we dont already have the annotation
                bool alreadyContainsAnnotation = false;
                foreach (var annotation in map.Annotations)
                {
                    StoreAnnotation mapAnnotationAsStore = annotation as StoreAnnotation;
                    if (mapAnnotationAsStore != null)
                    {
                        if (mapAnnotationAsStore.Address == store.Address)
                        {
                            alreadyContainsAnnotation = true;
                            break;
                        }
                    }
                }
                if (!alreadyContainsAnnotation)
                    map.AddAnnotation(storeAnnotation);
            }
        }

	}
}

