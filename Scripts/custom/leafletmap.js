var map = L.map('leafletMap').setView([-37.8136, 144.9631], 13);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

L.marker([-37.8136, 144.9631]).addTo(map)
    .bindPopup('Monash Library')
    .openPopup();