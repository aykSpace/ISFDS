function SetValues() {
    var idNu = $.session.get('idNu');
    $('#vectorNuId').attr("value", idNu);
    if (idNu.length != 0) {
        $('#nuNotFound').hide();
    }
    $("input[name='EnableInput']").click(function() {
        var test = $(this).val();
        enableElement(test);
    });

    var $grafICircle = $('#GrafICircle');
    $grafICircle.change(function() {
        var test = $grafICircle.prop('checked');
        if (test == true) {
            $.session.set('grafICircle', test);
        }
    });
    
    var $grafUCircle = $('#GrafUt');
    $grafUCircle.change(function () {
        var test = $grafUCircle.prop('checked');
        if (test == true) {
            $.session.set('grafUCircle', test);
        }
    });
    
}


$(function() {
    $(document).ajaxError(function(e, xhr) {
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location = response.LogOnUrl;
        }
    });
});

function clearFormElements() {
    $('#clearFormButton').click(function (event) {
        $('#inputPredictEements:first').empty();
    });
    $('#vectorNuId').click(function() {
        this.attr('disabled', false);
    });
}

function enableElement(elementName) {
    var elements = $("#inputPredictEements > div > input[type='text']:not(:first)");
    elements.attr('disabled', true);
    elements.val('');
    $('#'+ elementName).removeAttr('disabled');
}

function displayLoading() {
    $('#predictSubmit').append('<i class="fa fa-spinner fa-spin"></i>');
}


function removeLoading() {
    $('#predictSubmit').html('Посчитать');
}

$(document).ready(SetValues());
