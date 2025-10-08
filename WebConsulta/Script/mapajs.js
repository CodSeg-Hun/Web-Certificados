function setUpClickListener(map) {
  map.addEventListener('tap', function (evt) {
    var coord = map.screenToGeo(evt.currentPointer.viewportX,
                                evt.currentPointer.viewportY);
    //alert(coord.lat);
    //alert(coord.lng);
    alert(document.getElementById('<%= txtlat.ClientID %>').value);
    document.getElementById('<%= txtlat.ClientID %>').value = coord.lat;
    document.getElementById('<%= txtlon.ClientID %>').value = coord.lng;
    addMarkersToMap(map,coord.lat,coord.lng);
  });
}

function addMarkersToMap(map, latitud, longitud) {
    var pMarker = new H.map.Marker({lat:latitud, lng:longitud});
    map.removeObjects(map.getObjects ());
    map.addObject(pMarker);
}

var platform = new H.service.Platform({
    apikey: 'M8iySFedDKlFPqj200tdSwaK2CLKs9FOEuN9hZgevrE'
});
var defaultLayers = platform.createDefaultLayers();
var map = new H.Map(document.getElementById('map'),
                    defaultLayers.vector.normal.map,{
                    center: {lat:-2.235104, 
                            lng: -79.891516},
                            zoom: 15,
                            pixelRatio: window.devicePixelRatio || 1
});
window.addEventListener('resize', () => map.getViewPort().resize());

var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));

function logEvent(str) {
    var entry = document.createElement('li');
    entry.className = 'log-entry';
    entry.textContent = str;
    logContainer.insertBefore(entry, logContainer.firstChild);
}

setUpClickListener(map);