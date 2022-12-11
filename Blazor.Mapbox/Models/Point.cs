using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Models
{
    /// <summary>
    /// A Point geometry object, which has x and y properties representing screen coordinates in pixels.
    /// </summary>
    public class Point
    {
        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }
    }
}
