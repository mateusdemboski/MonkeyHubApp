using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation?.PushAsync(new MainPage());
        }

        private void ButtonModal_OnClicked(object sender, EventArgs e)
        {
            Navigation?.PushModalAsync(new MainPage());
        }

        private void ButtonVoltarModal_OnClicked(object sender, EventArgs e)
        {
            Navigation?.PopModalAsync();
        }
    }
}
