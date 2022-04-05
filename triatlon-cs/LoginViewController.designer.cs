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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		AppKit.NSTextField EmailTextField { get; set; }

		[Outlet]
		AppKit.NSSecureTextField PasswordTextField { get; set; }

		[Action ("LogInButtonClicked:")]
		partial void LogInButtonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}
		}
	}
}
