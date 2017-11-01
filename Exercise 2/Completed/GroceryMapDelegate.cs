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
			var pinView = mapView.DequeueReusableAnnotation("pin");

			if (pinView == null)
			{
				pinView = new MKAnnotationView(annotation, "pin");
				pinView.Image = UIImage.FromBundle("banana_pin.png");
				pinView.CenterOffset = new CoreGraphics.CGPoint(0, -20);
			}
			else
			{
				pinView.Annotation = annotation;
			}

			return pinView;
          
		}
	}
}

