using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class User
    {
        [JsonPropertyName("email")]
        public string Email { set; get; }

        [JsonPropertyName("have_subscribe")]
        public bool HaveSubsribe { set; get; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { set; get; }

        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("role")]
        public string Role { set; get; }

        [JsonPropertyName("surname")]
        public string Surname { set; get; }
    }
}
