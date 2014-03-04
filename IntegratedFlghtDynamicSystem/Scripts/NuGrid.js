function showGrid() {
    $('#_nuGrid').jqGrid({
        caption: paramFromView.Caption,
        colNames: [paramFromView.ID_NU, paramFromView.N_NU, paramFromView.Modification, paramFromView.Circle, paramFromView.X, paramFromView.Y, paramFromView.Z, paramFromView.DateTime],
        colModel: [
            { name: 'ID_NU', index: 'Id', width: 70, key: true },
            { name: 'N_NU', index: 'N_NU', width: 90 },
            { name: 'Modification', index: 'Modification', width: 80 },
            { name: 'Vitok', index: 'Circle', width: 100 },
            { name: 'X', index: 'X', width: 300 },
            { name: 'Y', index: 'Y', width: 300 },
            { name: 'Z', index: 'Z', width: 300 },
            { name: 'DateTime', index: 'DateTime', width: 300 }
            //{ name: 'Check', index: 'Check', width: 120, formatter: "checkbox", align: "center" }
        ],
        hidegrid: false,
        pager: jQuery('#_nuPaper'),
        sortname: 'ID_NU',
        rowList: [10, 20, 50, 100],
        sortorder: "desc",
        width: paramFromView.Width,
        height: paramFromView.Height,
        datatype: 'json',
        caption: paramFromView.Caption,
        viewrecords: true,
        mtype: 'GET',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            userdata: "userdata"
        },
        url: paramFromView.Url
    }).navGrid('#_nuPaper', { view: false, del: false, add: false, edit: false, search: false },
    { width: 400 },
    {}, // default settings for add
    {}, // delete instead that del:false we need this
    {}, // search options
    {} /* view parameters*/).navButtonAdd('#_nuPaper', {
        caption: paramFromView.DeleteAllCaption, buttonimg: "", onClickButton: function () {
            if (confirm(paramFromView.DeleteAllConfirmationMessage)) {
                document.location = paramFromView.ClearGridUrl;
            }
            else {
                $('#_nuGrid').resetSelection();
            }
        }, position: "last"
    });
}

var id;
function getId() {
    $('#jqGrid').on('click', (function (event) {
        var $nuGrid = $('#_nuGrid');
        var selRowId = $nuGrid.jqGrid('getGridParam', 'selrow'),
        celValue = $nuGrid.jqGrid('getCell', selRowId, 'ID_NU');
        id = celValue;
        changeVal(id);
        $('#orbElementsCalc').removeProp('disabled');

    }));
}

function calcOrbElements() {
    $('#orbElementsCalc').click(function (event) {
        event.preventDefault();
        var url = '/ISS/CalculateOrbitElements/' + id;
        var $container = $('#orbElementsCalcContainer');
        $container.load(url);
        $container[0].scrollIntoView();
    });
}

function changeVal(id) {
    $.session.set('idNu', id);
}

$(document).ready(function () {
    showGrid();
    getId();
    calcOrbElements();
});

