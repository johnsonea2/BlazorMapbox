using OneOf;
using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Models
{
    public class Layer
    {
        /// <summary>
        /// A unique idenfier that you define.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of layer (for example fill or symbol). A list of layer types is available in the Mapbox Style Specification.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The data source for the layer. Reference a source that has already been defined using the source's unique id. 
        /// Reference a new source using a source object (as defined in the Mapbox Style Specification ) directly. 
        /// This is required for all layer.type options except for custom.
        /// </summary>
        [JsonPropertyName("source")]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOf<string, Source> Source { get; set; }

        /// <summary>
        /// (optional) The name of the source layer within the specified layer.source to use for this style layer. 
        /// This is only applicable for vector tile sources and is required when layer.source is of the type vector.
        /// </summary>
        [JsonPropertyName("source-layer")]
        public string SourceLayer { get; set; }

        /// <summary>
        /// (optional) Paint properties for the layer. Available paint properties vary by layer.type. 
        /// A full list of paint properties for each layer type is available in the Mapbox Style Specification. 
        /// If no paint properties are specified, default values will be used.
        /// </summary>
        [JsonPropertyName("paint")]
        public Paint Paint { get; set; }
    }
}
