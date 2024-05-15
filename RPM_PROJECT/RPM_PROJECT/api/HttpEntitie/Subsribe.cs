using System.Data.Common;
using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class Subsribe
    {
        [JsonPropertyName("id")]
        public int Id { set; get; }

        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("count_month")]
        public int CountMonth { set; get; }

        [JsonPropertyName("title")]
        public string Title { set; get; }

        [JsonPropertyName("description")]
        public string Description { set; get; }

        [JsonPropertyName("discount")]
        public int Discount { set; get;  }

        [JsonPropertyName("price")]
        public int Price { set; get; }
    }
}
