using AppKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
        UsuarioRepositorio _userRepository = new UsuarioRepositorio();
		public Registro ()
		{
			InitializeComponent ();
		}

		public async void ButtonRegister_Clicked(object sender, EventArgs e)
		{
            try
            {
                string nombre = TxtName.Text;
                string apellido = TxtLastName.Text;
                string nombreusuario = TxtUserName.Text;
                string departamento = TxtDepartment.Text;
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;
                string confirmarcontrasena = TxtConfirmPass.Text;
                if (String.IsNullOrEmpty(nombre))
                {
                    await DisplayAlert("Advertencia", "Tipo nombre", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(apellido))
                {
                    await DisplayAlert("Advertencia", "Type apellido", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(nombreusuario))
                {
                    await DisplayAlert("Advertencia", "Type nombre usuario", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(departamento))
                {
                    await DisplayAlert("Advertencia", "Type departamento", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Advertencia", "Type correo", "Ok");
                    return;
                }
                if (contrasena.Length<8)
                {
                    await DisplayAlert("Advertencia", "Contraseña mayor a 8 caraceres", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Advertencia", "Type contraseña", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(confirmarcontrasena))
                {
                    await DisplayAlert("Advertencia", "Type confirmar contraseña", "Ok");
                    return;
                }
                if (contrasena != confirmarcontrasena)
                {
                    await DisplayAlert("Advertencia", "La contraseña no coincide", "Ok");
                    return;
                }

                bool isSave = await _userRepository.Resgister(nombre, correo, contrasena);
                if (isSave)
                {
                    await DisplayAlert("Resgistro de usuario", "Registro completo", "Ok");
                }
                else
                {
                    await DisplayAlert("Resgistro de usuario", "Registro fallido", "Ok");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email exist", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }

            }


        }

    }
}
