using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Models
{
    public class Paint
    {

    }

    public class PaintFill : Paint
    {
        /// <summary>
        /// Optional color. Defaults to "#000000".
        /// </summary>
        [JsonPropertyName("fill-color")]
        public string FillColor { get; set; } = default!;

        /// <summary>
        /// Optional number between 0 and 1 inclusive. Defaults to 1.
        /// </summary>
        [JsonPropertyName("fill-opacity")]
        public decimal? FillOpacity { get; set; } = default!;
    }

    public class PaintLine : Paint
    {
        /// <summary>
        /// Optional color. Defaults to "#000000". Disabled by line-pattern.
        /// </summary>
        [JsonPropertyName("line-color")]
        public string LineColor { get; set; } = default!;

        /// <summary>
        /// Optional number greater than or equal to 0. Units in pixels. Defaults to 1.
        /// </summary>
        [JsonPropertyName("line-width")]
        public decimal? LineWidth { get; set; } = default!;

    }
}
