// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface ViewController : NSViewController {
	NSTableView *_AthletesTableView;
	NSTableColumn *_IdColumn;
	NSTableColumn *_NameColumn;
	NSTextField *_NameLabel;
	NSTableColumn *_PointsColumn;
	NSTextField *_RaceTypeLabel;
}

@property (nonatomic, retain) IBOutlet NSTableView *AthletesTableView;

@property (nonatomic, retain) IBOutlet NSTableColumn *IdColumn;

@property (nonatomic, retain) IBOutlet NSTableColumn *NameColumn;

@property (nonatomic, retain) IBOutlet NSTextField *NameLabel;

@property (nonatomic, retain) IBOutlet NSTableColumn *PointsColumn;

@property (nonatomic, retain) IBOutlet NSTextField *RaceTypeLabel;

- (IBAction)AddResultButtonClicked:(id)sender;

- (IBAction)LeaderboardButtonClicked:(id)sender;

- (IBAction)LogOutButtonClicked:(id)sender;

@end
