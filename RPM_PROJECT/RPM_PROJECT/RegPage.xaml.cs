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
                Password2.IsVisible = false;
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if(btn.Text == "Войти")
            {
                btn.Text = "Зарегестрироваться";
                Reg.BackgroundColor = Color.FromHex("#1392DC");
                Entr.BackgroundColor = Color.FromHex("#000000");
                Password2.IsVisible = true;
            }
        }
    }
}