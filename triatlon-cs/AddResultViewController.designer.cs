// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace triatloncs
{
	[Register ("AddResultViewController")]
	partial class AddResultViewController
	{
		[Outlet]
		AppKit.NSPopUpButton AthletesPopUpButton { get; set; }

		[Outlet]
		AppKit.NSTextField PointsTextField { get; set; }

		[Action ("AddButtonClicked:")]
		partial void AddButtonClicked (Foundation.NSObject sender);

		[Action ("CloseButtonClicked:")]
		partial void CloseButtonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AthletesPopUpButton != null) {
				AthletesPopUpButton.Dispose ();
				AthletesPopUpButton = null;
			}

			if (PointsTextField != null) {
				PointsTextField.Dispose ();
				PointsTextField = null;
			}
		}
	}
}
