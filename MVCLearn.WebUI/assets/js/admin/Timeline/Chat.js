$(function () {
    var vue = new Vue({
        el: '#app',
        data: {
            messages: []
        }
    });
    $.connection.hub.url = 'http://localhost:26949/signalr';
    var chathub = $.connection.chatHub;
    chathub.client.start = function (connectionid) {
        console.log('连接:' + connectionid);
    };
    chathub.client.leave = function (connectionid) {
        console.log('离开:' + connectionid);
    };
    chathub.client.sendMessages = function (connectionid, messages) {
        vue.messages.push({
            aligned: 'left-aligned',
            icon: 'fa-lastfm',
            //username: connectionid,
            username: '对方',
            time: new Date().toLocaleTimeString(),
            content: messages
        });
    };
    $.connection.hub.start().done(function () {

    });
    $('#send-messages').click(function () {
        var messages = $('#messages').val();
        $('#messages').val('');
        vue.messages.push({
            aligned: 'right-aligned',
            icon: 'fa-database',
            username: '',
            time: new Date().toLocaleTimeString(),
            content: messages
        });
        chathub.server.sendMessages(messages);
    });
});