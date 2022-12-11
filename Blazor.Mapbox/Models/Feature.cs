using OneOf;
using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Models
{
    /// <summary>
    /// Feature identifier. Feature objects returned from Map#queryRenderedFeatures or event handlers can be used as feature identifiers.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Unique id of the feature.
        /// </summary>
        [JsonPropertyName("id")]
        public object Id { get; set; }

        /// <summary>
        /// string	The id of the vector or GeoJSON source for the feature.
        /// </summary>
        [JsonPropertyName("source")] 
        public string Source { get; set; }

        /// <summary>
        /// (optional) For vector tile sources, sourceLayer is required.
        /// </summary>
        [JsonPropertyName("sourceLayer")] 
        public string SourceLayer { get; set; }
    }
}
