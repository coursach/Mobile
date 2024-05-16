using Xamarin.Forms;
using RPM_PROJECT.api;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RPM_PROJECT
{
    public class Alert : IError
    {
        private readonly Page _page;

        public Alert(Page page)
        {
            _page = page;
        }

        public async Task DisplayAlert(string head, string info, string ok)
        {
            await _page.DisplayAlert(head, info, ok);
        }
    }

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            API.Alert = new Alert(MainPage);
            API.Token = Preferences.Get("token", "");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
