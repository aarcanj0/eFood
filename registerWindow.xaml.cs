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
            await DisplayAlert("Atenção", "Há campos em branco\n\nPreencha todos os campos para continuar", "OK");
        }
        else
        {
            if(email.Contains("@")==false)
            {
                await DisplayAlert("Atenção", "Email inserido é inválido\n", "OK");
            }
            else
            {
                if(this.checkbox1.IsChecked == false)
                {
                    await DisplayAlert("Atenção", "Você deve concordar com os termos e condições para continuar", "OK");
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
                            await DisplayAlert("Falha na criação de conta", "O nome: " + username + " já está cadastrado.\n\nErro:\n" + ex.Message, "OK");
                            this.activity1.IsRunning = false;
                            this.continuarBtn.IsEnabled = true;

                        }
                        else if (ex.Message.Equals("Object reference not set to an instance of an object."))
                        {
                            await DisplayAlert("Não foi possível conectar", "Verifique sua conecção e tente novamente!\n\nErro:\n" + ex.Message, "OK");
                            this.activity1.IsRunning = false;
                            this.continuarBtn.IsEnabled = true;

                        }
                    }
                }
            }
        }

    }
}