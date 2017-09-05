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

			//position the map over downtown Vancouver
			map.Camera.CenterCoordinate = new CLLocationCoordinate2D (49.28275, -123.12);
			map.Camera.Altitude = 10000;
		}
	}
}

