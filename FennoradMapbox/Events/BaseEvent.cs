using System.Text.Json.Serialization;

namespace FennoradMapbox.Events
{
    public class BaseEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
