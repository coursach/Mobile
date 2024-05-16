using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_PROJECT.api;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchPage : ContentPage
    {
        int Index;
        protected override async void OnAppearing()
        {
            var result = await API.GetContentInfo(Index);
            name.Text = result.Name;
            description_details.Text = result.DescriptionDetails;
            var path = await API.GetImageProfile(result.ImagePath);
            image.Source = ImageSource.FromStream(() => path);

        }
        public WatchPage()
        {
            InitializeComponent();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(300, 0, 0);
        }
        public WatchPage(int index)
        {
            InitializeComponent();
            Index = index;
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(300, 0, 0);
        }

        private void ClosePanel(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX != -300)
            {
                BurgerSlider.TranslateTo(-300, 0, 450, Easing.SinOut);

            }
            if (ProfileSlider.TranslationX != 300)
            {
                ProfileSlider.TranslateTo(300, 0, 450, Easing.SinOut);
            }
        }
        private void OpenBurger(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX == -300)
            {
                BurgerSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
            }
        }

        private void Profile(object sender, EventArgs e)
        {
            if (1 == 2) // Вошёл ли в аккаунт пользователь
            {
                Navigation.PushAsync(new RegPage());
            }
            else
            {
                ProfileSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
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
            Navigation.PushAsync(new MainPage());
        }

        private void Exit(object sender, EventArgs e)
        {

        }
    }
}