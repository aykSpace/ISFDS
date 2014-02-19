/// <reference path="jquery-1.7.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.20.js" />

$(document).ready(function (e) 
{
    //
    // Try to init the culture drop down list. 
    //
    try 
    {
        $("#_cultureDDL").msDropDown();
    }
    catch (e) 
    {
        alert(e.message);
    }

    //
    // Auto-complete the user input based on an existing attribute.
    //
    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });

    //
    // Activate the DateTime picker control based on an existing attribute.
    //
    $(":input[data-datepicker]").datepicker();

});
