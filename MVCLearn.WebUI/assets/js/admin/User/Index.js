var currentpage = {
    init: function () {
        var v = new Vue({
            el: '#user-table',
            data: {
                users: []
            }
        });
        this.loadData(v);
    },
    loadData: function (v) {
        var _this = this;
        $.get(GlobalConfig.Server + 'User', null, function (res) {
            _this.fillData(v, res);
        });
    },
    fillData: function (v, data) {
        v.users = data.data;
    }
};
$(function () {
    currentpage.init();
});