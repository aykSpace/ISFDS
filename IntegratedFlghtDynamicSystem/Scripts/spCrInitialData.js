function showMassInerCharacteristic(url) {
    var $container = $('#mic-container');
    $container.load(url);
    $('#spcr-init-data').removeClass('col-lg-offset-1 col-lg-10');
    $('#spcr-init-data').addClass('col-lg-offset-1 col-lg-4');
}


$(document).ready(function () {

    $('.list-group-item  a').click(function(e) {
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