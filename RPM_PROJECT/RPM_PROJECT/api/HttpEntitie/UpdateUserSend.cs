using System.Text.Json;
using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class UpdateUserSend
    {
        [JsonPropertyName("name_field")]
        public string NameField { set; get; }

        [JsonPropertyName("information")]
        public string NewValie { set; get; }
    }
}
