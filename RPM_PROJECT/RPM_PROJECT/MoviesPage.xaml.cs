using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_PROJECT.api;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoviesPage : ContentPage
	{
        protected override async void OnAppearing()
        {
            if (Preferences.Get("isLogin", false))
            {
                
                var user = await API.GetUser();
                if (user != null)
                {
                    ava.IsVisible = false;
                    var path = await API.GetImageProfile(user.ImageUrl);

                    avaImage.IsVisible = true;
                    avaImage.Source = ImageSource.FromStream(() => path);
                    avaImage.Aspect = Aspect.AspectFill;
                    ProfileName.Text = user.Name;
                    ProfileEmail.Text = user.Email;
                    var path1 = await API.GetImageProfile(user.ImageUrl);
                    ProfileAva.Source = ImageSource.FromStream(() => path1);
                    ProfileAva.Aspect = Aspect.AspectFill;
                }
            }
            if (types == 0)
            {
                StackLayout stackLayout = new StackLayout();
                var tapGestureRecognizer = new TapGestureRecognizer();
                var result = await API.GetAllMovie();
                foreach (var movie in result)// Третий скрол фильмы
                {
                    var path = await API.GetImageProfile(movie.ImagePath);
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.ClassId = movie.Id.ToString();
                    stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                    movieContent.Children.Add(stackLayout);
                }
                movie.IsVisible = true;
            }
            if (types == 1)
            {
                StackLayout stackLayout = new StackLayout();
                var tapGestureRecognizer = new TapGestureRecognizer();
                var result = await API.GetAllMovie();
                foreach (var movie in result)// Третий скрол фильмы
                {
                    var path = await API.GetImageProfile(movie.ImagePath);
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.ClassId = movie.Id.ToString();
                    stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                    historyContent.Children.Add(stackLayout);
                }
                serial.IsVisible = true;
            }
            if (types == 2)
            {
                StackLayout stackLayout = new StackLayout();
                var tapGestureRecognizer = new TapGestureRecognizer();
                var result = await API.GetAllAnime();
                foreach (var movie in result)// Третий скрол фильмы
                {
                    var path = await API.GetImageProfile(movie.ImagePath);
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.ClassId = movie.Id.ToString();
                    stackLayout.Children.Add(new Image { Source = ImageSource.FromStream(() => path), Aspect = Aspect.AspectFill });
                    animeContent.Children.Add(stackLayout);
                }
                anime.IsVisible = true;
            }
        }
        int types = 0;
		public MoviesPage ()
		{
			InitializeComponent ();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(600, 0, 0);
        }
		public MoviesPage(int type)
		{
			InitializeComponent ();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(600, 0, 0);
            types = type;
            
		}
        private void GoToPlayer(object sender, EventArgs e)
        {

            Navigation.PushAsync(new WatchPage(int.Parse((sender as StackLayout).ClassId)));
        }
        private void ClosePanel(object sender, EventArgs e)
        {
            if (BurgerSlider.TranslationX != -300)
            {
                BurgerSlider.TranslateTo(-300, 0, 450, Easing.SinOut);

            }
            if (ProfileSlider.TranslationX != 600)
            {
                ProfileSlider.TranslateTo(600, 0, 450, Easing.SinOut);
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
            if (!Preferences.Get("isLogin", false)) // Вошёл ли в аккаунт пользователь
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
            Preferences.Remove("isLogin");
            Preferences.Remove("token");
            ava.IsVisible = true;
            avaImage.IsVisible = false;
            ProfileSlider.TranslateTo(300, 0, 0);
        }
    }
}