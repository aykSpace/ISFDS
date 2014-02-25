var $container = $('#mic-container');
function showMassInerCharacteristicGuidePanel(url) {
    
    $container.load(url, null, function () {
        var url1 = '/ISS/GetMic' + url.substring(url.lastIndexOf('\/'));
        changeClasses();
        showMassInerData(url1);
    });
}

function changeClasses() {
    $('#spcr-init-data').removeClass('col-lg-offset-1 col-lg-10');
    $('#spcr-init-data').addClass('col-lg-offset-1 col-lg-4');
    
    $('#aval-mic').on('change', function () {
        var url = '/ISS/GetMic/' + $('#aval-mic option:selected').text();
        showMassInerData(url);
    } ); 
}

function showMassInerData(url) {
    $('#massInerData').empty();
    $.getJSON(url, null, function (mic) {
        var re = /-?\d+/;
        var m = re.exec(mic.DateOfID);
        mic.DateOfID = new Date(parseInt(m[0])).toLocaleString();
        $('#massInerTmp').tmpl(mic)
            .appendTo('#massInerData');

    });
}


$(document).ready(function () {

    $('td a').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        showMassInerCharacteristicGuidePanel(url);
    });
    $('.text-right a').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        showMassInerCharacteristicGuidePanel(url);
    });
});