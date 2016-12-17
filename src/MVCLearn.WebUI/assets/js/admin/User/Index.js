$(function () {
    _page.init();
});
var _page = {
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
        $.ajax({
            url: GlobalConfig.WebApi + 'User/GetAllUsers',
            type: 'GET',
            dataType: 'json',
            xhrFields: {
                withCredentials: true
            },
            crossdomain: true,
            success: function (res) {
                _this.fillData(vue_users, res);
            }
        })

    },
    fillData: function (vue_users, res) {
        vue_users.users = res.data;
    }
};