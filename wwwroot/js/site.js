// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var map;
var geocoder;
function InitializeMap() {

    var latlng = new google.maps.LatLng(30.4213, -87.2169);
    var myOptions =
    {
        zoom: 13,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: false
    };
    map = new google.maps.Map(document.getElementById("map"), myOptions);
}

function FindLocaiton() {
    geocoder = new google.maps.Geocoder();
    InitializeMap();

    var address = document.getElementById("addressinput").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });

        }
        else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });

}


function Button1_onclick() {
    FindLocaiton();
}

window.onload = InitializeMap;