﻿$(function () {
    if ($('#page-title').html().trim() > 0) {
        $('#page-title').show();// 没有内容就显示
    }
    footer();
    $('#page-body').resize(function () {
        footer();
    });
    $(window).resize(function () {
        footer();
    });
});
var GlobalConfig = {
    Server: 'http://localhost:33826/api/'
};
function footer() {
    var offsetHeight = $(window).height();
    var $pageTitle = $('#page-title');
    var $pageBody = $('#page-body');
    var $pageFooter = $('#page-footer');
    var marginTop = offsetHeight -
        $pageBody.outerHeight() -
        $pageFooter.outerHeight();
    if (!$pageTitle.is(':hidden')) { // 如果隐藏了就不减去高度
        marginTop -= $pageTitle.outerHeight();
    }
    if (marginTop > 0) {
        $pageFooter.css('margin-top', marginTop);
    } else {
        $pageFooter.css('margin-top', 30);
    }
    $pageFooter.show();//初始化完后再显示
}