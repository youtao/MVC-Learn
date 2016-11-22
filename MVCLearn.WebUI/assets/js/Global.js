$(function () {
    letfMenu();
    $('#page-body').resize(function (e) {
        footer();
    });
});
var GlobalConfig = {
    Server: 'http://localhost:33826/api/'
};
function letfMenu() {
    $.get('/assets/json/menus.json', null, function (res) {
        var html = recursiveMenu(res);
        $('#left-menu-inner-heard').after(html);
        $('#left-menu-inner').perfectScrollbar();
    });
}
function recursiveMenu(children) {
    var ul = '<ul>';
    for (var i = 0; i < children.length; i++) {
        var item = children[i];
        var li = '<li>';
        li += '<a href="javascript:void(0)"; data-url="' + item.url + '">' + item.title + '</a>';
        if (item.children.length > 0) {
            li += arguments.callee(item.children);
        }
        li += '</li>';
        ul += li;
    }
    ul += '</ul>';
    return ul;
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