using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RPM_PROJECT.api.HttpEntitie
{
    public class ContentInfo
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

        [JsonPropertyName("actor")]
        public List<Actor> Actors { set; get;  }

        [JsonPropertyName("director")]
        public List<Director> Directors { set; get; }

    }

    public class Actor
    {
        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("surname")]
        public string Surname { set; get; }
    }

    public class Director
    {
        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("surname")]
        public string Surname { set; get; }
    }
}
