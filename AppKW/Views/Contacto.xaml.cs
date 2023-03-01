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
        public Contacto()
        {
            InitializeComponent();
        }

        public async void SendData_Clicked(object sender, EventArgs e)
        {
            Contact contactForm = new Contact
            {
                name = TxtNombre.Text,
                lastname= TxtApellido.Text,
                phone_number= TxtTelefono.Text,
                email= TxtCorreo.Text,
                interested_in = TxtInteresado.SelectedItem.ToString(),
                company = TxtCompania.Text,
                description = TxtDescripcion.Text,
                type = TxtPersona.SelectedItem.ToString(),
            };

            try
            {
                Uri uri = new Uri("http://192.168.1.64:8000/api/contact");
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(contactForm);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");


                var resp = await client.PostAsync(uri, contentJson);
                Console.WriteLine(resp);

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Correcto", "Mensaje enviado", "OK");
                    TxtNombre.Text = "";
                    TxtApellido.Text = "";
                    TxtTelefono.Text = "";
                    TxtCorreo.Text = "";
                    TxtCompania.Text = "";
                    TxtDescripcion.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un error Servicio", "OK");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
