using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RPM_PROJECT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToPlayer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WatchPage());
        }
       private async void ClosePanel(object sender, EventArgs e)
        {
            if (BurgerSlider.IsVisible)
            {
                BurgerSlider.TranslateTo(-30, -50, 450, Easing.SinOut);
                await Task.Delay(200);
                BurgerSlider.IsVisible = false;
            }
            if (ProfileSlider.IsVisible)
            {
                ProfileSlider.TranslateTo(30, -50, 450, Easing.SinOut);
                await Task.Delay(200);
                ProfileSlider.IsVisible = false;
            }
        }
        private async void OpenBurger(object sender, EventArgs e)
        {
            if (BurgerSlider.IsVisible)
            {
                BurgerSlider.TranslateTo(-30, -50, 450, Easing.SinOut);
                await Task.Delay(200);
                BurgerSlider.IsVisible = false;
            }
            else
            {                
                BurgerSlider.IsVisible = true;
                BurgerSlider.TranslateTo(10, 10, 450, Easing.CubicInOut);
            }
        }

        private async void Profile(object sender, EventArgs e)
        {
            if (1 == 1)
            {

                Navigation.PushAsync(new RegPage());
            }
            else
            {
                if (ProfileSlider.IsVisible)
                {
                    ProfileSlider.TranslateTo(30, -50, 450, Easing.SinOut);
                    await Task.Delay(200);
                    ProfileSlider.IsVisible = false;
                }
                else
                {
                    ProfileSlider.IsVisible = true;
                    ProfileSlider.TranslateTo(-10, 10, 450, Easing.CubicInOut);
                }
            }
        }
        private void Anime(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(2));
        }
        private void Movies(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(0));
        }
        private void Serials(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage(1));
        }
        private void My(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MylistsPage());
        }
        private void Sub(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SubsPage());
        }
        private void Settings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void ToMainPage(object sender, EventArgs e)
        {
            //
        }

        private void Exit(object sender, EventArgs e)
        {

        }
    }
}
