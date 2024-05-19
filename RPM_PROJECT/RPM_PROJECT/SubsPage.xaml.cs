using RPM_PROJECT.api.HttpEntitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_PROJECT.api;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubsPage : ContentPage
	{
        StackLayout stackLayout = new StackLayout();
        StackLayout stackLayout2 = new StackLayout();
        StackLayout RealStack = new StackLayout();
        RelativeLayout relativeLayout = new RelativeLayout();
        protected override async void OnAppearing()
        {
            if (Preferences.Get("isLogin", false))
            {
                ava.IsVisible = false;
                var user = await API.GetUser();
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
            var subsribes = await API.GetAllSubscribe();
            foreach (Subsribe sub in subsribes)
            {
                string colour;
                var price = sub.Price;
                if (price < 300) colour = "#C39028";
                else if (price < 750) colour = "#1263DE";
                else if (price < 850) colour = "#5F7C8D";
                else if (price<2200) colour = "#1263DE";
                else colour = "#5F7C8D";

                stackLayout = new StackLayout
                {
                    Margin = new Thickness(50, 10),
                };
                RealStack = new StackLayout()
                {
                    BackgroundColor = Color.FromHex(colour),
                    Children = {
                        new Label
                    {
                        Margin = new Thickness(10, 40, 10, 70),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        TextColor = Color.FromHex("#FFFFFF"),
                        Text = sub.Name,
                        FontSize = 40
                    }
                }
                };

                relativeLayout = new RelativeLayout { HorizontalOptions = LayoutOptions.CenterAndExpand };

                relativeLayout.Children.Add(new Image()
                {
                    Source = "rectangularforsub.png",
                    Margin = new Thickness(35, 0),
                    IsVisible = false
                }, Constraint.Constant(0));
                relativeLayout.Children.Add(new Label()
                {
                    Text = "Месяцев",
                    Margin = new Thickness(49, 95, 0, 50),
                    TextColor = Color.White,
                    FontSize = 60,
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = 80
                }, Constraint.Constant(0));
                relativeLayout.Children.Add(new Label()
                {
                    Text = sub.CountMonth.ToString(),
                    FontSize = 80,
                    TextColor = Color.White,
                    Margin = new Thickness(48, 5)
                }, Constraint.Constant(0));


                RealStack.Children.Add(relativeLayout);
                RealStack.Children.Add(new Label
                {
                    Margin = new Thickness(10, 40, 10, 30),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = Color.White,
                    Text = sub.Title,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });

                stackLayout.Children.Add(RealStack);

                var btn = new Button
                {
                    Text = Preferences.Get("haveSub", false) ? "Уже есть" : "Оформить подписку" ,
                    CornerRadius = 20,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    Margin = new Thickness(0, 10, 0, 0),
                    HeightRequest = 45,
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("#1263DE"),
                };

                if (!Preferences.Get("haveSub", false))
                {
                    btn.Clicked += async (object sender, EventArgs e) =>
                    {
                        var result = await API.LinkUserWithSubscribe(sub.Id);
                        await DisplayAlert("Поздравляем", "Вы оформили подписку", "Ок");
                        Preferences.Set("haveSub", true);
                        await Navigation.PopAsync();
                    };
                }

                stackLayout2 = new StackLayout()
                {
                    Children =
                {

                    new Label
                        {
                            Text = sub.Description,  FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        },
                        btn,
                }
                };
                stackLayout.Children.Add(stackLayout2);

                scroll.Children.Add(stackLayout);
            }
            StackLayout stackLayout1 = new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 20,
                        Children =
                        {
                            new Image { Source = "vk.png" },
                            new Image { Source = "tg.png" },
                            new Image { Source = "chviter.png" }
                        }
                    },
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Text = "Мы всегда готовы вам помочь"
                    }
                }
            };
            scroll.Children.Add(stackLayout1);
            base.OnAppearing();
        }


        public SubsPage ()
		{
			InitializeComponent ();
            BurgerSlider.TranslateTo(-300, 0, 0);
            ProfileSlider.TranslateTo(600, 0, 0);

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
            //Navigation.PushAsync(new SubsPage());
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
