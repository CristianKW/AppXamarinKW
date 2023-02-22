using AppKW.Models;
using AppKW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contacto : ContentPage
    {
        //readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Contacto()
        {
            InitializeComponent();
        }

        public async void SendData_Clicked(object sender, EventArgs e)
        {
            Contact contactForm = new Contact
            {
                name = name.Text,
                lastname= lastname.Text,
                phone_number= phone_number.Text,
                email= email.Text,
                interested_in = interested_in.SelectedItem.ToString(),
                company = company.Text,
                description = description.Text,
                type = type.SelectedItem.ToString(),
            };

            try
            {
                Uri uri = new Uri("http://192.168.1.64:8000/api/contact");
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(contactForm);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(uri, contentJson);

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Correcto", "Mensaje enviado", "OK");
                    name.Text = "";
                    lastname.Text = "";
                    phone_number.Text = "";
                    email.Text = "";
                    company.Text = "";
                    description.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un error", "OK");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await DisplayAlert("Error", "Ocurrió un error", "OK");
            }
        }
    }
}
