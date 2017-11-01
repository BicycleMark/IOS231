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

