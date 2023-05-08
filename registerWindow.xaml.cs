using Parse;

namespace Projeto1;

public partial class registerWindow : ContentPage
{
	public registerWindow()
	{
		InitializeComponent();
        ParseClient.Initialize(new ParseClient.Configuration
        {
		//back4app
            Server = "https://parseapi.back4app.com",
            ApplicationId = "00000",
            WindowsKey = "0000"
        });

    }
	private async void continuarClicked(object sender, EventArgs e)
	{
        string username = this.userEntry.Text;
        string password = this.passwordEntry.Text;
        string email = this.mailEntry.Text;
        string phone = this.phoneEntry.Text;

        if(username.Equals("") || password.Equals("")|| email.Equals("") || phone.Equals(""))
        {
            await DisplayAlert("Aten��o", "H� campos em branco\n\nPreencha todos os campos para continuar", "OK");
        }
        else
        {
            if(email.Contains("@")==false)
            {
                await DisplayAlert("Aten��o", "Email inserido � inv�lido\n", "OK");
            }
            else
            {
                if(this.checkbox1.IsChecked == false)
                {
                    await DisplayAlert("Aten��o", "Voc� deve concordar com os termos e condi��es para continuar", "OK");
                }
                else
                {
                    this.activity1.IsRunning = true;
                    this.continuarBtn.IsEnabled = false;
                    

                    var user = new ParseUser()
                    {
                        Username = username,
                        Password = password,
                        Email = email,
                    };
                    user["mobilePhone"] = phone;

                    try
                    {
                        await user.SignUpAsync();
                        this.activity1.IsRunning = false;
                        await DisplayAlert("SUCESSO", "Sua conta foi criada com sucesso!", "OK");
                        await Navigation.PopAsync();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Account already exists for this username."))
                        {
                            await DisplayAlert("Falha na cria��o de conta", "O nome: " + username + " j� est� cadastrado.\n\nErro:\n" + ex.Message, "OK");
                            this.activity1.IsRunning = false;
                            this.continuarBtn.IsEnabled = true;

                        }
                        else if (ex.Message.Equals("Object reference not set to an instance of an object."))
                        {
                            await DisplayAlert("N�o foi poss�vel conectar", "Verifique sua conec��o e tente novamente!\n\nErro:\n" + ex.Message, "OK");
                            this.activity1.IsRunning = false;
                            this.continuarBtn.IsEnabled = true;

                        }
                    }
                }
            }
        }

    }
}