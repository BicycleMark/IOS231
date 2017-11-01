using System;
using MapKit;
using UIKit;
using System.Diagnostics;

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

                    pinView.CanShowCallout = true;
                    pinView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("banana.png"));
                    pinView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
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

		public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
		{
			var annotation = view.Annotation as StoreAnnotation;

			if (annotation == null)
				return;

			var msg = annotation.Address;

			new UIAlertView (annotation.GetTitle (), msg, null, "OK", null).Show ();
		}
	}
}

