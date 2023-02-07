using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
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

    }
}
