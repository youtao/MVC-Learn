using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Microsoft.AspNet.SignalR;
using Model;

namespace WebUI.SignalR
{
    public class BaseHub<T> : Hub<T> where T : class
    {
        protected readonly LearnDbContext Db = new LearnDbContext();
        public UserInfo UserInfo { get; set; }

        public override async Task OnConnected()
        {
            var uCookie = this.Context.Request.Cookies.FirstOrDefault(e => e.Key == "u").Value;
            var pCookie = this.Context.Request.Cookies.FirstOrDefault(e => e.Key == "p").Value;
            if (!string.IsNullOrEmpty(uCookie?.Value) && !string.IsNullOrEmpty(pCookie?.Value))
            {
                var user = Db.UserInfo.FirstOrDefault(e => e.UserName == uCookie.Value);
                if (user?.Password == pCookie.Value) // todo:暂时不加密密码
                {
                    var now = DateTime.Now;
                    Db.Connection.Add(new Connection() // 添加连接记录
                    {
                        ConnectionId = this.Context.ConnectionId,
                        UserAgent = this.Context.Headers["User-Agent"],
                        UserInfoId = user.Id,
                        CreateTime = now,
                    });
                    await Db.SaveChangesAsync();
                    if (user.SignoutTime != null) // 全部连接退出后第一次登录
                    {
                        //user.LoginTime = now;
                        //user.SignoutTime = null;
                        await Db.UserInfo.Where(e => e.Id == user.Id).UpdateAsync(e => new UserInfo()
                        {
                            LoginTime = now,
                            SignoutTime = null
                        });
                    }
                }
            }
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled) // 连接断开过快可能会导致OnConnected先执行完
        {
            // todo:时间周期过长,判断用户是否还连接
            var connection = this.Db.Connection.FirstOrDefault(e => e.ConnectionId == this.Context.ConnectionId);
            if (connection != null)
            {
                Db.Connection.Where(e => e.ConnectionId == this.Context.ConnectionId)
                      .Update(e => new Connection()
                      {
                          Connected = false
                      });
                var count = Db.Connection.Count(e =>
                    e.UserInfoId == connection.UserInfoId &&
                    e.Connected == true &&
                    e.CreateTime >= connection.UserInfo.LoginTime);
                if (count <= 0)
                {
                    await this.Db.UserInfo.Where(e => e.Id == connection.UserInfoId).UpdateAsync(e => new UserInfo()
                    {
                        SignoutTime = DateTime.Now // 最后一个连接退出
                    });
                }
            }
            await base.OnDisconnected(stopCalled);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}