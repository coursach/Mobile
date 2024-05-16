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
        private const string _invalidData = "Вы ввели не правильное значение";
        private const string _isOk = "Ок";

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

        private bool CheckEmailData(string data)
        {
            var regex = new Regex("^\\S+@\\S+\\.\\S+$", RegexOptions.Compiled);
            if (!regex.IsMatch(data))
            {
                DisplayAlert(_invalidData, "Почта не соотвествует стилю почты", _isOk);
                return false;
            }

            return true;
        }

        private async void UpdateSurnameClick(object sender, EventArgs e)
        {
            if (!CheckTextData("Фамилия", Surname.Text))
                return;

            await API.UpdateUserField(new UpdateUserSend { NameField = "Surname", NewValue = Surname.Text });
        }

        private async void UpdateImageClick(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();

                if (!result.FileName.EndsWith(".jpeg") || !result.FileName.EndsWith(".png") || !result.FileName.EndsWith(".jpg"))
                {
                    await DisplayAlert(_invalidData, "Выберите jpg или png формат", _isOk);
                    return;
                }

                var isValid = await API.UpdateImgeUser(result.FileName);
                if (isValid)
                    return;

                OnAppearing();
            } 
            catch
            {
                await DisplayAlert("Ошибка при чтении хранилища", "Разрешите права на чтение", _isOk);
            }
        }

        private async void UpdateNameClick(object sender, EventArgs e)
        {
            if (!CheckTextData("Имя", Name.Text))
                return;

            await API.UpdateUserField(new UpdateUserSend { NameField = "Name", NewValue= Name.Text });
        }

        private async void UpdateEmailClick(object sender, EventArgs e)
        {
            if (!CheckEmailData(Email.Text))
                return;

            await API.UpdateUserField(new UpdateUserSend { NameField = "Email", NewValue = Email.Text });
            Preferences.Set("token", API.Token);
        }

        private async void UpdatePasswordClick(object sender, EventArgs e)
        {
            if (!CheckTextData("Пароль", password.Text))
                return;

            await API.UpdateUserField(new UpdateUserSend { NameField = "Password", NewValue = password.Text });
            Preferences.Set("token", API.Token);
        }

        private bool CheckTextData(string name, string data)
        {
            if (data.Length == 0)
            {
                DisplayAlert(_invalidData, $"{name} не имеет симолов", _isOk);
                return false;
            }

            if (data.Contains(" "))
            {
                DisplayAlert(_invalidData, $"{name} содержит пробелы", _isOk);
                return false;
            }

            var onlyTextRegex = new Regex(@"^\w+$", RegexOptions.Compiled);
            if (!onlyTextRegex.IsMatch(data))
            {
                DisplayAlert(_invalidData, $"{name} содержит не только буквы", _isOk);
                return false;
            }

            return true; 
        }
    }
}