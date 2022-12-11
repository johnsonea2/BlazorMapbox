using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Fennorad.Mapbox.Models;

namespace Fennorad.Mapbox.Events
{
    /// <summary>
    /// A MapDataEvent object is emitted with the Map.event:data and Map.event:dataloading events. Possible values for dataTypes are: 
    /// • 'source': The non-tile data associated with any source 
    /// • 'style': The style used by the map
    /// </summary>
    public class MapWheelEvent : BaseEvent
    {
        /// <summary>
        /// The type of data that has changed. One of 'source' , 'style'.
        /// </summary>
        [JsonPropertyName("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// True if the event has a dataType of source and the source has no outstanding network requests.
        /// </summary>
        [JsonPropertyName("isSourceLoaded")]
        public bool? IsSourceLoaded { get; set; }

        /// <summary>
        /// Included if the event has a dataType of source and the event signals that internal data has been received or changed. Possible values are metadata , content and visibility.
        /// </summary>
        [JsonPropertyName("sourceDataType")]
        public string SourceDataType { get; set; }

        /// <summary>
        /// The coordinate of the tile if the event has a dataType of source and the event is related to loading of a tile.
        /// </summary>
        [JsonPropertyName("coordinate")]
        public MercatorCoordinate Coordinate { get; set; }
    }
}
