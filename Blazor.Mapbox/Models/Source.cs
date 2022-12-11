using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fennorad.Mapbox.Models
{
    /// <summary>
    /// Sources state which data the map should display. Specify the type of source with the "type" property, which must be one of vector, 
    /// raster, raster-dem, geojson, image, video. Adding a source isn't enough to make data appear on the map because sources don't contain 
    /// styling details like color or width. Layers refer to a source and give it a visual representation. This makes it possible to 
    /// style the same source in different ways, like differentiating between types of roads in a highways layer.
    /// </summary>
    public class Source
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// A URL to a TileJSON resource. Supported protocols are http:, https:, and mapbox://<Tileset ID>.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
