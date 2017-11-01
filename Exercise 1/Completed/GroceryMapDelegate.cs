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
			var pinView = new MKMarkerAnnotationView (annotation, "pin");

			var storeAnnotation = annotation as StoreAnnotation;

			if (storeAnnotation != null && storeAnnotation.TimeOpen < 9)
                pinView.MarkerTintColor = UIColor.Purple;
			else if (storeAnnotation != null)
				pinView.MarkerTintColor = UIColor.Gray;

            return pinView;
		}
	}
}

