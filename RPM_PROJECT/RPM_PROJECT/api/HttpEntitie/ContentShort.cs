using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class ContentShort
    {
        [JsonPropertyName("id")]
        public int Id { set; get; }

        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("description")]
        public string Description { set; get; }

        [JsonPropertyName("description_details")]
        public string DescriptionDetails { set; get; }

        [JsonPropertyName("image_path")]
        public string ImagePath { set; get; }

        [JsonPropertyName("level_subscribe")]
        public int LevelSubribe { set; get; }
    }
}
