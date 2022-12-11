using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using Fennorad.Mapbox.Models;

namespace Fennorad.Mapbox
{
    public class Options
    {
        /// <summary>
        /// The inital geographical centerpoint of the map. If center is not specified in the constructor options, 
        /// Mapbox GL JS will look for it in the map's style object. If it is not specified in the style, either, 
        /// it will default to [0, 0] Note: Mapbox GL uses longitude, latitude coordinate order (as opposed to latitude, longitude) to match GeoJSON.
        /// </summary>
        public LngLat? Center { get; set; }

        /// <summary>
        /// The HTML element in which Mapbox GL JS will render the map, or the element's string id . The specified element must have no children.
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        /// The minimum zoom level of the map (0-24).
        /// </summary>
        /// <remarks>
        /// default: 0
        /// </remarks>
        public int MinZoom { get; set; }

        /// <summary>
        /// The maximum zoom level of the map (0-24).
        /// </summary>
        /// <remarks>
        /// default: 22
        /// </remarks>
        public int MaxZoom { get; set; }

        public class DefaultStyles
        {
            public const string Streets = "mapbox://styles/mapbox/streets-v11";
            public const string Outdoors = "mapbox://styles/mapbox/outdoors-v11";
            public const string Light = "mapbox://styles/mapbox/light-v10";
            public const string Dark = "mapbox://styles/mapbox/dark-v10";
            public const string Satellite = "mapbox://styles/mapbox/satellite-v9";
            public const string SatelliteStreets = "mapbox://styles/mapbox/satellite-streets-v11";
            public const string NavigationPreviewDay = "mapbox://styles/mapbox/navigation-preview-day-v4";
            public const string NavigationPreviewNight = "mapbox://styles/mapbox/navigation-preview-night-v4";
            public const string NavigationGuidanceDay = "mapbox://styles/mapbox/navigation-guidance-day-v4";
            public const string NavigationGuidanceNight = "mapbox://styles/mapbox/navigation-guidance-night-v4";
        }

        /// <summary>
        /// The map's Mapbox style. This must be an a JSON object conforming to the schema described in the Mapbox Style Specification , or a URL to such JSON.
        /// To load a style from the Mapbox API, you can use a URL of the form mapbox://styles/:owner/:style, 
        /// where :owner is your Mapbox account name and :style is the style ID. Or you can use one of the following the predefined Mapbox styles:
        /// mapbox://styles/mapbox/streets-v11
        /// mapbox://styles/mapbox/outdoors-v11
        /// mapbox://styles/mapbox/light-v10
        /// mapbox://styles/mapbox/dark-v10
        /// mapbox://styles/mapbox/satellite-v9
        /// mapbox://styles/mapbox/satellite-streets-v11
        /// mapbox://styles/mapbox/navigation-preview-day-v4
        /// mapbox://styles/mapbox/navigation-preview-night-v4
        /// mapbox://styles/mapbox/navigation-guidance-day-v4
        /// mapbox://styles/mapbox/navigation-guidance-night-v4
        /// Tilesets hosted with Mapbox can be style-optimized if you append ?optimize=true to the end of your style URL, 
        /// like mapbox://styles/mapbox/streets-v11?optimize=true. Learn more about style-optimized vector tiles in our API documentation.
        /// </summary>
        public string Style { get; set; } = DefaultStyles.Streets;

        /// <summary>
        /// If specified, map will use this token instead of the one defined in mapboxgl.accessToken.
        /// </summary>
        /// <remarks>
        /// default: null
        /// </remarks>
        public string AccessToken { get; set; }

        /// <summary>
        /// The initial zoom level of the map. If zoom is not specified in the constructor options, Mapbox GL JS will look for it in the map's style object. 
        /// If it is not specified in the style, either, it will default to 0.
        /// </summary>
        public int? Zoom { get; set; }
    }
}
