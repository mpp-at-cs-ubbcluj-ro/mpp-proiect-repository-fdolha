using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace triatloncs
{
    public partial class ViewController : NSViewController
    {
        private TriatlonService service = DependencyProvider.Shared.GetSharedService();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            var dataSource = new AthletesResultsDataSource();
            AthletesTableView.DataSource = dataSource;
            AthletesTableView.Delegate = new AthletesResultsDelegate(dataSource);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Referee referee = service.CurrentReferee;
            NameLabel.StringValue = referee.FirstName + " " + referee.LastName;
            RaceTypeLabel.StringValue = "Arbitru " + referee.RaceType;
        }

        partial void LogOutButtonClicked(Foundation.NSObject sender)
        {
            service.LogOutReferee();
            var storyboard = NSStoryboard.FromName("Main", null);
            var controller = storyboard.InstantiateControllerWithIdentifier("LoginWindow") as NSWindowController;
            View.Window.OrderOut(Self);
            controller.ShowWindow(this);
        }

        partial void LeaderboardButtonClicked(Foundation.NSObject sender)
        {
            var storyboard = NSStoryboard.FromName("Main", null);
            var controller = storyboard.InstantiateControllerWithIdentifier("LeaderboardWindow") as NSWindowController;
            controller.ShowWindow(this);
        }

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            switch (segue.Identifier)
            {
                case "AddResultSegue":
                    var dialog = segue.DestinationController as AddResultViewController;
                    dialog.Presentor = this;
                    break;
            }
        }
    }

    public class AthletesResultsDataSource : NSTableViewDataSource
    {
        public List<Result> AthletesResults = DependencyProvider.Shared.GetSharedService().GetAthletesWithTotalPoints();

        public override nint GetRowCount(NSTableView tableView)
        {
            return AthletesResults.Count;
        }
    }

    public class AthletesResultsDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "AthletesResultsCell";
        private AthletesResultsDataSource DataSource;

        public AthletesResultsDelegate(AthletesResultsDataSource dataSource)
        {
            this.DataSource = dataSource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            NSTextField view = (NSTextField) tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Editable = false;
            }

            switch (tableColumn.Title)
            {
                case "ID":
                    view.StringValue = DataSource.AthletesResults[(int)row].Id.ToString();
                    break;
                case "Nume":
                    view.StringValue = DataSource.AthletesResults[(int)row].Name;
                    break;
                case "Total Puncte":
                    view.StringValue = DataSource.AthletesResults[(int)row].Points.ToString();
                    break;
            }

            return view;
        }
    }
}
