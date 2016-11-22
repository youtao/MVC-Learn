var currentpage = {
    init: function () {
        var vue_users = new Vue({
            el: '#vue-users',
            data: {
                users: []
            }
        });
        this.loadData(vue_users);
    },
    loadData: function (vue_users) {
        var _this = this;
        $.get(GlobalConfig.Server + 'User', null, function (res) {
            _this.fillData(vue_users, res);
        });
    },
    fillData: function (vue_users, res) {
        vue_users.users = res.data;
    }
};
$(function () {

    loadLeftMenu();
    currentpage.init();
});

function loadLeftMenu() {
    $.get('/assets/json/menus.json', null, function (res) {
        builderMenu(res);
    });
}

function builderMenu(menus) {
    var html = recursiveMenu(menus);
    $('#left-menu').html(html);
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