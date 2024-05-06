using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnterPage : ContentPage
	{
        static HttpClient httpClient = new HttpClient();
        public EnterPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked_2(object sender, EventArgs e) // Reg form
        {
            Navigation.PushAsync(new RegitrationPage());
        }

        private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            using(var response = await httpClient.GetAsync("http://localhost:8000/user/get/profile"))
            {

            }
        }
    }
}