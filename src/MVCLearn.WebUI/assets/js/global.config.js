//全局配置
var GlobalConfig = {
    //api接口
    WebApi: 'http://localhost:33826/api/',
    //SignalR
    SignalR: 'http://localhost:26949/signalr/',
    //SignalR-Hub
    SignalrHub: 'http://localhost:26949/signalr/hubs/',
    //默认iframe
    Iframe: '/admin/user/index',
    // cookie domain
    CookieDomain: 'localhost',
    // 按钮权限
    UserButtons: []
};
var UserButtons = $('#user-privilege-buttons').text().trim();
if (UserButtons.length > 0) {
    GlobalConfig.UserButtons = JSON.parse(UserButtons);
}