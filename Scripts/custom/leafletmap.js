var map = L.map('leafletMap').setView([-37.8136, 144.9631], 13);

var geocoder = L.Control.Geocoder.nominatim();
if (URLSearchParams && location.search) {
    // parse /?geocoder=nominatim from URL
    var params = new URLSearchParams(location.search);
    var geocoderString = params.get('geocoder');
    if (geocoderString && L.Control.Geocoder[geocoderString]) {
        console.log('Using geocoder', geocoderString);
        geocoder = L.Control.Geocoder[geocoderString]();
    } else if (geocoderString) {
        console.warn('Unsupported geocoder', geocoderString);
    }
}


var control = L.Control.geocoder({
    query: 'Moon',
    placeholder: 'Search here...',
    geocoder: geocoder
}).on('markgeocode', function (e) {
    var bbox = e.geocode.bbox;
    console.log(e.geocode.center)

    L.Routing.control({
        waypoints: [
            L.latLng(-37.8136, 144.9631),
            L.latLng(e.geocode.center.lat, e.geocode.center.lng)
        ],
        //routeWhileDragging: true,
        geocoder: L.Control.Geocoder.nominatim()
    }).addTo(map);

    console.log(bbox)

    var wayPoint1 = L.latLng(-37.8136, 144.9631);
    var wayPoint2 = L.latLng(e.geocode.center.lat, e.geocode.center.lng);

    rWP1 = new L.Routing.Waypoint;
    rWP1.latLng = wayPoint1;

    rWP2 = new L.Routing.Waypoint;
    rWP2.latLng = wayPoint2;

    var myRoute = L.Routing.osrmv1();
    myRoute.route([rWP1, rWP2], function (err, routes) {
        distance = routes[0].summary.totalDistance;
        console.log('routing distance: ' + distance);
    });

    map.fitBounds(L.latLngBounds(wayPoint1, wayPoint2));


    $(".leaflet-routing-container").remove();

}).addTo(map);







L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

L.marker([-37.8136, 144.9631]).addTo(map)
    .bindPopup('Monash Library')
    .openPopup();