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
    // 全部按钮权限
    buttons: [{
        name: '增加',
        type: 0
    }, {
        name: '删除',
        type: 1
    }, {
        name: '修改',
        type: 2
    }, {
        name: '查询',
        type: 3
    }, {
        name: '报表',
        type: 4
    }, {
        name: '打印',
        type: 5
    }, {
        name: '分享',
        type: 6
    }]
};