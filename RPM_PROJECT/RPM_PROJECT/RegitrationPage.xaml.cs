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
	public partial class RegitrationPage : ContentPage
	{
		public RegitrationPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
			Navigation.PushAsync(new EnterPage ());
        }
    }
}