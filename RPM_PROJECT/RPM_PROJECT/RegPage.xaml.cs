using System;
using System.Data;
using RPM_PROJECT.api;
using RPM_PROJECT.api.HttpEntitie;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegPage : ContentPage
	{
		public RegPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(btn.Text == "Зарегестрироваться")
            {
                btn.Text = "Войти";
                Entr.BackgroundColor = Color.FromHex("#1392DC");
                Reg.BackgroundColor = Color.FromHex("#000000");
                Password2.IsVisible = false;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (btn.Text == "Войти")
            {
                btn.Text = "Зарегестрироваться";
                Reg.BackgroundColor = Color.FromHex("#1392DC");
                Entr.BackgroundColor = Color.FromHex("#000000");
                Password2.IsVisible = true;
            }
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            if(btn.Text == "Войти")
            {
                var result = await API.Login(new AuthData { Email = "", Password = "" });
                if (!result)
                    return;

                Preferences.Set("isLogin", true);
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                var result = await API.Registration(new AuthData { });
                if (!result)
                    return;
                btn.Text = "Войти";
                Entr.BackgroundColor = Color.FromHex("#1392DC");
                Reg.BackgroundColor = Color.FromHex("#000000");
                Password2.IsVisible = false;
            }
        }
    }
}