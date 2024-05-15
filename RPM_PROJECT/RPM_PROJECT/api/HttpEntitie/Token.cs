using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class Token
    {
        [JsonPropertyName("token")]
        public string TokenValue { set; get; }
    }
}
