using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using RPM_PROJECT.api.HttpEntitie;
using System.Collections.Generic;
using System.IO;
using System.Transactions;


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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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


                await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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
                    await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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
                
                await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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

                await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
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

                var folder = Path.GetTempPath();
                var fullPath = Path.Combine(folder, "xamarinVideo.mp4");

                var input = await response.Content.ReadAsByteArrayAsync();
                using (var output = new BinaryWriter(File.Open(fullPath, FileMode.OpenOrCreate)))
                {
                    output.Write(input);
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

        public static async ValueTask<bool> UpdateImgeUser(string path)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Token);
            var file = new ByteArrayContent(File.ReadAllBytes(path));

            using (var response = await httpClient.PostAsync(_baseApi + "/user/update/user", file))
            {

                if (response.IsSuccessStatusCode)
                    return true;

                await Alert.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, _displayOk);
                return false;
            }
        }

    }
}
