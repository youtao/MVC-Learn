using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MVCLearn.ModelDTO;
using MVCLearn.Service.Interface;
using MVCLearn.WebAPI.Session;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MVCLearn.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        public readonly IAccountService AccuntService;

        public AccountController(IAccountService accuntService)
        {
            AccuntService = accuntService;
        }
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDTO login)
        {
            var user = await this.AccuntService.LoginAsync(login.UserName, login.Password);
            if (user == null)
            {
                return Ok(ResponseUtils.Converter(new object(), 0, "登录失败"));
            }

            // todo:放到其他地方(每个接口都要认证权限)
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("localhost");
            var db = conn.GetDatabase();
            RedisAuthorize authorize = new RedisAuthorize(user);
            var json = JsonConvert.SerializeObject(authorize);
            await db.HashSetAsync("MVCLearn_AuthorizeId", authorize.AuthorizeId, json);
            return Ok(ResponseUtils.Converter(authorize.AuthorizeId));
        }
    }

    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
