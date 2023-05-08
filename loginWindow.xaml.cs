using Parse;

namespace Projeto1;

public partial class loginWindow : ContentPage
{
	public loginWindow()
	{
		InitializeComponent();
        ParseClient.Initialize(new ParseClient.Configuration
        {
		//back4app
            Server = "https://parseapi.back4app.com",
            ApplicationId = "0000",
            WindowsKey = "0000"
        });

    }

    private async void entrarClicked(object sender, EventArgs e)
    {

        this.activity1.IsRunning = true;
        this.entrarBtn.IsEnabled = false;
        this.criarcontaBtn.IsEnabled = false;
        
        string username = this.userEntry.Text;
        string password = this.passwordEntry.Text;

        try
        {
            await ParseUser.LogInAsync(username, password);
            this.activity1.IsRunning = false;
            this.entrarBtn.IsEnabled = true;
            this.criarcontaBtn.IsEnabled = true;
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("Invalid username/password."))
            {
                await DisplayAlert("Erro de autenticação", "Nome de usuário ou/e senha são inválidas\nVerifique se você digitou tudo corretamente", "OK");
                this.activity1.IsRunning = false;
                this.entrarBtn.IsEnabled = true;
                this.criarcontaBtn.IsEnabled = true;

            }
            else if (ex.Message.Equals("Object reference not set to an instance of an object."))
            {
                await DisplayAlert("Não foi possível conectar", "Não foi possível fazer login na sua conta.\nVerifique sua conecção e tente novamente!\n\nErro:\n" + ex.Message, "OK");
                this.activity1.IsRunning = false;
                this.entrarBtn.IsEnabled = true;
                this.criarcontaBtn.IsEnabled = true;
            }
            
        }

    }
    private async void criarcontaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new registerWindow());
    }
}