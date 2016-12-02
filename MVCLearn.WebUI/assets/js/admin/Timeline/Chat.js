$(function () {
    onresize();
    $(window).resize(onresize);
    $('#input-area').resize(onresize);
    var vue = new Vue({
        el: '#chat-app',
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
            username: '',
            time: new Date().Format('hh:mm:ss'),
            content: messages
        });
    };
    $.connection.hub.start().done(function () {

    });
    $('#send-messages').click(function () {
        var messages = $('#chat-input').text();
        $('#chat-input').text('');
        vue.messages.push({
            aligned: 'right-aligned',
            icon: 'fa-database',
            username: '',
            time: new Date().Format('hh:mm:ss'),
            content: messages
        });
        chathub.server.sendMessages(messages);
        $('#send-messages').attr('disabled', 'disabled');
    });

    $('#chat-conversation').perfectScrollbar();
    $('div.chat-input').perfectScrollbar();

    $('#chat-input').bind('input propertychange', function () {
        var text = $(this).text();
        if (text.trim().length>0) {
            $('#send-messages').removeAttr('disabled');
        } else {
            $('#send-messages').attr('disabled', 'disabled')
        }
    });
});


function onresize(e) {
    var height = $(window).height();
    var $inputArea = $('#input-area');
    var $chatConversation = $('#chat-conversation');
    var maxHeight = height -
        $inputArea.outerHeight();
    $chatConversation.css('height', maxHeight);
    $('#chat-conversation').scrollTop($('#chat-conversation')[0].scrollHeight);
}