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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableView AthletesTableView { get; set; }

		[Outlet]
		AppKit.NSTableColumn IdColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn NameColumn { get; set; }

		[Outlet]
		AppKit.NSTextField NameLabel { get; set; }

		[Outlet]
		AppKit.NSTableColumn PointsColumn { get; set; }

		[Outlet]
		AppKit.NSTextField RaceTypeLabel { get; set; }

		[Action ("AddResultButtonClicked:")]
		partial void AddResultButtonClicked (Foundation.NSObject sender);

		[Action ("LeaderboardButtonClicked:")]
		partial void LeaderboardButtonClicked (Foundation.NSObject sender);

		[Action ("LogOutButtonClicked:")]
		partial void LogOutButtonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RaceTypeLabel != null) {
				RaceTypeLabel.Dispose ();
				RaceTypeLabel = null;
			}

			if (AthletesTableView != null) {
				AthletesTableView.Dispose ();
				AthletesTableView = null;
			}

			if (IdColumn != null) {
				IdColumn.Dispose ();
				IdColumn = null;
			}

			if (NameColumn != null) {
				NameColumn.Dispose ();
				NameColumn = null;
			}

			if (PointsColumn != null) {
				PointsColumn.Dispose ();
				PointsColumn = null;
			}
		}
	}
}
