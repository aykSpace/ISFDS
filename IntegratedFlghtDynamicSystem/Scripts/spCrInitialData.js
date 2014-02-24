var $container = $('#mic-container');
function showMassInerCharacteristic(url) {
    
    $container.load(url, null, function() {
        changeClasses();
    });
}

function changeClasses() {
    $('#spcr-init-data').removeClass('col-lg-offset-1 col-lg-10');
    $('#spcr-init-data').addClass('col-lg-offset-1 col-lg-4');

    $('#test').on('change', function () {
        var url = '/ISS/MassInertialCharachteristic?idCharacteristic=' + $('#test option:selected').text();
        alert(url);
        //$container.load(url);
    } ); 
    
    var i = 0;
}


$(document).ready(function () {

    $('td a').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        showMassInerCharacteristic(url);
    });
    $('.text-right a').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        showMassInerCharacteristic(url);
    });
});