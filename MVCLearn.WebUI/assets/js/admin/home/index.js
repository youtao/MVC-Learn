var CurrentScr = undefined;
$(function () {
    letfMenu();
    $(window).resize(function () {
        onresize();
    });
});
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
        li += '<a href="javascript:void(0);"' + style + 'data-url="' + item.url + '">';
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
        var $li = $(this);
        if ($li.hasClass('has-sub')) { // 有子集
            if ($li.hasClass('expanded')) {
                $li.find('ul').css('display', 'none'); // 子元素
                $li.find('li').removeClass('expanded'); // 子元素
                $li.removeClass('expanded'); // 当前
            } else {
                $('#left-menu-inner-main ul').css('display', 'none');
                $('#left-menu-inner-main li').removeClass('expanded');
                expandMenu($li);
            }
        } else {
            if ($li.parent().attr('id') === 'left-menu-inner-main') {
                $('#left-menu-inner-main ul').css('display', 'none');
                $('#left-menu-inner-main li').removeClass('expanded');
            }
            var $a = $li.find('> a');
            var url = $a.attr('data-url');
            if (url !== 'javascript:void(0);') {
                CurrentScr = url;
                loadIFrame();
            }
        }
    });
}
function expandMenu($element) {
    if ($element.is('li')) {
        $element.addClass('expanded');
        $element.find('> ul').css('display', 'block');
        arguments.callee($element.parent());
    } else if ($element.is('ul')) {
        var id = $element.attr('id');
        if (id !== 'left-menu-inner-main') {
            arguments.callee($element.parent());
        }
    }
}
function loadIFrame() {
    if (!CurrentScr) return;
    onresize();
    var iframe = '<iframe style="padding: 0; height:100%; width: 100%; border: none; overflow-x: hidden;' +
                 '"src="' + CurrentScr + '"></iframe>';
    $('#page-iframe').html(iframe);
}
function onresize() {
    var offsetHeight = document.body.offsetHeight;
    var $pageHeard = $('#page-heard');
    var $pageIframe = $('#page-iframe');
    var height = offsetHeight - $pageHeard.outerHeight();
    $pageIframe.css('height', height);
}