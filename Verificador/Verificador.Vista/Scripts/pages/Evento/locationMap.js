var autocomplete;
function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 6.2530408, lng: -75.56457369999998 },
        zoom: 13,
        streetViewControl: false,
        panControl: false,
        mapTypeControl: true,
        zoomControl: true,
    });
    var input = document.getElementById('Direccion');
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    var infowindow = new google.maps.InfoWindow();
    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, -29)
    });

    autocomplete.addListener('place_changed', function () {
        infowindow.close();
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("Autocomplete's returned place contains no geometry");
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }
        marker.setIcon(({
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(35, 35)
        }));
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        infowindow.open(map, marker);

        //Location details
        for (var i = 0; i < place.address_components.length; i++) {
            if (place.address_components[i].types[0] == 'postal_code') {
                document.getElementById('postal_code').innerHTML = place.address_components[i].long_name;
            }
            if (place.address_components[i].types[0] == 'country') {
                document.getElementById('country').innerHTML = place.address_components[i].long_name;
            }
        }
        document.getElementById('location').innerHTML = place.formatted_address;
        document.getElementById('lat').innerHTML = place.geometry.location.lat();
        document.getElementById('lon').innerHTML = place.geometry.location.lng();

        document.getElementById("Latitud").value = place.geometry.location.lat();
        document.getElementById("Longitud").value = place.geometry.location.lng();

        fillInAddress();
        //showButtonNext(true);
    });

    google.maps.event.addListener(map, 'click', function (event) {
        //placeMarker(event.latLng);

        var service = new google.maps.places.PlacesService(map);
        service.nearbySearch({
            location: event.latLng,
            radius: 500,
            types: ['store']
        }, callback);


    });

    function placeMarker(location) {
        var marker = new google.maps.Marker({
            position: location,
            map: map
        });
    }

    const Venue = EventStorage.getProp("Venue");
    const placeId = Step2.GeoCode || Venue.GeoCode;
    if (placeId !== null && placeId !== "") {
        var geocoder = new google.maps.Geocoder;
        geocodePlaceId(placeId, geocoder, map, infowindow);
    }

    function createMarker(place) {

        marker.setVisible(false);
        //var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("Autocomplete's returned place contains no geometry");
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
        }
        map.setZoom(17);
        marker.setIcon(({
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(35, 35)
        }));
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        infowindow.open(map, marker);

        //Location details

        document.getElementById('location').innerHTML = place.formatted_address == '' ? place.vicinity : place.formatted_address;
        document.getElementById('lat').innerHTML = place.geometry.location.lat();
        document.getElementById('lon').innerHTML = place.geometry.location.lng();
    }

    var componentForm = {
        street_number: 'short_name',
        route: 'long_name',
        locality: 'long_name',
        administrative_area_level_1: 'short_name',
        country: 'long_name',
        postal_code: 'short_name',
    };

    function fillInAddress() {
        // Get the place details from the autocomplete object.
        var place = autocomplete.getPlace();

        document.getElementById('GeoCode').value = place.place_id;

        for (var component in componentForm) {
            document.getElementById(component).value = '';
        }

        // Get each component of the address from the place details
        // and fill the corresponding field on the form.
        for (var i = 0; i < place.address_components.length; i++) {
            var addressType = place.address_components[i].types[0];
            if (componentForm[addressType]) {
                var val = place.address_components[i][componentForm[addressType]];
                document.getElementById(addressType).value = val;
            }
        }

        var ciudad = document.getElementById("locality").value;
        if (ciudad != '') {
            var url = urlBase + "Administracion/get_CiudadByName";
            $.get(url, { text: ciudad }, function (e) {
                if (e != null) {
                    document.getElementById("Ciudad_id").value = e.id;
                }
            });
        }
    }

    function geocodePlaceId(placeId, geocoder, map, infowindow) {
        geocoder.geocode({ 'placeId': placeId }, callback);
    }

    var map;
    var infowindow;

    function initMap() {
        var pyrmont = { lat: -33.867, lng: 151.195 };

        map = new google.maps.Map(document.getElementById('map'), {
            center: pyrmont,
            zoom: 15
        });

        infowindow = new google.maps.InfoWindow();

        var service = new google.maps.places.PlacesService(map);
        service.nearbySearch({
            location: pyrmont,
            radius: 100,
            //types: ['store']
        }, callback);
    }

    function callback(results, status) {
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            //for (var i = 0; i < results.length; i++) {
            createMarker(results[0]);
            //}
        }
    }



}