﻿using AppKW.Models;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");
        static string webAPIKey = "AIzaSyA9YNZpoGoOmy18G8aUA84VmIcmcmXOFAE";
        FirebaseAuthProvider authProvider; 

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }
        public async Task<bool> Resgister(string nombre, string correo, string contrasena)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(correo,contrasena,nombre);
            if(!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string> SignIn(string correo, string contrasena)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(correo, contrasena);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }

        //Guardar datos del usuario
        public async Task<bool> Save(RegistroModel registro)
        {
           var data = await firebaseClient.Child(nameof(RegistroModel)).PostAsync(JsonConvert.SerializeObject(registro));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        //Recuperar contraseña
        public async Task<bool>ReserPassword(string email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }
    }
}
