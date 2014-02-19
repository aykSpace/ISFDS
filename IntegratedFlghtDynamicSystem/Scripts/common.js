// Activates the Carousel
$('.carousel').carousel({
    interval: 5000
});

// Activates Tooltips for Social Links
$('.tooltip-social').tooltip({
    selector: "a[data-toggle=tooltip]"
});

function Common() {
    _this = this;

    this.init = function() {
        $('#loginLink').click(function() {
            _this.showPopup("/Account/AjaxLogin", initLoginPopup);
        });
    }

    this.showPopup = function(url, callback) { //public methods
        $.ajax({
            type: "GET",
            url: url,
            success: function(data) {
                showModalData(data, callback);
            }
        });
    };

    function initLoginPopup(modal) {        //private methods
        $('#LoginButton').click(function() {
            $.ajax({
                type: "POST",
                url: "/Account/AjaxLogin",
                data: $('#AjaxLoginForm').serialize(),
                success: function(data) {
                    showModalData(data);
                    initLoginPopup(modal);
                }
            });
        });
        
    }

    function showModalData(data, callback) {
        $(".modal-backdrop").remove();
        var popupWrapper = $('#PopupWrapper');
        popupWrapper.empty();
        popupWrapper.html(data);
        var popup = $('.modal', popupWrapper);
        $('.modal', popupWrapper).modal();
        if (callback != undefined) {
            callback(popup);
        }
    }


}

var common = null;
$().ready(function() {
    common = new Common();
    common.init();
} );