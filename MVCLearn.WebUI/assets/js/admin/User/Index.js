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
    currentpage.init();
});