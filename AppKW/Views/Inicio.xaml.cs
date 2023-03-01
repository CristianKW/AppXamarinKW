using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AppKW.Models;
using Xamarin.Forms.Xaml;


namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();

            List<CarouselModel> carousels = new List<CarouselModel>()
            {
                new CarouselModel(){Title = "img 1", Url = "img1.jpg"},
                new CarouselModel(){Title = "img 2", Url = "img2.jpg"},
                new CarouselModel(){Title = "img 3", Url = "img4.jpg"}
            };


            Carousel.ItemsSource = carousels;

            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                Carousel.Position = (Carousel.Position + 1) % carousels.Count;
                return true;
            }));
        }

        async void Button_VisionClicked(object sender, EventArgs e)
        {
            await DisplayAlert("NUESTRA VISIÓN", "Consolidarnos como una empresa líder, " +
                "rentable, en constante crecimiento, comprometidos con la calidad, el desarrollo " +
                "de nuestro personal y de la comunidad", "Ok");
        }

        async void Button_MisionClicked(object sender, EventArgs e)
        {
            await DisplayAlert("NUESTRA MISIÓN", "Somos una organización en constante desarrollo, " +
                "comprometidos en dar solución a los retos del transporte, superando las expectativas " +
                "de nuestros clientes, fortaleciendo nuestra empresa y comunidad.", "Ok");
        }

        private void displayFullImage_Tapped(object sender, EventArgs e)
        {
            var imageSource = imgMision.Source;
            Navigation.PushModalAsync(new Mision());
        }
    }
}
