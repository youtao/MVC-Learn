using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService AccountService;
        private readonly IPrivilegeService PrivilegeService;

        public AccountController(IAccountService accountService, IPrivilegeService privilegeService)
        {
            AccountService = accountService;
            this.PrivilegeService = privilegeService;
        }
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDTO login)
        {
            var user = await this.AccountService
                .LoginAsync(login.UserName, login.Password)
                .ConfigureAwait(true);
            if (user == null)
            {
                return Ok(ResponseUtils.Converter(new object(), 0, "登录失败"));
            }
            RedisAuthorize authorize = await this.PrivilegeService
                .UpdateAuthorizeAsync(user)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(authorize.AuthorizeId));
        }
    }

    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
