using System;
using MapKit;
using UIKit;
using System.Diagnostics;
using CoreLocation;
using Foundation;

namespace BananaFinder
{
	public class GroceryMapDelegate : MKMapViewDelegate
	{
		public Action MapViewChanged { get; set; }

		MKPolyline route;

		public GroceryMapDelegate ()
		{
		}

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			if (annotation.GetType () == typeof(MKPointAnnotation)) {
				return new MKAnnotationView (annotation, "me") { Image = UIImage.FromBundle ("monkey.png") };
			}

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

			var msg = annotation.Address;

			new UIAlertView (annotation.GetTitle (), msg, null, "OK", null).Show ();
		}

		public override void RegionChanged (MKMapView mapView, bool animated)
		{
			Debug.WriteLine ("RegionChanged");

			if (MapViewChanged != null)
				MapViewChanged ();
		}

		public override MKOverlayRenderer OverlayRenderer (MKMapView mapView, IMKOverlay overlay)
		{
			if (overlay is MKCircle) 
			{
				var renderer = new MKCircleRenderer ((MKCircle)overlay) { FillColor = UIColor.FromRGBA (0.0f, 0.5f, 1.0f, 0.5f) };
				return renderer;
			}

			if (overlay is MKPolyline)
            {
               	var route = (MKPolyline)overlay;
				var renderer = new MKPolylineRenderer(route) { StrokeColor = UIColor.Red, LineWidth = 3.0f };
                return renderer;
            }
            return null;
		}

		public override void DidSelectAnnotationView (MKMapView mapView, MKAnnotationView view)
		{
			if (view.Annotation is StoreAnnotation == false)
				return;

			CLLocationCoordinate2D coord;

			coord = view.Annotation.Coordinate;

		    coord = ((StoreAnnotation)view.Annotation).Coordinate;

			var destination = new MKMapItem (new MKPlacemark (coord, (MKPlacemarkAddress)null));
			ShowDirections (destination, mapView);
		}

		void ShowDirections (MKMapItem destination, MKMapView mapView)
		{
			var source = new MKMapItem (new MKPlacemark (ViewController.currentLocation, (MKPlacemarkAddress)null));

			var request = new MKDirectionsRequest () {
				Destination = destination,
				Source = source,
				RequestsAlternateRoutes = false,
			};

			var directions = new MKDirections (request);

			directions.CalculateDirections ((MKDirectionsResponse response, NSError e) => {

				if(this.route != null)
					mapView.RemoveOverlay(this.route);

				if(response == null || response.Routes.Length == 0)
					return;

				//save the overlay so we can remove it next time we draw
				route = response.Routes[0].Polyline;

				mapView.AddOverlay(route, MKOverlayLevel.AboveRoads);
			});
		}
	}
}

