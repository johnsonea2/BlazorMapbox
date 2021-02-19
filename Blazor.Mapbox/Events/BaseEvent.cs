using System.Text.Json.Serialization;

namespace Blazor.Mapbox.Events
{
    public class BaseEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
