function AjaxPaging() {
    _this = this;

    this.init = function () {
        $('#pager').on('click', 'a', function (e) {
            e.preventDefault();
            if (this.href == undefined) {
                return;
            }
            $.ajax({
                url: this.href,
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#mic-container').html(result);
                }
            });
        });
    };
}

var ajaxPaging = null;
$().ready(function () {
    ajaxPaging = new AjaxPaging();
    ajaxPaging.init();
});