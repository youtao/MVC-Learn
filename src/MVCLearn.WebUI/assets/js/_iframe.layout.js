$(function () {
    _layout.init();
});
var _layout = {
    init: function () {
        var _this = this;
        _this.initPageTitle();
        $('#page-body').resize(function () {
            _this.onresize();
        });
        $(window).resize(function () {
            _this.onresize();
        });
        _this.chatScrollbal();
        setTimeout(function () { // 防止第一次初始化与ajax冲突
            var $pageFooter = $('#page-footer');
            if ($pageFooter.is(':hidden')) {
                _this.onresize();
            }
        }, 100);
        _this.documentOnClick();
    },
    initPageTitle: function () { // 头部导航
        var $pageTitle = $('#page-title');
        if ($pageTitle.html().trim().length > 0) {
            $pageTitle.show();// 没有内容就显示
        }
    },
    onresize: function (event) { // 页面大小改变事件
        var height = $(window).height();
        var $pageTitle = $('#page-title');
        var $pageBody = $('#page-body');
        var $pageFooter = $('#page-footer');
        var marginTop = height -
            $pageBody.outerHeight() -
            $pageFooter.outerHeight() - 20;
        if (!$pageTitle.is(':hidden')) { // 如果隐藏了就不减去高度
            marginTop -= $pageTitle.outerHeight();
        }
        if (marginTop > 0) {
            $pageFooter.css({
                marginTop: marginTop
            });
        } else {
            $pageFooter.css({
                marginTop: 30
            });
        }
        $pageFooter.show();//初始化完后再显示
    },
    chatScrollbal: function () { // 聊天框滚动条
        $('div.chat-input').perfectScrollbar();
    },
    documentOnClick: function () {
        if (window != window.parent) { // 如果是在iframe中
            $(document).click(function (event) {
                $(window.parent.document)
                    .find('#page-heard ul.nav-menu > li.item') //收起父级通知栏
                    .removeClass('open');
            });
        }
    }
};