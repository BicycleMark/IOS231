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
			//pin recylcing
			var pinView = mapView.DequeueReusableAnnotation ("pin");

			if (pinView == null) {
				pinView = new MKAnnotationView (annotation, "pin");
				//custom pin image
				pinView.Image = UIImage.FromBundle ("banana_pin.png");

				//by default, MKMapView assumes the center our image represents the location
				//our image uses the buttom center so we need to offset in the Y direction by half the height
				pinView.CenterOffset = new CoreGraphics.CGPoint (0, -20);
			} else {
				pinView.Annotation = annotation;
			}
			return pinView;
		}
	}
}

