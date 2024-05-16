using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using RPM_PROJECT.api.HttpEntitie;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Xamarin.Essentials;


namespace RPM_PROJECT.api
{
    public interface IError
    {
        Task DisplayAlert(string head, string info, string ok);
    }

    public static class API
    {
        private const string _baseApi = "http://10.0.2.2:8000";
        private const string _displayOk = "Ok";
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public static string Token { set; get; }
        public static IError Alert { set; get; }

        public static async ValueTask<bool> UpdateUserField(UpdateUserSend updateValue)
        {
            const string emailField = "Email";
            const string passwordField = "Password";
        
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsJsonAsync(_baseApi + "/user/update/user", updateValue, _options))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Не удалось обновить поле пользователя", response.ReasonPhrase ?? "", _displayOk);
                    return false;
                }


                if (updateValue.NameField == emailField || updateValue.NameField == passwordField)
                {
                    Token = await response.Content.ReadAsStringAsync();
                }
            }

            return true;
        }

        public static async ValueTask<User> GetUser()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/get/profile",null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Не удалось получить пользователя", "Вы навторизированы", _displayOk);
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<User>(_options);
                return result;
            }
        }

        public static async ValueTask<bool> Registration(AuthData data)
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.PostAsJsonAsync(_baseApi + "/registration/user", data, _options))
            {
                if (response.IsSuccessStatusCode)
                    return true;


                await Alert.DisplayAlert("Ошибка регистрации", "Такая почта уже есть", _displayOk);
                return false;
            }

        }

        public static async ValueTask<string > Login(AuthData data)
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.PostAsJsonAsync(_baseApi + "/login", data, _options))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Ошибка авторизации", "Не верная почта или пароль", _displayOk);
                    return "";
                }

                var result = await response.Content.ReadFromJsonAsync<Token>();
                Token = result.TokenValue;

                return result.TokenValue;
            }
        }

        public static async ValueTask<IEnumerable<Subsribe>> GetAllSubscribe()
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.GetAsync(_baseApi + "/subscribe"))
            {
                if(!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Не удалось получить подписки", response.ReasonPhrase ?? "", _displayOk);
                    return null;
                }

                List<Subsribe> subsribes = new List<Subsribe>();

                var jsonResult = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<List<Subsribe>>(jsonResult);
                return result;
            }
        }

        public static async ValueTask<bool> LinkUserWithSubscribe(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/link/subscribe/" + id.ToString(), null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Не удалось оформить подписку", "Такой подписки нет", _displayOk);
                    return false;
                }
            }

            return true;
        }

        public static async ValueTask<bool> DeleteSubsribe()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/unlink/subscribe", null))
            {
                if (response.IsSuccessStatusCode)
                    return true;
                
                await Alert.DisplayAlert("Не удалось удалит подписку", response.ReasonPhrase ?? "", _displayOk);
                return false;
            }

        }

        public static async ValueTask<Subsribe> CheckUserSubscribe()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.GetAsync(_baseApi + "/user/get/subscribe"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<Subsribe>();

                await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase ?? "", _displayOk);
                return null;
            }
        }

        public static async ValueTask<bool> UserActivateCode(string promocode)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/get/promocode/" + promocode, null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public static async ValueTask<System.IO.Stream> GetImageProfile(string profileLink)
        {
            var httpClient = new HttpClient();

            return await httpClient.GetStreamAsync(_baseApi + "/" + profileLink);
        }

        public static async ValueTask<bool> GetContent(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/get/content/" + id.ToString(), null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert("Вам не доступен контент из-за уровня подписки", "пожалуйста приобритите подписку", _displayOk);
                    return false;
                }

                var folder = FileSystem.CacheDirectory;
                var fullPath = Path.Combine(folder, "xamarinVideo.mp4");


                using (var input = await response.Content.ReadAsStreamAsync())
                {

                    using (var outPut = File.Open(fullPath, FileMode.OpenOrCreate))
                    {
                        input.CopyTo(outPut);
                    }
                }
                return true;
            }
        }



        public static async ValueTask<ContentInfo> GetContentInfo(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            var response = await httpClient.GetFromJsonAsync<ContentInfo>(_baseApi + "/content/info/" + id.ToString());

            return response;
        }   

        public static async ValueTask<List<ContentShort>> GetAllMovie()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ContentShort>>(_baseApi + "/content/movie");

            return response;
        } 


        public static async ValueTask<List<ContentShort>> GetAllAnime()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ContentShort>>(_baseApi + "/content/anime");

            return response;
        }

        public static async ValueTask<bool> UpdateImageUserPng(string filePath) =>
    await UpdateImgeUser(filePath, "image/png");

        public static async ValueTask<bool> UpdateImageUserJpeg(string filePath) =>
            await UpdateImgeUser(filePath, "image/jpeg");


        private static async ValueTask<bool> UpdateImgeUser(string path, string typeImage)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);
            using (var file = File.Open(path, FileMode.Open))
            {
                using (var httpContent = new StreamContent(file))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(typeImage);
                    using (var response = await httpClient.PostAsync(_baseApi + "/user/update/user", httpContent))
                    {

                        if (response.IsSuccessStatusCode)
                            return true;

                        await Alert.DisplayAlert("Не удалось обновить фотогафию пользователя", response.ReasonPhrase ?? "", _displayOk);
                        return false;
                    }
                }
            }
        }

    }
}
