﻿function Mics() {
    _this = this;
    var $container = $('#mic-container');
    var $enginecontainer = $('#engine-container');
    this.init = function () {
        $('td a').off('click');
        $('td a').click(function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            showMassInerCharacteristicGuidePanel(url);
        });
        
        $('.col-lg-offset-1 .col-lg-5 a').off('click');
        $('.col-lg-offset-1 .col-lg-5 a').click(function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            showMassInerCharacteristicGuidePanel(url);
        });
    };

    //init available engines
    this.initEngine = function() {
        $('#engineGuidence a').off('click');
        $('#engineGuidence a').click(function(e) {
            e.preventDefault();
            _this.changeClasses();
            var url = $(this).attr('href');
            showEngineGuidePanel(url);
        });
        
        $('#engines-ref a').off('click');
        $('#engines-ref a').click(function (e) {
            e.preventDefault();
            _this.changeClasses();
            var url = $(this).attr('href');
            showEngineGuidePanel(url);
        });

    };
    
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

    //change class in index view
    this.changeClasses = function() {
        $('#spcr-init-data').removeClass();
        $('#spcr-init-data').addClass('col-lg-offset-1 col-lg-5');
    };

    //and correct urls to guidance buttons
    function addHandlers() {
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
        });

    }

    //loading mass inertial characteristic data

    function showEngineData(url) {
        $('#engineInerData').empty();
        $.getJSON(url, null, function (engine) {
            $('#engineTmp').tmpl(engine)
                .appendTo('#engineInerData');
        });
    }


    //loading guidance panel with mic data
    function showMassInerCharacteristicGuidePanel(url) {

        $container.load(url, null, function () {
            var url1 = $('#getJsonMic').attr('href') + url.substring(url.lastIndexOf('\/'));
            _this.changeClasses();
            addHandlers();
            showMassInerData(url1);
        });
    }
    
    //loading guidance panel with engine data
    function showEngineGuidePanel(url) {
        $enginecontainer.load(url, null, function () {
            var url1 = $('#getJsonEngine').attr('href') + url.substring(url.lastIndexOf('\/'));
            _this.changeClasses();
            showEngineData(url1);
        });
    }



}

var mics = null;
$(document).ready(function () {
    mics = new Mics();
    mics.init();
    mics.initEngine();
});