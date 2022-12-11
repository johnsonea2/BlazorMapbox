using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fennorad.Mapbox.Events;
using Fennorad.Mapbox.Models;

namespace Fennorad.Mapbox
{
    public partial class MapboxMap : ComponentBase, IAsyncDisposable
    {
        public enum Events
        {
            /// <summary>
            /// Fired when the user cancels a "box zoom" interaction, or when the bounding box does not meet the minimum size threshold. 
            /// </summary>
            boxzoomcancel,

            /// <summary>
            /// Fired when a "box zoom" interaction ends.
            /// </summary>
            boxzoomend,

            /// <summary>
            /// Fired when a "box zoom" interaction starts. 
            /// </summary>
            boxzoomstart,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is pressed and released at the same point on the map. 
            /// • This event is compatible with the optional layerId parameter. If layerId is included as the second argument in Map#on, 
            /// the event listener will fire only when the point that is pressed and released contains a visible portion of the specifed layer.
            /// </summary>
            click,

            /// <summary>
            /// Fired when the right button of the mouse is clicked or the context menu key is pressed within the map.
            /// </summary>
            contextmenu,

            /// <summary>
            /// Fired when any map data loads or changes.
            /// </summary>
            data,

            /// <summary>
            /// Fired when any map data (style, source, tile, etc) begins loading or changing asyncronously. All dataloading events are followed by a data or error event.
            /// </summary>
            dataloading,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is pressed and released twice at the same point on the map in rapid succession.
            /// </summary>
            dblclick,

            /// <summary>
            /// Fired repeatedly during a "drag to pan" interaction.
            /// </summary>
            drag,

            /// <summary>
            /// Fired when a "drag to pan" interaction ends.
            /// </summary>
            dragend,

            /// <summary>
            /// Fired when a "drag to pan" interaction starts.
            /// </summary>
            dragstart,

            /// <summary>
            /// Fired when an error occurs. This is GL JS's primary error reporting mechanism. We use an event instead of throw to better accommodate asyncronous operations. 
            /// If no listeners are bound to the error event, the error will be printed to the console.
            /// </summary>
            error,

            /// <summary>
            /// Fired after the last frame rendered before the map enters an "idle" state: 
            /// • No camera transitions are in progress 
            /// • All currently requested tiles have loaded 
            /// • All fade/transition animations have completed
            /// </summary>
            idle,

            /// <summary>
            /// Fired immediately after all necessary resources have been downloaded and the first visually complete rendering of the map has occurred.
            /// </summary>
            load,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is pressed within the map.
            /// • This event is compatible with the optional layerId parameter. If layerId is included as the second argument in Map#on, 
            /// the event listener will fire only when the the cursor is pressed while inside a visible portion of the specifed layer.
            /// </summary>
            mousedown,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) enters a visible portion of a specified layer from outside that layer or outside the map canvas.
            /// • Important: This event can only be listened for when Map#on includes three arguments, where the second argument specifies the desired layer.
            /// </summary>
            mouseenter,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) leaves a visible portion of a specified layer, or leaves the map canvas.
            /// • Important: This event can only be listened for when Map#on includes three arguements, where the second argument specifies the desired layer.
            /// </summary>
            mouseleave,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is moved while the cursor is inside the map. As you move the cursor across the map, 
            /// the event will fire every time the cursor changes position within the map.
            /// • Note: This event is compatible with the optional layerId parameter. If layerId is included as the second argument in Map#on, 
            /// the event listener will fire only when the the cursor is inside a visible portion of the specified layer.
            /// </summary>
            mousemove,

            /// <summary>
            /// Fired when a point device (usually a mouse) leaves the map's canvas.
            /// </summary>
            mouseout,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is moved within the map. As you move the cursor across a web page containing a map, the event will fire each time it enters the map or any child elements.
            /// • Note: This event is compatible with the optional layerId parameter. If layerId is included as the second argument in Map#on, 
            /// the event listener will fire only when the the cursor is moved inside a visible portion of the specifed layer.
            /// </summary>
            mouseover,

            /// <summary>
            /// Fired when a pointing device (usually a mouse) is released within the map.
            /// • Note: This event is compatible with the optional layerId parameter. If layerId is included as the second argument in Map#on, 
            /// the event listener will fire only when the the cursor is released while inside a visible portion of the specifed layer.
            /// </summary>
            mouseup,

            /// <summary>
            /// Fired repeatedly during an animated transition from one view to another, as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            move,

            /// <summary>
            /// Fired just after the map completes a transition from one view to another, as the result of either user interaction or methods such as Map#jumpTo.
            /// </summary>
            moveend,

            /// <summary>
            /// Fired just before the map begins a transition from one view to another, as the result of either user interaction or methods such as Map#jumpTo.
            /// </summary>
            movestart,

            /// <summary>
            /// Fired repeatedly during the map's pitch (tilt) animation between one state and another as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            pitch,

            /// <summary>
            /// Fired immediately after the map's pitch (tilt) finishes changing as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            pitchend,

            /// <summary>
            /// Fired whenever the map's pitch (tilt) begins a change as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            pitchstart,

            /// <summary>
            /// Fired immediately after the map has been removed with Map.event:remove.
            /// </summary>
            remove,

            /// <summary>
            /// Fired whenever the map is drawn to the screen, as the result of
            /// • a change to the map's position, zoom, pitch, or bearing
            /// • a change to the map's style
            /// • a change to a GeoJSON source
            /// • the loading of a vector tile, GeoJSON file, glyph, or sprite
            /// </summary>
            render,

            /// <summary>
            /// Fired immediately after the map has been resized.
            /// </summary>
            resize,

            /// <summary>
            /// Fired repeatedly during a "drag to rotate" interaction
            /// </summary>
            rotate,

            /// <summary>
            /// Fired when a "drag to rotate" interaction ends. 
            /// </summary>
            rotateend,

            /// <summary>
            /// Fired when a "drag to rotate" interaction starts
            /// </summary>
            rotatestart,

            /// <summary>
            /// Fired when one of the map's sources loads or changes, including if a tile belonging to a source loads or changes. 
            /// </summary>
            sourcedata,

            /// <summary>
            /// Fired when one of the map's sources begins loading or changing asyncronously. All sourcedataloading events are followed by a sourcedata or error event. 
            /// </summary>
            sourcedataloading,

            /// <summary>
            /// Fired when the map's style loads or changes.
            /// </summary>
            styledata,

            /// <summary>
            /// Fired when the map's style begins loading or changing asyncronously. All styledataloading events are followed by a styledata or error event. 
            /// </summary>
            styledataloading,

            /// <summary>
            /// Fired when an icon or pattern needed by the style is missing. The missing image can be added with Map#addImage within this event listener 
            /// callback to prevent the image from being skipped. This event can be used to dynamically generate icons and patterns.
            /// </summary>
            styleimagemissing,

            /// <summary>
            /// Fired when a touchcancel event occurs within the map.
            /// </summary>
            touchcancel,

            /// <summary>
            /// Fired when a touchend event occurs within the map.
            /// </summary>
            touchend,

            /// <summary>
            /// Fired when a touchmove event occurs within the map.
            /// </summary>
            touchmove,

            /// <summary>
            /// Fired when a touchstart event occurs within the map.
            /// </summary>
            touchstart,

            /// <summary>
            /// Fired when the WebGL context is lost.
            /// </summary>
            webglcontextlost,

            /// <summary>
            /// Fired when the WebGL context is restored.
            /// </summary>
            webglcontextrestored,

            /// <summary>
            /// Fired when a wheel event occurs within the map.
            /// </summary>
            wheel,

            /// <summary>
            /// Fired repeatedly during an animated transition from one zoom level to another, as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            zoom,

            /// <summary>
            /// Fired just after the map completes a transition from one zoom level to another, as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            zoomend,

            /// <summary>
            /// Fired just before the map begins a transition from one zoom level to another, as the result of either user interaction or methods such as Map#flyTo.
            /// </summary>
            zoomstart
        }

        private string _AccessToken { get; set; }

        /// <summary>
        /// The reference to our exported module. 
        /// </summary>
        private IJSObjectReference _Module;

        private readonly ConcurrentDictionary<Guid, DotNetObjectReference<CallbackAction>> _References = new ConcurrentDictionary<Guid, DotNetObjectReference<CallbackAction>>();

        private DotNetObjectReference<MapboxMap> _DotNetObjectReference;

        [Inject]
        private IJSRuntime _JsRuntime { get; set; }

        /// <summary>
        /// The identifier for the HTML element.
        /// </summary>
        private readonly string _Id = "mapbox_" + Guid.NewGuid();

        [Parameter]
        public string AccessToken
        {
            get => _AccessToken; // ?? throw new NotImplementedException("A Mapbox access token is required");
            set => _AccessToken = value;
        }

        [Parameter]
        public string Height { get; set; } = "500px";

        [Parameter]
        public string Width { get; set; } = "100%";

        [Parameter]
        public Options Options { get; set; }

        [Parameter]
        public EventCallback<EventArgs> OnLoad { get; set; }

        [JSInvokable]
        public async Task OnLoadCallback()
        {
            await OnLoad.InvokeAsync(EventArgs.Empty);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", _Id);
            builder.AddAttribute(2, "style", $"width: {Width}; height: {Height}");


            if (string.IsNullOrWhiteSpace(AccessToken))
            {
                builder.AddContent(3, new MarkupString("<h3>Could not load map. Mapbox requires an access token to use the API.</h3> <a href='https://docs.mapbox.com/help/how-mapbox-works/access-tokens/'>https://docs.mapbox.com/help/how-mapbox-works/access-tokens/</a>"));
            }

            //builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.CloseElement();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _JsRuntime.InvokeAsync<IJSObjectReference>("import", "https://api.mapbox.com/mapbox-gl-js/v2.0.0/mapbox-gl.js");

                _Module = await _JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/FennoradMapbox/MapboxInterop.js");
                await _Module.InvokeVoidAsync("AddStylesheet");
                await _Module.InvokeVoidAsync("AddDirectionsJS");
                await _Module.InvokeVoidAsync("AddDirectionsCSS");

                _DotNetObjectReference = DotNetObjectReference.Create(this);

                await Create();
            }
        }

        private ValueTask Create()
        {
            // Set the container identifier.
            if (Options == null)
            {
                Options = new Options();
            }
            Options.Container = _Id;

            return _Module.InvokeVoidAsync("Mapbox.create", AccessToken, Options, _DotNetObjectReference);
        }

        public async Task<Listener> AddListener<T>(Events eventName, string layer, Action<T> handler)
        {
            var callback = new CallbackAction(_Module, eventName.ToString(), handler, typeof(T));
            var reference = DotNetObjectReference.Create(callback);
            _References.TryAdd(Guid.NewGuid(), reference);

            if (string.IsNullOrWhiteSpace(layer))
            {
                await _Module.InvokeVoidAsync("Mapbox.on", _Id, eventName.ToString(), reference);
            }
            else
            {
                await _Module.InvokeVoidAsync("Mapbox.on", _Id, eventName.ToString(), reference, layer);
            }            

            return new Listener(callback);
        }

        public async Task<Popup> GetPopup(PopupOptions options)
        {
            var popup = new Popup(_Module, _Id);
            await _Module.InvokeVoidAsync($"MapboxPopup.create", popup.ContainerId, options);
            return popup;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var value in _References.Values)
            {
                value?.Dispose();
            }

            _DotNetObjectReference?.Dispose();

            if (_Module != null)
            {
                await _Module.DisposeAsync();
            }
        }

        #region Instance Members

        /// <summary>
        /// Adds a Mapbox style layer to the map's style.
        /// </summary>
        /// <param name="layer">The layer to add, conforming to either the Mapbox Style Specification's layer definition or, less commonly, the CustomLayerInterface specification. </param>
        /// <param name="beforeId">The ID of an existing layer to insert the new layer before, resulting in the new layer appearing visually beneath the existing layer. 
        /// If this argument is not specified, the layer will be appended to the end of the layers array and appear visually above all other layers.</param>
        public ValueTask AddLayer(Layer layer, string beforeId = null) => _Module.InvokeVoidAsync("Mapbox.addLayer", _Id, layer, beforeId);

        /// <summary>
        /// Adds a source to the map's style.
        /// </summary>
        /// <param name="id">The ID of the source to add. Must not conflict with existing sources.</param>
        /// <param name="source">The source object, conforming to the Mapbox Style Specification's source definition or CanvasSourceOptions.</param>
        public ValueTask AddSource(string id, Source source) => _Module.InvokeVoidAsync("Mapbox.addSource", _Id, id, source);

        /// <summary>
        /// Pans and zooms the map to contain its visible area within the specified geographical bounds. This function will also reset the map's bearing to 0 if bearing is nonzero.
        /// </summary>
        /// <param name="bounds">Center these bounds in the viewport and use the highest zoom level up to and including Map#getMaxZoom() that fits them in the viewport.</param>
        public ValueTask FitBounds(LngLatBounds bounds) => _Module.InvokeVoidAsync($"Mapbox.fitBounds", _Id, bounds);

        /// <summary>
        /// Returns the map's geographical centerpoint.
        /// </summary>
        /// <returns><see cref="LatLng">LngLat</see>: The map's geographical centerpoint.</returns>
        public ValueTask<LngLat> GetCenter() => _Module.InvokeAsync<LngLat>($"Mapbox.getCenter", _Id);

        /// <summary>
        /// Returns a Point representing pixel coordinates, relative to the map's container, that correspond to the specified geographical location.
        /// When the map is pitched and lnglat is completely behind the camera, there are no pixel coordinates corresponding to that location.
        /// In that case, the x and y components of the returned Point are set to Number.MAX_VALUE.
        /// </summary>
        /// <param name="coordinate">The geographical location to project.</param>
        /// <returns>The Point corresponding to lnglat, relative to the map's container.</returns>
        public ValueTask<Point> Project(LngLat coordinate) => _Module.InvokeAsync<Point>("Mapbox.project", _Id, coordinate);

        /// <summary>
        /// Resizes the map according to the dimensions of its container element.  Checks if the map container size changed and updates the map if it has changed.
        /// This method must be called after the map's container is resized programmatically or when the map is shown after being initially hidden with CSS.
        /// </summary>
        public ValueTask Resize() => _Module.InvokeVoidAsync($"Mapbox.resize", _Id);

        /// <summary>
        /// Sets the state of a feature. A feature's state is a set of user-defined key-value pairs that are assigned to a feature at runtime. 
        /// When using this method, the state object is merged with any existing key-value pairs in the feature's state. 
        /// Features are identified by their feature.id attribute, which can be any number or string.       
        /// This method can only be used with sources that have a feature.id attribute.The feature.id attribute can be defined in three ways:
        /// • For vector or GeoJSON sources, including an id attribute in the original data file.
        /// • For vector or GeoJSON sources, using the promoteId option at the time the source is defined.
        /// • For GeoJSON sources, using the generateId option to auto-assign an id based on the feature's index in the source data. 
        /// If you change feature data using map.getSource('some id').setData(..), you may need to re-apply state taking into account updated id values.
        /// Note: You can use the feature-state expression to access the values in a feature's state object for the purposes of styling.
        /// </summary>
        /// <param name="feature">Feature identifier. </param>
        /// <param name="state">A set of key-value pairs. The values should be valid JSON types.</param>
        public ValueTask SetFeatureState(Feature feature, Dictionary<string, object> state) => _Module.InvokeVoidAsync("Mapbox.setFeatureState", _Id, feature, state);

        #endregion 

    }
}
