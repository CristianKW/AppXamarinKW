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
    public partial class RecuperarContrasenaPage : ContentPage
    {
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public RecuperarContrasenaPage()
        {
            InitializeComponent();
        }

        public async void Button_SendLink(object sender, EventArgs e)
        {
            string correo = TxtEmail.Text;
            if (string.IsNullOrEmpty(correo))
            {
                await DisplayAlert("Mensaje", "Por favor introdusca su correo electronico", "Ok");
                return;
            }

            bool isSend = await _usuarioRepositorio.ReserPassword(correo);
            if (isSend)
            {
                await DisplayAlert("Restaurar Contraseña", "Enviar elance a correo electronico", "Ok");
            }
            else
            {
                await DisplayAlert("Restaurar coontraseña", "El link fallo", "Ok");
            }

        }
    }
}