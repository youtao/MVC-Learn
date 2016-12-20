$(function () {
    _page.init();
});

var _page = {
    iframeSrc: '',
    lastLoadIframeTime: -1,
    init: function () {
        var _this = this;
        $(window).resize(function () {
            _this.onresize();
        });
        _this.initMenu();
        _this.loadIframe(GlobalConfig.Iframe);
        _this.initNotification();
        _this.documentOnClick();
    },
    initMenu: function () { // 初始化菜单
        var _this = this;
        $.get('/assets/json/menus.json', null, function (res) {
            var html = _this.recursiveMenu(res);
            var attr = ' id="menu-main" class="menu-main" ';
            var start = html.substr(0, 3);
            var end = html.substring(3, html.length);
            html = start + attr + end;
            $('#menu-inner').append(html)
                            .perfectScrollbar();
            _this.menuListener();
        });
    },
    recursiveMenu: function (children, level) { // 递归加载菜单
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
            var icon = 'fa fa-sitemap'; // 默认图标
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
    },
    menuListener: function () { // 监听菜单点击事件
        var _this = this;
        $('#left-menu').on('click', 'a', function (event) {
            var $a = $(this);
            var $li = $a.parent();
            if ($li.hasClass('has-sub')) {
                if ($li.hasClass('expanded')) _this.collapseMenu($li);
                else _this.expandMenu($li);
            } else {
                _this.collapseMenu($li.siblings('.expanded'));
                var url = $a.attr('data-url');
                if (url !== 'javascript:void(0);') {
                    _this.loadIframe(url);
                    $('#left-menu a.active').removeClass('active');
                    $a.addClass('active');
                }
            }
        });
    },
    expandMenu: function ($li) {//展开菜单
        $li.addClass('expanded');
        this.collapseMenu($li.siblings('.expanded')); // 折叠同级菜单
        var $ul = $li.children('ul');
        $ul.css('display', 'block');
        var height = $ul.outerHeight();
        $ul.css('height', 0);
        $ul.animate({ height: height }, 500, function () {
            $ul.css('height', '');
            $('#menu-inner').perfectScrollbar('update'); // 每次展开或折叠菜单就更新滚动条
        });
    },
    collapseMenu: function ($li) { // 折叠菜单
        $li.removeClass('expanded');
        var $ul = $li.children('ul');
        $ul.css('height', $ul.outerHeight());
        $ul.animate({ height: 0 }, 500, function () {
            $ul.css('height', '');
            $li.find('ul').css('display', 'none'); // 子元素
            $li.find('li').removeClass('expanded'); // 子元素
            $('#menu-inner').perfectScrollbar('update'); // 每次展开或折叠菜单就更新滚动条
        });
    },
    loadIframe: function (src) { // 加载iframe
        if (!src) return;
        this.onresize();
        this.lastLoadIframeTime = new Date().getTime();
        src += '?from=iframe';
        var iframe = '<iframe src="' + src + '" onload="iframeOnLoad();"></iframe>';
        $('#page-iframe').html(iframe);
        this.iframeSrc = src;
    },
    onresize: function () { // 页面大小改变事件
        var height = $(window).height();
        var $pageHeard = $('#page-heard');
        var $pageIframe = $('#page-iframe');
        height = height - $pageHeard.outerHeight();
        $pageIframe.css('height', height);
    },
    initNotification: function () { // 初始化通知栏
        $('#page-heard ul.nav-menu > li.item')
            .click(function () {
                $(this).siblings().removeClass('open');
                if ($(this).hasClass('open')) $(this).removeClass('open');
                else $(this).addClass('open');
            });
        $('#page-heard ul.messages').perfectScrollbar();
    },
    documentOnClick: function () {
        $(document).click(function (event) {
            var inArea = $(event.target).isChildAndSelfOf('#page-heard ul.nav-menu > li.item');
            if (!inArea) { // 只要点了其它区域就收起通知栏
                $('#page-heard ul.nav-menu > li.item').removeClass('open');
            }
        });
    }
};

function iframeOnLoad() {
    var start = _page.lastLoadIframeTime;
    if (start < 0) return;
    var now = new Date();
    var time = now.getTime() - start;
    time = (time / 1000).toFixed(2);
    var title = moment().format('L');
    var message = '页面加载完成(耗时:' + time + 's)';
    window.toastr.success(message, title, {
        closeButton: true,
        positionClass: "toast-top-right",
        timeOut: "5000"
    });
    _page.lastLoadIframeTime = -1; // 只在每个iframe第一次加载时才弹提示
}