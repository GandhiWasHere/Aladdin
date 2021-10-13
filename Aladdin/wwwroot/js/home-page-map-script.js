var map;
var marker


const locationList = {
  nemo: { lat: -48.876667, lng: -123.393333 },
  antartica: {lat: -62.940122577017675, lng:-60.55539982795775},
  mosscow: {lat: 56.406907832198044, lng: 37.68425713399322},
  birTawil: {lat: 21.900277058794284, lng: 33.62077231035544}
}

// Initialize and add the map
function initMap() {
  // The map, centered at Nemo
  map = new google.maps.Map(document.getElementById("map"), {
    zoom: 2,
    center: locationList.nemo,
  });
  // The marker, positioned at location
  marker = new google.maps.Marker({
    position: locationList.nemo,
    map: map,
  });
}

function panMap( loc, newCenter ) {
    marker.setPosition(newCenter);
    switch (loc) {
        case 'antartica':
            map.setZoom(10);
            break;
        case 'nemo':
            map.setZoom(2);
            break;
        default:
            map.setZoom(8);
            break;
    }
  map.panTo(newCenter);
}

const mapBtns = {
  nemo: 'btn-nemo',
  antartica: 'btn-antartica',
  mosscow: 'btn-mosscow',
  birTawil: 'btn-birTawil'
}

for (const [key, value] of Object.entries(mapBtns)) {
  document.getElementById(value).addEventListener('click', () => {
    panMap(key, locationList[key]);
  });
}

function toProducts() {
    $.get('/products/index', {}, function (data, status) {
        
        $("body").html(data);
    })
}

$('#intro-left-cta').click(toProducts);

const cta = document.getElementById('intro-left-cta');
cta.addEventListener('click', () => {
    toProducts();
});