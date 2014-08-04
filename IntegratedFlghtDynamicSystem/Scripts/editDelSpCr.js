function DelHandler() {
    _this = this;

    this.init = function () {
        _this.initDialog();
        _this.addDelHandler();
    };

    this.initDialog = function () {
        $('#dialog').dialog({
            autoOpen: false,
            modal: true
        });
    };
    this.addDelHandler = function () {
        $('.deleteSpCr').unbind('click');
        $('.deleteSpCr').click(function (e) {
            e.preventDefault();
            var targetUrl = $(this).attr('href');
            $('#dialog').dialog({
                buttons: {
                    "Confirm": function () {
                        $.ajax({
                            url: targetUrl,
                            type: 'DELETE',
                            statusCode: {
                                404: function (response) {
                                    $('#errorResult').show();
                                }
                            }
                        })
                            .done(function () {
                                location.reload();
                            })
                            .fail(function () {
                                $('#dialog').dialog('close');
                                $('#errorResult').show();
                            });
                    },
                    "Cancel": function () {
                        $(this).dialog('close');
                    }
                }
            });
            $('#dialog').dialog('open');
        });

    };
}


function EditHandler() {

    _this = this;
    var submitUrl = null;

    this.init = function () {
        _this.addEditHandelr();
        /*for (var i = 0; i < 10; i++) {
           / * (function(i) {
                setTimeout(function () {
                    console.log(i);
                }, 0);
            })(i);* /
            //---------------------------
            / *setTimeout(function (i) {
                console.log(i);
            }.bind(undefined, i), 0);* /

        }*/
    };

    this.addEditHandelr = function () {
        $('.editSpCr').unbind('click');
        $('.editSpCr').click(function (e) {
            e.preventDefault();
            var targetUrl = $(this).attr('href');
            submitUrl = $(this).data('ref');
            _this.showPopup(targetUrl, initEditPopup);
        });

    };

    this.showPopup = function (url, callback) { //public methods
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                showModalData(data, callback);
            }
        });
    };

    function initEditPopup(modal) {        //private methods
        $('#editButton').click(function () {
            $.ajax({
                type: "PUT",
                url: submitUrl,
                data: $('#editSpCrForm').serialize(),
            }).done(function (data) {
                showModalData(data);
                initEditPopup(modal);
                location.reload();
            })
                .fail(function () {
                    $('#errorEditResult').show();
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

var Del = null;
var Edit = null;
$(document).ready(function () {
    Del = new DelHandler;
    Del.init();
    Edit = new EditHandler;
    Edit.init();
});