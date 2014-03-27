function AjaxPaging() {
    _this = this;

    this.init = function () {
        onPageClick('pagerMic', 'mic-container');
        onPageClick('pagerEngine', 'engine-container');
    };

    function onPageClick(pagerId, containerId) {

        $('#'+pagerId).on('click', 'li:not(.active) a', function (e) {
            e.preventDefault();
            $.ajax({
                url: this.href,
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#' + containerId).html(result);
                }
            });

        });
        
    }
}

var ajaxPaging = null;
$().ready(function () {
    ajaxPaging = new AjaxPaging();
    ajaxPaging.init();
});