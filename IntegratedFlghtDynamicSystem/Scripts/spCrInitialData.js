var $container = $('#mic-container');
//loading guidance panel with data
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
    var $check = $('#check');
    var $edit = $('#edit');
    var $delete = $('#delete');
    var editUrl = $edit.attr('href');
    var editNewUrl = editUrl.slice(0, editUrl.lastIndexOf('\/'));
    var deleteUrl = $delete.attr('href');
    var deleteNewUrl = deleteUrl.slice(0, deleteUrl.lastIndexOf('\/'));

    var checkUrl = $check.attr('href');
    $('#aval-mic').on('change', function () {
        var url = '/ISS/GetMic/' + $('#aval-mic option:selected').text();
        $check.attr('href', checkUrl + '\/' + $('#aval-mic option:selected').text());
        $edit.attr('href', editNewUrl + '\/' + $('#aval-mic option:selected').text());
        $delete.attr('href', deleteNewUrl + '\/' + $('#aval-mic option:selected').text());
        showMassInerData(url);
    } ); 
}

//loading mass inertial characteristic data

function showMassInerData(url) {
    $('#massInerData').empty();
    $.getJSON(url, null, function (mic) {
        var re = /-?\d+/;
        var m = re.exec(mic.DateOfID);
        mic.DateOfID = new Date(parseInt(m[0])).toLocaleDateString();
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
    $('.col-lg-offset-1 .col-lg-5 a').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        showMassInerCharacteristicGuidePanel(url);
    });
});