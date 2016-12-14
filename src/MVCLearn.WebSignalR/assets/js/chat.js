$(function () {
    var chathub = $.connection.chatHub;
    chathub.client.start = function (connectionid) {
        console.log('连接:' + connectionid);
    };
    chathub.client.leave = function (connectionid) {
        console.log('离开:' + connectionid);
    };
    chathub.client.sendMessages = function (connectionid, messages) {
        console.log(connectionid + ':' + messages);
    };

    $.connection.hub.start().done(function () {
        //chathub.server.start();
    });



    $('#send-messages').click(function () {
        var messages = $('#messages').val();
        console.log(messages);
        chathub.server.sendMessages(messages);
    });
});