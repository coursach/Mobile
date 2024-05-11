using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MylistsPage : ContentPage
	{
		public MylistsPage ()
		{
			InitializeComponent ();
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
            if (1 == 2)
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
            Navigation.PushAsync(new MoviesPage());
        }
        private void Movies(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage());
        }
        private void Serials(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoviesPage());
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
            Navigation.PushAsync(new MainPage());
        }

        private void Exit(object sender, EventArgs e)
        {

        }
    }
}