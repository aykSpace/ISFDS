//function getData() {
//    $('#nuNotFound').hide();
//    $.getJSON('/ISS/GetChartsData', function (result) {
//        var line = new Morris.Line({
//            element: 'morris-chart-line',
//            data: result,
//            parseTime: false,
//             The name of the data record attribute that contains x-visitss.
//            xkey: 'XValue',
//             A list of names of data record attributes that contain y-visitss.
//            ykeys: ['YValue'],
//             Labels for the ykeys -- will be displayed when you hover over the
//             chart.
//            labels: ['YValue']
//        });
//    }).error(function () {
//        $('#nuNotFound').show();
//    }).always(function () {
//        console.log("complete");
//    });
//}


function getData(containerId, nameSeries, id) {
    $('#nuNotFound').hide();
    $.getJSON('/ISS/GetChartsData/'+ id, function (result) {
        $('#'+containerId).dxChart({
            dataSource: result,
            commonSeriesSettings: {
                argumentField: "XValue",
                type: "spline"
            },
            series: [{ valueField: "YValue", name: nameSeries }],
            argumentAxis: {
                grid: {
                    visible: true
                }
            },
            tooltip: { enabled: true },
            //title: "Test diagram",
            legend: {
                verticalAlignment: "bottom",
                horizontalAlignment: "center"
            },
            commonPaneSettings: {
                border: {
                    visible: true,
                    right: false
                }
            }
            
        });
    }).error(function () {
        $('#nuNotFound').show();
    }).always(function () {
        console.log("complete");
    });
}

$(document).ready(function () {

    var grafICircle = $.session.get('grafICircle');
    var grafUCircle = $.session.get('grafUCircle');
    if (grafICircle != undefined) {
        getData('icircle-line', 'Наклонение', 0);
        $.session.remove('grafICircle');
    }
    if (grafUCircle != undefined && grafICircle != undefined) {
        getData('ucircle-line', 'Большая полуось', 1);
        $.session.remove('grafUCircle');
    } else if (grafUCircle != undefined && grafICircle == undefined) {
        getData('ucircle-line', 'Большая полуось', 0);
        $.session.remove('grafUCircle');
    }
    
    
});