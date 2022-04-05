// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface LoginViewController : NSViewController {
	NSTextField *_EmailTextField;
	NSSecureTextField *_PasswordTextField;
}

@property (nonatomic, retain) IBOutlet NSTextField *EmailTextField;

@property (nonatomic, retain) IBOutlet NSSecureTextField *PasswordTextField;

- (IBAction)LogInButtonClicked:(id)sender;

@end
