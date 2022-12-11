using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Fennorad.Mapbox.Models;

namespace Fennorad.Mapbox.Events
{
    public class MapMouseEvent : BaseEvent
    {
        [JsonPropertyName("lngLat")]
        public LngLat LngLat { get; set; }

        [JsonPropertyName("point")]
        public Point Point { get; set; }

        [JsonPropertyName("features")]
        public Feature[] Features { get; set; }
    }
}
