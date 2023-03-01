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
using Firebase.Auth;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Buzon : ContentPage
    {
        public Buzon()
        {
            InitializeComponent();
        }

        public async void SendData_Clicked(object sender, EventArgs e)
        { 
            Mailbox mailboxForm = new Mailbox
            {
                name = TxtNombre.Text,
                company_name = TxtCompania.Text,
                reason = TxtRazon.SelectedItem.ToString(),
                date_of_incident = TxtDateOfIncident.Date.ToString("yyyy-MM-dd"),
                message = TxtMessage.Text,
            };

            try
            {
                Uri uri = new Uri("http://192.168.1.64:8000/api/mailbox");
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(mailboxForm);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(uri, contentJson);

                Console.WriteLine(json);
                Console.WriteLine(resp);

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Correcto", "Mensaje enviado", "OK");
                    TxtNombre.Text = "";
                    TxtCompania.Text = "";
                    TxtMessage.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un error Servicio", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }


    }
}