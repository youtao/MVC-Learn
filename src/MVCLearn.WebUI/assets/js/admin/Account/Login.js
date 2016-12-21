$(function () {
    $('#login-button').click(function () {
        var param = $('form#login-form').serializeJson();
        $.post(GlobalConfig.WebApi + 'Account/Login', param, function (res) {
            if (res.state == 200) { // 登录成功
                Cookies.set('MVCLearn_AuthorizeId', res.data, {
                    domain: GlobalConfig.CookieDomain
                });
                window.location.href = '/admin/';
            }
        });
    });
});