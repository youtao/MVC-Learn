using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MVC_Learn.SignalR
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            var connectionId = this.Context.ConnectionId;
            return base.OnConnected();
        }



        public override Task OnReconnected()
        {
            var connectionId = this.Context.ConnectionId;
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = this.Context.ConnectionId;
            return base.OnDisconnected(stopCalled);
        }
    }
}