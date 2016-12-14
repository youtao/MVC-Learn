using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace MVCLearn.WebSignalR.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public override Task OnConnected()
        {
            this.Clients.Others.Start(this.Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            this.Clients.Others.Leave(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public void Start()
        {
            this.Clients.Others.Start(this.Context.ConnectionId);
        }

        public void SendMessages(string messages)
        {
            this.Clients.Others.SendMessages(this.Context.ConnectionId, messages);
        }
    }

    /// <summary>
    /// ChatHub客户端接口
    /// </summary>
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

        void SendMessages(string connectionId, string messages);
    }
}