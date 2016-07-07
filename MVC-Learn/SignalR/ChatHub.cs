using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MVC_Learn.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public void Hello()
        {
            Clients.Others.Hello(this.Context.ConnectionId);
        }
    }

    public interface IChatHub
    {
        /// <summary>
        /// 上线通知
        /// </summary>
        void Hello(string connectionId);
    }
}