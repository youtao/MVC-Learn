$(function () {
    _page.loadData();
});

var _page = {
    init: function () {
        this.loadData();
    },
    loadData: function () {
        var _this = this;
        $.get('/assets/json/menus.json', null, function (res) {
            var tbody = _this.recursiveMenu(res);
            $('#menu-table > tbody').html(tbody);
            _this.menuListener();
        });
    },
    recursiveMenu: function (children, level) {
        if (!level) level = 0;
        var tbody = '';
        for (var i = 0; i < children.length; i++) {
            var item = children[i];
            var tr = '<tr data-level="' + level + '">';
            tr += '<td></td>';


            var style = ' style="text-align: left;';
            var px = 10;
            if (level >= 1) {
                px += 30 * level;
            }
            style += 'padding-left:' + px + 'px;" ';
            tr += '<td ' + style + '>';
            if (item.children.length > 0) {
                tr += '<i class="fa fa-minus-square-o tree-operator"></i> ';
                tr += '<i class="fa fa-folder-open tree-folder"></i> ';
            } else {
                tr += '<i class="fa fa-file-text-o"></i> ';
            }
            tr += item.title + '</td>';

            tr += '<td>' + decodeURI(item.url) + '</td>';
            tr += '<td><i class="' + item.icon + '"></i> ' + item.icon + '</td>';
            tr += '</tr>';
            if (item.children.length > 0) {
                tr += arguments.callee(item.children, level + 1);
            }
            tbody += tr;
        }
        return tbody;
    },
    menuListener: function () {
        var _this = this;
        $('#menu-table').on('click', '.tree-operator', function (e) {
            var $this = $(this);
            var $tr = $this.parent().parent();
            var level = parseInt($tr.attr('data-level'));
            var $trNextAll = $tr.nextAll();
            if ($this.hasClass('fa-minus-square-o')) { // 打开状态 --> 关闭状态
                for (var i = 0; i < $trNextAll.length; i++) {
                    var $trNext = $($trNextAll[i]);
                    if (parseInt($trNext.attr('data-level')) === level) break; // 遇到同级不在继续
                    $trNext.hide();
                    var $mnius = $trNext.find('>td:nth-child(2) >.fa-minus-square-o');
                    _this.collapseMenu($mnius);
                }
                _this.collapseMenu($this);
            } else { // 关闭状态 --> 打开状态
                var nextLevel = level + 1; // 下一级
                for (var j = 0; j < $trNextAll.length; j++) {
                    var $trNext = $($trNextAll[j]);
                    var dataLevel = parseInt($trNext.attr('data-level'));
                    if (dataLevel === nextLevel) $trNext.show();

                    else {
                        if (dataLevel === level) break; // 遇到同级不在继续
                        if (dataLevel !== nextLevel) continue; // 下下一级
                    }
                }
                _this.expandMenu($this);
            }
        });
    },
    expandMenu: function ($plus) {
        $plus.removeClass('fa-plus-square-o');
        $plus.addClass('fa-minus-square-o');
        $plus.next().removeClass('fa-folde');
        $plus.next().addClass('fa-folder-open');
    },
    collapseMenu: function ($mnius) {
        $mnius.removeClass('fa-minus-square-o');
        $mnius.addClass('fa-plus-square-o');
        $mnius.next().removeClass('fa-folder-open');
        $mnius.next().addClass('fa-folder');
    }
}