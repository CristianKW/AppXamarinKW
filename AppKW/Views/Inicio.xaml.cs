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
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();


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
    }
}