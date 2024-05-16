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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(btn.Text == "Зарегестрироваться")
            {
                btn.Text = "Войти";
                Entr.BackgroundColor = Color.FromHex("#1392DC");
                Reg.BackgroundColor = Color.FromHex("#000000");
                password2.IsVisible = false;
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (btn.Text == "Войти")
            {
                btn.Text = "Зарегестрироваться";
                Reg.BackgroundColor = Color.FromHex("#1392DC");
                Entr.BackgroundColor = Color.FromHex("#000000");
                password2.IsVisible = true;
            }
        }
        public bool CheckData()
        {
            if(email.Text.Length <= 2) return false;
            if(password.Text.Length <= 0) return false;
            if (password2.Text.Length <= 0 && password2.IsVisible) return false;
            if(!email.Text.Contains("@")) return false;
            try{
                char a = email.Text[email.Text.IndexOf("@") + 1], b = email.Text[email.Text.IndexOf("@") - 1];
            }
            catch
            {
                return false;
            }
            return true;
        }
        private async void btn_Clicked(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (btn.Text == "Войти")
                {
                    var result = await API.Login(new AuthData { Email = email.Text, Password = password.Text });
                    if (result == "")
                        return;

                    Preferences.Set("isLogin", true);
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    if (password.Text == password2.Text)
                    {
                        var result = await API.Registration(new AuthData { Email = email.Text, Password = password.Text });
                        if (!result)
                            return;
                        btn.Text = "Войти";
                        Entr.BackgroundColor = Color.FromHex("#1392DC");
                        Reg.BackgroundColor = Color.FromHex("#000000");
                        password2.IsVisible = false;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Не верный пароль", "Ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Не верные данные", "Ok");
            }
        }
    }
}