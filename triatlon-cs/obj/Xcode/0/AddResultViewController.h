// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface AddResultViewController : NSViewController {
	NSPopUpButton *_AthletesPopUpButton;
	NSTextField *_PointsTextField;
}

@property (nonatomic, retain) IBOutlet NSPopUpButton *AthletesPopUpButton;

@property (nonatomic, retain) IBOutlet NSTextField *PointsTextField;

- (IBAction)AddButtonClicked:(id)sender;

- (IBAction)CloseButtonClicked:(id)sender;

@end
