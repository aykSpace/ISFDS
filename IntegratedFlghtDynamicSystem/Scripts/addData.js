function AddData() {
    _this = this;

    this.formSubmitt = function onSubmit() {
<<<<<<< HEAD
        $('#form').unbind('submit').bind('submit',function () {
=======
        $('#form').unbind('submit').bind('submit', function () {
>>>>>>> DataCenterSupport
            $.post(this.action, $('#form').serialize())
                .success(function (result) {
                    $('#message').html(result.Message);
                    $('#successResult').show();
                }).error(function() {
                    $('#errorResult').show();
                });
            return false;
        });
    };
}

var addData = null;
$().ready(function () {
    addData = new AddData();
    addData.formSubmitt();
});