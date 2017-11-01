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
            string resuseId = annotation is StoreAnnotation ? "pin" : "cluster";
            var pinView = mapView.DequeueReusableAnnotation(resuseId);

			if (pinView == null)
			{
                if (annotation is StoreAnnotation)
                {
                    pinView = new MKAnnotationView(annotation, "pin");
                    pinView.Image = UIImage.FromBundle("banana_pin.png");
                    pinView.CenterOffset = new CoreGraphics.CGPoint(0, -20);
                }
                else
                {
                    pinView = new MKMarkerAnnotationView(annotation, "cluster");
                }
			}
			else
			{
				pinView.Annotation = annotation;
			}
            pinView.ClusteringIdentifier = "banana";
			return pinView;
		}
	}
}

