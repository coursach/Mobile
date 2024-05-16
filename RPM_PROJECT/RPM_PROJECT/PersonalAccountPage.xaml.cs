using System;
using System.Text.RegularExpressions;
using RPM_PROJECT.api;
using RPM_PROJECT.api.HttpEntitie;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalAccountPage : ContentPage
	{
        protected async override void OnAppearing()
        {
            var result = await API.GetUser();

            Name.Text = result.Name;
            Email.Text = result.Email;
            Surname.Text = result.Surname;

            var imageStream = await API.GetImageProfile(result.ImageUrl);
            image.Source = ImageSource.FromStream(() => imageStream);

            base.OnAppearing();
        }

        public PersonalAccountPage ()
		{
			InitializeComponent ();
		}

        private bool CheckData()
        {
            var regex = new Regex("^\\S+@\\S+\\.\\S+$");
            var result = regex.IsMatch(Email.Text);
            
            if (result)
            {
                DisplayAlert("Не правильные данные", "Ошибка в почту", "Ок"); 
                return false;
            }

            if (Name.Text.Length == 0 || Name.Text.Contains(" "))
            {
                DisplayAlert("Не правильные данные", "Ошибка в имени", "Ок");
                return false;
            }

            if (Surname.Text.Length == 0 || Surname.Text.Contains(" "))
            {
                DisplayAlert("Не правильные данные", "Ошибка в фамилии", "Ок");
                return false;
            }

            if (password.Text.Contains(" "))
            {
                DisplayAlert("Не правильные данные", "Ошибка в пароли", "Ок");
                return false;
            }

            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!CheckData())
                return;

            if (password.Text.Length == 0)
            {
                var result = await API.UpdateUserField(new UpdateUserSend { NameField = "password", NewValie = password.Text});
                if (!result)
                    return;
            }

            await API.UpdateUserField(new UpdateUserSend { NameField = "email", NewValie = Email.Text });
            await API.UpdateUserField(new UpdateUserSend { NameField = "name", NewValie = Name.Text });
            await API.UpdateUserField(new UpdateUserSend { NameField = "surname", NewValie = Surname.Text });
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Png,
            });

            var isValid = await API.UpdateImgeUser(result.FileName);
            if (isValid)
                return;

            OnAppearing();
        }
    }
}