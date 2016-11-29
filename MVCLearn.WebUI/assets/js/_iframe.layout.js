$(function () {
    _layout.init();
});
var _layout = {
    init: function () {
        var _this = this;
        this.initPageTitle();
        $('#page-body').resize(this.onresize);
        $(window).resize(this.onresize);
        setTimeout(function () { // 防止第一次初始化与ajax冲突
            var $pageFooter = $('#page-footer');
            if ($pageFooter.is(':hidden')) {
                _this.onresize();
           }
        }, 100);
    },
    initPageTitle: function () {
        var $pageTitle = $('#page-title');
        if ($pageTitle.html().trim().length > 0)
            $pageTitle.show();// 没有内容就显示

    },
    onresize: function (e) {
        var height = $(window).height();
        var $pageTitle = $('#page-title');
        var $pageBody = $('#page-body');
        var $pageFooter = $('#page-footer');
        var marginTop = height -
            $pageBody.outerHeight() -
            $pageFooter.outerHeight();
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
    }
};