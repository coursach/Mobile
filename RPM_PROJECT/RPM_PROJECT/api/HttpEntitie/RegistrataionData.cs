using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class AuthData
    {
        [JsonPropertyName("password")]
        public string Password { set; get; }

        [JsonPropertyName("email")]
        public string Email { set; get; }
    }
}
