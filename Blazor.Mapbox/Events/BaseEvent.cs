using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Events
{
    public class BaseEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
