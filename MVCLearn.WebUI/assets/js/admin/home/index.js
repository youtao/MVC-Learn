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
        var id = ' id="menu-main" ';
        var start = html.substr(0, 3);
        var end = html.substring(3, html.length);
        html = start + id + end;
        $('#menu-inner').append(html);
        $('#menu-inner').perfectScrollbar();
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
            var classStr = ' class="has-sub" ';
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
    $('#menu-inner').on('click', 'li', function (e) {
        e.stopPropagation(); // 阻止事件冒泡
        var $li = $(this);
        if ($li.hasClass('has-sub')) { // 有子菜单
            if ($li.hasClass('expanded')) collapseMenu($li); // 已经展开过了,折叠
            else expandMenu($li); // 没有展开过,展开
        } else {
            collapseMenu($li.siblings('.expanded')); // 折叠同级菜单
            var $a = $li.find('> a');
            var url = $a.attr('data-url');
            if (url !== 'javascript:void(0);') {
                CurrentScr = url;
                loadIFrame();
            }
        }
    });
}
function expandMenu($li) { // 展开菜单
    $li.addClass('expanded');
    collapseMenu($li.siblings('.expanded')); // 折叠同级菜单
    var $ul = $li.children('ul');
    $ul.css('display', 'block');
    var height = $ul.outerHeight();
    $ul.css('height', 0);
    $ul.animate({ height: height }, 500, function () {
        $ul.css('height', '');
    });
}
function collapseMenu($li) { // 折叠菜单
    $li.removeClass('expanded');
    var $ul = $li.children('ul');
    $ul.css('height', $ul.outerHeight());
    $ul.animate({ height: 0 }, 500, function () {
        $ul.css('height', '');
        $li.find('ul').css('display', 'none'); // 子元素
        $li.find('li').removeClass('expanded'); // 子元素
    });
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