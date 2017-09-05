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

			map.Delegate = mapDelegate;
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

