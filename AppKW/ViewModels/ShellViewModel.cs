using AppKW.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private bool isAdmin;
        private bool isUser;

        public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }

        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }
        public ShellViewModel()
        {
            MessagingCenter.Subscribe<LoginPage>(this, "Admin", (sender) =>
            {
                IsAdmin = true;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "user", (sender) =>
            {
                IsAdmin = false;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "User", (sender) =>
            {
                IsUser = true;
            });
            MessagingCenter.Subscribe<LoginPage>(this, "invitado", (sender) =>
            {
                IsUser = false;
            });

        }
    }
}
