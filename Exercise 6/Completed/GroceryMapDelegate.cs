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

		public override void RegionChanged (MKMapView mapView, bool animated)
		{
			Debug.WriteLine ("RegionChanged");

			if (MapViewChanged != null)
				MapViewChanged ();
		}

		public override MKOverlayRenderer OverlayRenderer (MKMapView mapView, IMKOverlay overlay)
		{
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
            StoreAnnotation storeAnnotation = view.Annotation as StoreAnnotation;
            if (storeAnnotation != null)
            {
                var coord = storeAnnotation.Coordinate;

                var destination = new MKMapItem(new MKPlacemark(coord, (MKPlacemarkAddress)null));
                ShowDirections(destination, mapView);
            }
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

