// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface LeaderboardViewController : NSViewController {
	NSTableColumn *_IDColumn;
	NSTableView *_LeaderboardTableView;
	NSTableColumn *_NameColumn;
	NSTableColumn *_PointsColumn;
}

@property (nonatomic, retain) IBOutlet NSTableColumn *IDColumn;

@property (nonatomic, retain) IBOutlet NSTableView *LeaderboardTableView;

@property (nonatomic, retain) IBOutlet NSTableColumn *NameColumn;

@property (nonatomic, retain) IBOutlet NSTableColumn *PointsColumn;

@end
