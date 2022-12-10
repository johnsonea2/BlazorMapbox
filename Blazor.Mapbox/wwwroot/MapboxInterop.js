function AddStylesheet() {
    var link = document.createElement('link');
    link.rel = "stylesheet";
    link.href = "https://api.mapbox.com/mapbox-gl-js/v2.0.0/mapbox-gl.css";
    document.head.appendChild(link)
}

function AddDirectionsJS() {
    var script = document.createElement("script");  // create a script DOM node
    script.src = "https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-directions/v4.1.0/mapbox-gl-directions.js";  // set its src to the provided URL
    document.head.appendChild(script);
}

function AddDirectionsCSS() {
    var link = document.createElement('link');
    link.rel = "stylesheet";
    link.href = "https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-directions/v4.1.0/mapbox-gl-directions.css";
    document.head.appendChild(link)
}


const instances = {};

var Mapbox = {
    create: function (accessToken, options, dotnetReference) {
        mapboxgl.accessToken = accessToken;

        var map = new mapboxgl.Map(options);
        map.addControl(
            new MapboxDirections({
                accessToken: mapboxgl.accessToken
            }),
            'top-left'
        );
        map.addControl(
            new mapboxgl.GeolocateControl({
                positionOptions: {
                    enableHighAccuracy: true
                },
                // When active the map will receive updates to the device's location as it changes.
                trackUserLocation: true,
                // Draw an arrow next to the location dot to indicate which direction the device is heading.
                showUserHeading: true
            })
        );
        instances[options.container] = map;

        map.on('load', function () {
            dotnetReference.invokeMethodAsync("OnLoadCallback")
        });
    },
    addLayer: function (container, layer, beforeId) {
        instances[container].addLayer(layer, beforeId);
    },
    addSource: function (container, id, source) {
        instances[container].addSource(id, source);
    },
    fitBounds: function (container, bounds) {
        var llb = new mapboxgl.LngLatBounds(bounds.sw, bounds.ne);
        instances[container].fitBounds(llb);
    },
    getCenter: function (container) {
        return instances[container].getCenter();
    },
    project: function (container, coordinate) {
        return instances[container].project(coordinate);
    },
    resize: function (container) {
        instances[container].resize();
    },
    setFeatureState: function (container, feature, state) {
        instances[container].setFeatureState(feature, state);
    },
    on: (container, eventType, dotnetReference, args) => {
        if (args === undefined) {
            instances[container].on(eventType, function (e) {
                e.target = null; // Remove map to prevent circular references.
                const result = JSON.stringify(e);
                dotnetReference.invokeMethodAsync('Invoke', result)
            })
        }
        else {
            instances[container].on(eventType, args, function (e) {
                e.target = null; // Remove map to prevent circular references.
                const result = JSON.stringify(e);
                dotnetReference.invokeMethodAsync('Invoke', result)
            })
        }
    }
}

const popups = {};

var MapboxPopup = {
    create: function (popupId, options) {
        var popup = new mapboxgl.Popup(options);
        popups[popupId] = popup;
    },
    addClassName: function (popupId, className) {
        popups[popupId].addClassName(className);
    },
    addTo: function (popupId, mapId) {
        var map = instances[mapId];
        popups[popupId].addTo(map);
    },
    getLngLat: function (popupId) {
        return popups[popupId].getLngLat();
    },
    getMaxWidth: function (popupId) {
        return popups[popupId].getMaxWidth();
    },
    isOpen: function (popupId) {
        return popups[popupId].isOpen();
    },
    remove: function (popupId) {
        popups[popupId].remove();
    },
    removeClassName: function (popupId, className) {
        popups[popupId].removeClassName(className);
    },
    setLngLat: function (popupId, lnglat) {
        popups[popupId].setLngLat(lnglat);
    },
    setText: function (popupId, text) {
        popups[popupId].setText(text);
    },
    toggleClassName: function (popupId, className) {
        return popups[popupId].toggleClassName(className);
    },
    on: (popupId, eventType, dotnetReference) => {
        popups[popupId].on(eventType, function () {
            dotnetReference.invokeMethodAsync('InvokeWithoutArgs')
        })
    }
}

export { Mapbox, MapboxPopup, AddStylesheet, AddDirectionsJS, AddDirectionsCSS };