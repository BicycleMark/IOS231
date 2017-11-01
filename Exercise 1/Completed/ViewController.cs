using System;

using UIKit;
using MapKit;
using CoreLocation;

namespace BananaFinder
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			map.Camera.CenterCoordinate = new CLLocationCoordinate2D (49.28275, -123.12);
			map.Camera.Altitude = 10000;

			map.Delegate = new GroceryMapDelegate ();

			/*
			//GetViewForAnnotation delegate property 
			//Can't be used if a map delegate instance is assigned to the map view
			map.GetViewForAnnotation = (mapView, annotation) => {
				var pinView = new MKMarkerAnnotationView (annotation, "pin");
				pinView.MarkerTintColor = UIColor.Purple;
				return pinView;
			}; 
			*/

			AddStoreAnnotations ();
		}

		void AddStoreAnnotations ()
		{
			var stores = StoreFactory.GetStores ();

			foreach (var store in stores) 
			{
				var annotation = new StoreAnnotation (store);
				map.AddAnnotation (annotation);
			}
		}
	}
}

