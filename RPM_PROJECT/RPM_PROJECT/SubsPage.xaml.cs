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
	public partial class SubsPage : ContentPage
	{
        public SubsPage()
        {
            InitializeComponent();


            // Отрисовка подписок

            StackLayout stackLayout = new StackLayout
            {
                Margin = new Thickness(50, 10),
            };
            StackLayout RealStack = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#1263DE"),
                Children = {
                        new Label
                    {
                        Margin = new Thickness(10, 40, 10, 70),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Text = "Premium",
                        FontSize = 40
                    } 
                }                        
            };

            RelativeLayout relativeLayout = new RelativeLayout { HorizontalOptions = LayoutOptions.CenterAndExpand };

            relativeLayout.Children.Add(new Image()
            {
                Source = "rectangularforsub.png",
                Margin = new Thickness(35, 0)
            }, Constraint.Constant(0));
            relativeLayout.Children.Add(new Label()
            {
                Text = "12",
                FontSize = 70,
                Margin = new Thickness(48, 5)
            }, Constraint.Constant(0));
            relativeLayout.Children.Add(new Label()
            {
                Text = "Месяцев",
                Margin = new Thickness(49, 95, 0, 50),
                TextColor = Color.White,
                FontSize = 60,
                BackgroundColor = Color.FromHex("#1263DE"),
                HorizontalTextAlignment = TextAlignment.Center,
                WidthRequest = 80
            }, Constraint.Constant(0));

            RealStack.Children.Add(relativeLayout);
            RealStack.Children.Add(new Label
                {
                Margin = new Thickness(10, 40, 10, 30),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Все фильмы и сериалы",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                });

            stackLayout.Children.Add(RealStack);

            StackLayout stackLayout2 = new StackLayout()
            {
                Children =
                {

                    new Label
        {
            Text = "1 000₽ в первый год",  FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        },
                    new Label
        {
            Text = "далее 2 349 в год", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        },
                    new Button
                        {
                            Text = "Оформить подписку",
                            CornerRadius = 20,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                            Margin = new Thickness(0, 10, 0, 0),
                            HeightRequest = 45,
                            TextColor= Color.White,
                            BackgroundColor= Color.FromHex("#1263DE")
                        }
                }
            };
            stackLayout.Children.Add(stackLayout2);
            
            scroll.Children.Add(stackLayout);
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