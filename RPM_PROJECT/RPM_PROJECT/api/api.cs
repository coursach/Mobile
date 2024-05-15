using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using RPM_PROJECT.api.HttpEntitie;
using System.Collections.Generic;
using System.Data;
using System.IO;


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

        public static string Token { private set; get; }
        public static IError Alert { set; get; }

        public static async ValueTask<bool> UpdateUserField(UpdateUserSend updateValue)
        {
            const string emailField = "email";
            const string passwordField = "password";
        
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsJsonAsync(_baseApi + "/user/update/user", updateValue, _options))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(),
                        _displayOk);
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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(),
                       _displayOk);
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
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(),
                       _displayOk);
                    return false;
                }
            }

            return true;
        }

        public static async ValueTask<string> Login(AuthData data)
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.PostAsJsonAsync(_baseApi + "/login", data, _options))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(),
                       _displayOk);
                    return null;
                }

                var token= await response.Content.ReadFromJsonAsync<Token>();
                Token = token.TokenValue;

                return token.TokenValue;
            }
        }

        public static async ValueTask<IEnumerable<Subsribe>> GetAllSubscribe()
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.GetAsync(_baseApi + "/subscribe"))
            {
                if(!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(),
                       _displayOk);
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

            using (var response = await httpClient.PostAsync(_baseApi + "/user/link/subscribe" + id.ToString(), null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), "Такой подписки не существует",
                       _displayOk);
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
                if (!response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), "Ошибка при отмене подписки",
                       _displayOk);
                    return false;
                }
            }

            return true;
        }

        public static async ValueTask<Subsribe> CheckUserSubscribe()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/get/subscribe", null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<Subsribe>();
            }
        }

        public static async ValueTask<bool> UserActivateCode(string promocode)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.PostAsync(_baseApi + "/user/get/promocode/" + promocode, null))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;
            }
        }

        public static async ValueTask<System.IO.Stream> GetImageProfile(string profileLink)
        {
            var httpClient = new HttpClient();

            using (var response = await httpClient.GetStreamAsync(_baseApi + "/" + profileLink))
            {
                return response;
            }
        }

        public static async ValueTask<System.IO.Stream> GetContent(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            using (var response = await httpClient.GetStreamAsync(_baseApi + "/user/get/content/" + id.ToString()))
            {
                return response;
            }
        }

        public static async ValueTask<ContentInfo> GetContentInfo(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);

            var response = await httpClient.GetFromJsonAsync<ContentInfo>(_baseApi + "/content/info/" + id.ToString());
            if (response is null)
            {
                await Alert.DisplayAlert("500", "Контента нет", _displayOk);
                return null;
            }

            return response;
        }   

        public static async ValueTask<List<ContentShort>> GetAllMovie()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ContentShort>>(_baseApi + "/content/movie");

            if (response is null)
            {
                await Alert.DisplayAlert("500", "Контента нет", _displayOk);
            }

            return response;
        } 

        public static async ValueTask<List<ContentShort>> GetAllAnime()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<ContentShort>>(_baseApi + "/content/anime");

            if (response is null)
            {
                await Alert.DisplayAlert("500", "Контента нет", _displayOk);
            }

            return response;
        }

        public static async ValueTask<bool> UpdateImgeUser(string path)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);
            var file = new ByteArrayContent(File.ReadAllBytes(path));

            using (var response = await httpClient.PostAsync(_baseApi + "/user/update/user", file))
            {

                if (response.IsSuccessStatusCode)
                {
                    await Alert.DisplayAlert(response.StatusCode.ToString(), "Ошибка при отмене подписки",
                       _displayOk);
                    return false;
                }
            }

            return true;
        }
    }
}
