function AjaxPaging() {
    _this = this;

    this.init = function () {
        onPageClick('pagerMic');
        onPageClick('pagerEngine');
    };

    function onPageClick(pagerId) {

        $('#'+pagerId).on('click', 'li:not(.active) a', function (e) {
            e.preventDefault();
            $.ajax({
                url: this.href,
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#mic-container').html(result);
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