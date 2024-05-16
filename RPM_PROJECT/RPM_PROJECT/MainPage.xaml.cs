using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RPM_PROJECT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(300, 0, 0);

            StackLayout stackLayout = new StackLayout();
            var tapGestureRecognizer = new TapGestureRecognizer();

            if (1 == 5) // Зашёл ли пользователь в аккаунт
            {
                if(2 == 2) // есть ли подписка
                {
                    twoLabel.Text = "Сериалы рекомендованные к просмотру";
                    movie.IsVisible = true;
                    anime.IsVisible = true;
                    history.IsVisible = true;

                    // Третий скрол фильмы

                    for (int i = 0; i < 10; i++)
                    {
                        stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                        tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += GoToPlayer;
                        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                        stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                        movieContent.Children.Add(stackLayout);
                    }

                    //Четвёртый скрол аниме

                    for (int i = 0; i < 10; i++)
                    {
                        stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                        tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += GoToPlayer;
                        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                        stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                        animeContent.Children.Add(stackLayout);
                    }

                    //Пятый скрол история

                    for (int i = 0; i < 10; i++)
                    {
                        stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                        tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += GoToPlayer;
                        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                        stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                        historyContent.Children.Add(stackLayout);
                    }
                }
                // Первый скрол                

                for (int i = 0; i < 10; i++)
                {
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                    firstContent.Children.Add(stackLayout);
                }

                //Второй скрол
                
                for (int i = 0; i < 10; i++)
                {
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                    twoContent.Children.Add(stackLayout);
                }

                
            }
            else
            {
                // Первый скрол

                firstLabel.Text = "Топ 100 за месяц";

                for(int i = 0; i < 10; i++)
                {
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                    firstContent.Children.Add(stackLayout);
                }

                //Второй скрол

                twoLabel.Text = "Рекомендованные к просмотру";
                for (int i = 0;i < 10; i++)
                {
                    stackLayout = new StackLayout() { WidthRequest = 250, HeightRequest = 150 };
                    tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += GoToPlayer;
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    stackLayout.Children.Add(new Image { Source = "i_drive.png", Aspect = Aspect.AspectFill });
                    twoContent.Children.Add(stackLayout);
                }
            }
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
            if(BurgerSlider.TranslationX == - 300)
            {
                BurgerSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
            }
        }

        private async void Profile(object sender, EventArgs e)
        {
            if (!Preferences.Get("isLogin", false)) // Вошёл ли в аккаунт пользователь
            {
                Navigation.PushAsync(new RegPage());
            }
            else
            {
                ProfileSlider.TranslateTo(0, 0, 450, Easing.CubicInOut);
                if (1 == 1) // Какая подписка у пользователя
                {
                    twoLabel.Text = "Сериалы на основе ваших предпочтений";
                    movie.IsVisible = true;
                    anime.IsVisible = true;
                    history.IsVisible = true;
                }
            }
        }

        private void GoToPlayer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WatchPage());
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
            Preferences.Remove("isLogin");
            Preferences.Remove("token");
            Profile(sender, e);
        }
    }
}
