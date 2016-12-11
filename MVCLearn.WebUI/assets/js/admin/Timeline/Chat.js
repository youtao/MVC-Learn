$(function () {
    $.getScript(GlobalConfig.SignalrHub, function (data, status, jqXHR) {
        var lastHeight = $('#chat-input').outerHeight();
        onresize();
        $(window).resize(onresize);
        $('#chat-input').resize(onresize);
        var vue = new Vue({
            el: '#chat-app',
            data: {
                messages: []
            }
        });
        $.connection.hub.url = GlobalConfig.SignalR;
        var chathub = $.connection.chatHub;
        chathub.client.sendMessages = function (connectionid, messages) {
            vue.messages.push({
                aligned: 'left-aligned',
                icon: 'fa-lastfm',
                //username: connectionid,
                username: '',
                time: moment().format('HH:mm:ss'),
                content: messages
            });
        };
        $.connection.hub.start();
        $('#send-messages').click(function () {
            var messages = $('#chat-input').text();
            $('#chat-input').text('');
            vue.messages.push({
                aligned: 'right-aligned',
                icon: 'fa-database',
                username: '',
                time: moment().format('HH:mm:ss'),
                content: messages
            });
            chathub.server.sendMessages(messages);
            $('#send-messages').attr('disabled', 'disabled');
            lastHeight = $(this).outerHeight();
        });

        $('#chat-conversation').perfectScrollbar();
        $('div.chat-input').perfectScrollbar();

        $('#chat-input').bind('input propertychange', function () {
            if ($(this).outerHeight() !== lastHeight) {
                $('#chat-conversation').perfectScrollbar('update');
                $('#chat-conversation').scrollTop($('#chat-conversation')[0].scrollHeight);
                onresize();
            }
            lastHeight = $(this).outerHeight();
            var text = $(this).text();
            if (text.trim().length > 0) {
                $('#send-messages').removeAttr('disabled');
            } else {
                $('#send-messages').attr('disabled', 'disabled')
            }
        });

        vue.$watch('messages', function () {
            $('#chat-conversation').perfectScrollbar('update');
            $('#chat-conversation').scrollTop($('#chat-conversation')[0].scrollHeight);
            onresize();
        });
    });
});

function onresize() {
    var $window = $(window);
    var $inputArea = $('#input-area');
    var $chatConversation = $('#chat-conversation');

    var height = $window.height() - $inputArea.outerHeight();
    $chatConversation.css('height', height)
        .scrollTop($('#chat-conversation')[0].scrollHeight);
    $inputArea.show();
}