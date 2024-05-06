using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RPM_PROJECT
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            ToolbarItem tb = new ToolbarItem();
        }
        
        private void Profile(object sender, EventArgs e)
        {
            if (1 == 1)
            {

                Navigation.PushAsync(new EnterPage());
            }
            else
            {
                Navigation.PushAsync(new PersonalAccountPage());
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
    }
}
