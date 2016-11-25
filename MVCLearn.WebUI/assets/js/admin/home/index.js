$(function () {
    _page.init();
});

var _page = {
    iframeSrc: '',
    lastLoadIframeTime: -1,
    init: function () {
        $(window).resize(this.onresize);
        this.initMenu();
        this.initIframe(GlobalConfig.IframeSrc);
        this.eventListener();
        this.scrollbalMenu();
    },
    initMenu: function () {
        var _this = this;
        $.get('/assets/json/menus.json', null, function (res) {
            var html = _this.recursiveMenu(res);
            var id = ' id="menu-main" ';
            var start = html.substr(0, 3);
            var end = html.substring(3, html.length);
            html = start + id + end;
            $('#menu-inner').append(html);
            _this.menuListener();
        });
    },
    recursiveMenu: function (children, level) {
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
    },
    menuListener: function () {
        var _this = this;
        $('#menu-inner').on('click', 'li', function (e) {
            e.stopPropagation(); // 阻止事件冒泡
            var $li = $(this);
            if ($li.hasClass('has-sub')) { // 有子菜单
                if ($li.hasClass('expanded')) _this.collapseMenu($li); // 已经展开过了,折叠
                else _this.expandMenu($li); // 没有展开过,展开
            } else {
                _this.collapseMenu($li.siblings('.expanded')); // 折叠同级菜单
                var $a = $li.find('> a');
                var url = $a.attr('data-url');
                if (url !== 'javascript:void(0);') {
                    _this.initIframe(url);
                    $('#menu-inner a.active').removeClass('active');
                    $li.children('a').addClass('active');
                }
            }
        });
    },
    expandMenu: function ($li) {
        $li.addClass('expanded');
        this.collapseMenu($li.siblings('.expanded')); // 折叠同级菜单
        var $ul = $li.children('ul');
        $ul.css('display', 'block');
        var height = $ul.outerHeight();
        $ul.css('height', 0);
        $ul.animate({ height: height }, 500, function () {
            $ul.css('height', '');
        });
    },
    collapseMenu: function ($li) {
        $li.removeClass('expanded');
        var $ul = $li.children('ul');
        $ul.css('height', $ul.outerHeight());
        $ul.animate({ height: 0 }, 500, function () {
            $ul.css('height', '');
            $li.find('ul').css('display', 'none'); // 子元素
            $li.find('li').removeClass('expanded'); // 子元素
        });
    },
    initIframe: function (src) {
        if (!src) return;
        this.onresize();
        this.lastLoadIframeTime = new Date().getTime();
        var iframe = '<iframe src="' + src + '" onload="iframeOnLoad();"></iframe>';
        $('#page-iframe').html(iframe);
        this.iframeSrc = src;
    },
    onresize: function () {
        var height = $(window).height();
        var $pageHeard = $('#page-heard');
        var $pageIframe = $('#page-iframe');
        height = height - $pageHeard.outerHeight();
        $pageIframe.css('height', height);
    },
    eventListener: function () {
        $('#page-heard li.menu-nav-item')
            .click(function () {
                $(this).siblings().removeClass('open');
                if ($(this).hasClass('open')) $(this).removeClass('open');
                else $(this).addClass('open');
            });
    },
    scrollbalMenu: function () {
        $('#menu-inner').perfectScrollbar();
        $('.menu-message').perfectScrollbar();
    }
};

function iframeOnLoad() {
    var start = _page.lastLoadIframeTime;
    if (start < 0) return;
    var now = new Date();
    var time = now.getTime() - start;
    time = (time / 1000).toFixed(2);
    var title = now.Format('yyyy-MM-dd hh:mm');
    var message = '页面加载完成(耗时:' + time + 's)';
    window.toastr.success(message, title, {
        closeButton: true,
        positionClass: "toast-top-right",
        timeOut: "5000"
    });
    _page.lastLoadIframeTime = -1; // 只在每个iframe第一次加载时才弹提示
}