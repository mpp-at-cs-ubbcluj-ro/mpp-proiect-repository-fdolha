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
	[Register ("LeaderboardViewController")]
	partial class LeaderboardViewController
	{
		[Outlet]
		AppKit.NSTableColumn IDColumn { get; set; }

		[Outlet]
		AppKit.NSTableView LeaderboardTableView { get; set; }

		[Outlet]
		AppKit.NSTableColumn NameColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn PointsColumn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LeaderboardTableView != null) {
				LeaderboardTableView.Dispose ();
				LeaderboardTableView = null;
			}

			if (IDColumn != null) {
				IDColumn.Dispose ();
				IDColumn = null;
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
