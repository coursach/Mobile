using System;
using RPM_PROJECT.api;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubControlPage : ContentPage
	{
        protected override async void OnAppearing()
        {
			if (!Preferences.Get("haveSub", false))
			{
				button.IsVisible = false;
				subscribeInfo.Text = "У вас нет подписки";
				subscribeType.Text = "";
				return;
			}

			subscribeType.Text = "Активная подписка";
			subscribeInfo.Text = "";
			button.IsVisible = true;

			var result = await API.CheckUserSubscribe();

			subscribeInfo.Text = $"{result.Name} {result.Title} {result.Price} {result.CountMonth}";

            base.OnAppearing();
        }

        public SubControlPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
			if (!Preferences.Get("haveSub", false))
				return; 

			await API.DeleteSubsribe();
			Preferences.Set("haveSub", false);
            OnAppearing();
		}
    }
}