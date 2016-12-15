$(function () {
    _layout.init();
});

var _layout = {
    init: function () {
        var _this = this;
        _this.documentOnClick();
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
}