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
			MKAnnotationView view = mapView.DequeueReusableAnnotation ("pin");

			if (view == null) {
				view = new MKAnnotationView (annotation, "pin");
				view.Image = UIImage.FromBundle ("banana_pin.png");
				view.CenterOffset = new CoreGraphics.CGPoint (0, -20);

				view.CanShowCallout = true;
				view.LeftCalloutAccessoryView = new UIImageView (UIImage.FromBundle ("banana.png"));
				view.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);
			} else {
				view.Annotation = annotation;
			}

			return view;
		}

		public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
		{
			var annotation = view.Annotation as StoreAnnotation;

			if (annotation == null)
				return;

			var msg = String.Format ("Hours:\r\n{0} till {1}", annotation.TimeOpen, annotation.TimeClosed);

			new UIAlertView (annotation.Title, msg, null, "OK", null).Show();
		}
	}
}

























