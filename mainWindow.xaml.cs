using Parse;
namespace Projeto1;

public partial class mainWindow : ContentPage
{

	public mainWindow()
	{
		InitializeComponent();
        
    }

    private async void next(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new loginWindow());
    }
}