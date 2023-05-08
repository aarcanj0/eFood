using Android.App;
using Android.Runtime;

namespace Projeto1;

[Application]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
