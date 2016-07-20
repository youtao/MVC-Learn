using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MVC_Learn.SignalR
{
    public class ChatHub : BaseHub<IChatHub>
    {
        public void Start()
        {
            this.Clients.Others.Start(this.Context.ConnectionId);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            this.Clients.Others.Leave(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

    }

    public interface IChatHub
    {
        /// <summary>
        /// 连接
        /// </summary>
        void Start(string connectionId);

        /// <summary>
        /// 离开
        /// </summary>
        /// <param name="connectionId"></param>
        void Leave(string connectionId);
    }
}