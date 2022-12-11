using System.Text.Json.Serialization;

namespace Fennorad.Mapbox.Models
{
    /// <summary>
    /// A LngLatBounds object represents a geographical bounding box, defined by its southwest and northeast points in longitude and latitude.
    /// If no arguments are provided to the constructor, a null bounding box is created.
    /// Note that any Mapbox GL method that accepts a LngLatBounds object as an argument or option can also accept an Array of two LngLatLike constructs and will perform an implicit conversion.This flexible type is documented as LngLatBoundsLike.
    /// </summary>
    public class LngLatBounds
    {
        /// <summary>
        /// The southwest corner of the bounding box.
        /// </summary>
        [JsonPropertyName("sw")]
        public LngLat Southwest { get; set; }

        /// <summary>
        /// The northeast corner of the bounding box.
        /// </summary>
        [JsonPropertyName("ne")]
        public LngLat Northeast { get; set; }

        public LngLatBounds(LngLat southwest, LngLat northeast)
        {
            Southwest = southwest;
            Northeast = northeast;
        }
    }
}
