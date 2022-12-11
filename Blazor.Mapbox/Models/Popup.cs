using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Fennorad.Mapbox.Models
{
    public class Popup : IDisposable
    {
        public enum Events
        {
            /// <summary>
            /// Fired when the popup is opened manually or programatically.
            /// </summary>
            open,

            /// <summary>
            /// Fired when the popup is closed manually or programatically.
            /// </summary>
            close
        }

        private readonly IJSObjectReference _Module;
        private readonly string _MapId;
        private readonly ConcurrentDictionary<Guid, DotNetObjectReference<CallbackAction>> _References = new ConcurrentDictionary<Guid, DotNetObjectReference<CallbackAction>>();

        /// <summary>
        /// The identifier for the HTML element.
        /// </summary>
        public readonly string ContainerId = "mapboxpopup_" + Guid.NewGuid();


        internal Popup(IJSObjectReference module, string mapId)
        {
            _Module = module;
            _MapId = mapId;
        }

        public async Task<Listener> AddListener(Events eventName, Action handler)
        {
            var callback = new CallbackAction(_Module, eventName.ToString(), handler);
            var reference = DotNetObjectReference.Create(callback);
            _References.TryAdd(Guid.NewGuid(), reference);

            await _Module.InvokeVoidAsync("MapboxPopup.on", ContainerId, eventName.ToString(), reference);

            return new Listener(callback);
        }

        /// <summary>
        /// Adds a CSS class to the popup container element.
        /// </summary>
        /// <param name="className">Non-empty string with CSS class name to add to popup container.</param>
        public async ValueTask AddClassName(string className) => await _Module.InvokeVoidAsync($"MapboxPopup.addClassName", ContainerId, className);

        /// <summary>
        /// Adds the popup to a map.
        /// </summary>
        /// <param name="map">The Mapbox GL JS map to add the popup to.</param>
        public async ValueTask AddTo(MapboxMap map) => await _Module.InvokeVoidAsync($"MapboxPopup.addTo", ContainerId, _MapId);

        /// <summary>
        /// Returns the geographical location of the popup's anchor.
        /// The longitude of the result may differ by a multiple of 360 degrees from the longitude previously set by 
        /// setLngLat because Popup wraps the anchor longitude across copies of the world to keep the popup on screen.
        /// </summary>
        /// <returns>The geographical location of the popup's anchor.</returns>
        public async ValueTask<LngLat> GetLngLat() => await _Module.InvokeAsync<LngLat>($"MapboxPopup.getLngLat", ContainerId);

        /// <summary>
        /// Returns the popup's maximum width.
        /// </summary>
        /// <returns>The maximum width of the popup.</returns>
        public async ValueTask<string> GetMaxWidth() => await _Module.InvokeAsync<string>($"MapboxPopup.getMaxWidth", ContainerId);

        /// <summary>
        /// </summary>
        /// <returns>true if the popup is open, false if it is closed.</returns>
        public async ValueTask<bool> IsOpen() => await _Module.InvokeAsync<bool>($"MapboxPopup.isOpen", ContainerId);

        /// <summary>
        /// Removes the popup from the map it has been added to.
        /// </summary>
        public async ValueTask Remove() => await _Module.InvokeVoidAsync($"MapboxPopup.remove", ContainerId);

        /// <summary>
        /// Removes a CSS class from the popup container element.
        /// </summary>
        /// <param name="className">Non-empty string with CSS class name to remove from popup container.</param>
        public async ValueTask RemoveClassName(string className) => await _Module.InvokeVoidAsync($"MapboxPopup.removeClassName", ContainerId, className);

        /// <summary>
        /// Sets the popup's content to the HTML provided as a string. 
        /// This method does not perform HTML filtering or sanitization, and must be used only with trusted content.
        /// Consider Popup#setText if the content is an untrusted text string.
        /// </summary>
        /// <param name="html">A string representing HTML content for the popup.</param>
        public async ValueTask SetHtml(string html) => await _Module.InvokeVoidAsync($"MapboxPopup.setHTML", ContainerId, html);

        /// <summary>
        /// Sets the geographical location of the popup's anchor, and moves the popup to it. Replaces trackPointer() behavior.
        /// </summary>
        /// <param name="lngLat">The geographical location to set as the popup's anchor.</param>
        public async ValueTask SetLngLat(LngLat lngLat) => await _Module.InvokeVoidAsync($"MapboxPopup.setLngLat", ContainerId, lngLat);

        /// <summary>
        /// Sets the popup's content to a string of text.
        /// This function creates a Text node in the DOM, so it cannot insert raw HTML.
        /// Use this method for security against XSS if the popup content is user-provided.
        /// </summary>
        /// <param name="text">Textual content for the popup.</param>
        public async ValueTask SetText(string text) => await _Module.InvokeVoidAsync($"MapboxPopup.setText", ContainerId, text);

        /// <summary>
        /// Add or remove the given CSS class on the popup container, depending on whether the container currently has that class.
        /// </summary>
        /// <param name="className">Non-empty string with CSS class name to add/remove.</param>
        /// <returns>If the class was removed return false, if class was added, then return true.</returns>
        public async ValueTask<bool> ToggleClassName(string className) => await _Module.InvokeAsync<bool>($"MapboxPopup.toggleClassName", ContainerId, className);

        public void Dispose()
        {
            foreach (var value in _References.Values)
            {
                value?.Dispose();
            }
        }
    }
}
