function AddMic() {
    _this = this;

    this.formSubmitt = function onSubmit() {
        $('#addMicForm').unbind('submit').bind('submit',function () {
            $.post(this.action, $('#addMicForm').serialize())
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

var addMic = null;
$().ready(function () {
    addMic = new AddMic();
    addMic.formSubmitt();
});