using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using MVC_Learn.Models;

namespace MVC_Learn.SignalR
{
    public class BaseHub<T> : Hub<T> where T : class
    {
        protected readonly LearnDbContext Db = new LearnDbContext();
        public UserInfo UserInfo { get; set; }

        public override Task OnConnected()
        {
            var cookie = this.Context.Request.Cookies.FirstOrDefault(e => e.Key == "username").Value;
            if (!string.IsNullOrEmpty(cookie?.Value))
            {

            }
            return base.OnConnected();
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