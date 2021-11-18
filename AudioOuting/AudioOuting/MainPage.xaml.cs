using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AudioOuting
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Padding = new Thickness(0, 80, 0, 0);

            InitializeComponent();
        }

        private async void AudioButton_Clicked(object sender, EventArgs e)
        {
            var page = new AudioPage();
            await Navigation.PushModalAsync(page);
        }
    }
}
