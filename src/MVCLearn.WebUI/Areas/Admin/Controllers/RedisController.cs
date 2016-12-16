using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class RedisController : Controller
    {
        // GET: Admin/Redis
        public ActionResult Index()
        {
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("localhost");
            var db = conn.GetDatabase();
            var session = new RedisSession<int>(123456789);
            var json = JsonConvert.SerializeObject(session);
            db.HashSetAsync("MVCLearn_Session", session.SessionID.ToString(), json);

            HttpCookie cookie = new HttpCookie("session", session.SessionID.ToString());
            cookie.Path = "/";
            cookie.Domain = "localhost";
            cookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cookie);

            return View();
        }
    }

    public class RedisSession<T>
    {
        public RedisSession(T value)
        {
            this.Value = value;
            this.SessionID = Guid.NewGuid();
        }

        public Guid SessionID { get; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 过期时间(默认7天).
        /// </summary>
        /// <value>The expiry.</value>
        public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(7);
        public T Value { get; set; }
    }

}