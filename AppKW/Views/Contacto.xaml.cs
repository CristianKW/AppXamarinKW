using AppKW.Models;
using AppKW.Services;
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
    public partial class Contacto : ContentPage
    {
        readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Contacto()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await FetchAllPosts();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                await DisplayAlert("Error", "Title are required", "OK");
                return;
            }

            await firebaseHelper.AddPost(TxtTitle.Text);

            TxtTitle.Text = string.Empty;

            await DisplayAlert("Success", "Post Added Successfully", "OK");

            await FetchAllPosts();
        }

        private async void LstPosts_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var post = await firebaseHelper.GetPost(SelectedPost.PostId);

            TxtTitle.Text = post.Title;
        }

        private async Task FetchAllPosts()
        {
            var allPersons = await firebaseHelper.GetAllPosts();

            LstPosts.ItemsSource = allPersons;
        }

        private Post SelectedPost => (Post)LstPosts.SelectedItem;

        private bool IsFormValid() => IsTitleValid();

        private bool IsTitleValid() => !string.IsNullOrWhiteSpace(TxtTitle.Text);
    }
}
