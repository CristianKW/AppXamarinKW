using AppKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        public async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Advertencia", "introduce tu correo", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Advertencia", "Introduce tu contraseña", "Ok");
                    return;
                }
                
                string token = await usuarioRepositorio.SignIn(correo, contrasena);
                await SecureStorage.SetAsync("token", token);

                if (!string.IsNullOrEmpty(token))
                {
                    await Navigation.PushAsync(new Inicio());
                }
                else
                {
                    await DisplayAlert("Inicio de sesión", "Fallo el inicio de sesión", "Ok");
                }
            }
            catch(Exception exception) 
            {
                if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("No autorizado", "Correo no existente", "Ok");

                }
                else if(exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("No autorizado", "Contraseña incorrecta", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }
            }
            
        }
    }
}