using AppKW.Models;
using AppKW.ViewModels;
using Firebase.Auth;
using Java.Net;
using Plugin.Toast.Abstractions;
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
    public partial class Registro : ContentPage
    {
        UsuarioRepositorio _userRepository = new UsuarioRepositorio();
        public Registro()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }



        public async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string nombre = TxtName.Text;
                string apellido = TxtLastName.Text;
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;
                string confirmarcontrasena = TxtConfirmPass.Text;
                //Validaciones de formulario de regstro de usuario
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
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Advertencia", "Type correo", "Ok");
                    return;
                }
                if (contrasena.Length < 8)
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



                //Validar si el registro se completo o fallo
                bool isSave = await _userRepository.Resgister(nombre, correo, contrasena);
                if (isSave)
                {
                    RegistroModel registro = new RegistroModel();
                    registro.nombre = nombre;
                    registro.apellido = apellido;
                    registro.correo = correo;

                    //Validar si se agregaron los datos
                    var isSaved = await _userRepository.Save(registro);
                    if (isSaved)
                    {
                        //await DisplayAlert("Informacion", "Registro exito", "Ok");
                        //dirigir al Login
                        await DisplayAlert("Registro exitoso", "Se envió un enlace de verificación a tu correo.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No funciono", "Ok");
                    }


                }
                else
                {
                    await DisplayAlert("Resgistro de usuario", "Registro fallido", "Ok");
                } 
            }
            //Validacion de si el correo exite en la base de datos
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Correo ya existente", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }

            }
        } 

        public void ImageButtonFacebook(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.facebook.com/kenworthdeleste"));
        }

        private void ImageButtonTwitter(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://twitter.com/KenworthdelEste"));
        }
    }
}