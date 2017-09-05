using System;
using MapKit;
using UIKit;

namespace BananaFinder
{
	public class GroceryMapDelegate : MKMapViewDelegate
	{
		public GroceryMapDelegate ()
		{
		}

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			var pinView = new MKPinAnnotationView (annotation, "pin");

			var storeAnnotation = annotation as StoreAnnotation;

			if (storeAnnotation != null && storeAnnotation.TimeOpen < 9)
				pinView.PinColor = MKPinAnnotationColor.Purple;//iOS6
			else if (storeAnnotation != null)
				pinView.PinTintColor = UIColor.Gray;//iOS9+

			return pinView;
		}
	}
}

