$(function () {
    letfMenu();
    footer();
    $('#page-body').resize(function (e) {
        footer();
    });
    $(window).resize(function (e) {
        footer();
    });
});
var GlobalConfig = {
    Server: 'http://localhost:33826/api/'
};
function letfMenu() {
    $.get('/assets/json/menus.json', null, function (res) {
        var html = recursiveMenu(res);
        var classStr = ' id="left-menu-inner-main"';
        var start = html.substr(0, 3);
        var end = html.substring(3, html.length);
        html = start + classStr + end;
        $('#left-menu-inner-heard').after(html);
        $('#left-menu-inner').perfectScrollbar();
        leftMenuListener();
    });
}
function recursiveMenu(children, level) {
    if (!level) level = 0;
    var ul = '<ul>';
    for (var i = 0; i < children.length; i++) {
        var item = children[i];
        var li = '<li>';
        var style = '';
        if (level >= 1) {
            var px = 30 + (level - 1) * 25;
            style = ' style="padding-left:' + px + 'px;" ';
        }
        li += '<a href="javascript:void(0);";' + style + 'data-url="' + item.url + '">';
        var icon = 'fa fa-bars'; // 默认图标
        if (item.icon) icon = item.icon;
        li += '<span class="' + icon + ' menu-icon"></span> ';
        li += '<span class="menu-title">' + item.title + '</span>';
        li += '</a>';
        if (item.children.length > 0) {
            var classStr = ' class="has-sub"';
            var start = li.substr(0, 3);
            var end = li.substring(3, li.length);
            li = start + classStr + end;
            li += arguments.callee(item.children, level + 1);
        }
        li += '</li>';
        ul += li;
    }
    ul += '</ul>';
    return ul;
}
function leftMenuListener() {
    $('#left-menu-inner').on('click', 'li', function (e) {
        e.stopPropagation(); // 阻止事件冒泡
        var _this = $(this);
        if (_this.hasClass('has-sub')) {
            if (_this.hasClass('expanded')) {
                _this.find('ul').css('display', 'none');
                _this.find('li').removeClass('expanded');
                _this.removeClass('expanded');
            } else {
                _this.find('> ul').css('display', 'block');
                _this.addClass('expanded');
            }
        }
    });
}
function footer() {
    var offsetHeight = document.body.offsetHeight;
    var $pageHeard = $('#page-heard');
    var $pageTitle = $('#page-title');
    var $pageBody = $('#page-body');
    var $pageFooter = $('#page-footer');
    var marginTop = offsetHeight -
        $pageHeard.outerHeight() -
        $pageTitle.outerHeight() -
        $pageBody.outerHeight() -
        $pageFooter.outerHeight() - 30;
    if (marginTop > 0) {
        $pageFooter.css('margin-top', marginTop);
    } else {
        $pageFooter.css('margin-top', 30);
    }
}