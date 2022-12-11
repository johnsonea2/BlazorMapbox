using System.Text.Json.Serialization;

namespace FennoradMapbox.Models
{
    public class LngLat
    {
        [JsonPropertyName("lat")]
        public float Lat { get; set; }

        [JsonPropertyName("lng")]
        public float Lng { get; set; }

        public LngLat(float lng, float lat)
        {
            Lng = lng;
            Lat = lat;            
        }
    }
}
