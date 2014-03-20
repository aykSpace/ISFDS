function AddData() {
    _this = this;

    this.formSubmitt = function onSubmit() {
        $('#addForm').unbind('submit').bind('submit',function () {
            $.post(this.action, $('#addForm').serialize())
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