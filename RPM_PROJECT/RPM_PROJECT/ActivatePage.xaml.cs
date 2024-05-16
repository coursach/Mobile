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
	public partial class ActivatePage : ContentPage
	{
		public ActivatePage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await API.UserActivateCode(entry.Text);

            if (result)
            {
                await DisplayAlert("System", "Промокод активирован", "Ok");
            }
            else
            {
                await DisplayAlert("System", "Не верный промокод", "Ok");
            }
        }
    }
}