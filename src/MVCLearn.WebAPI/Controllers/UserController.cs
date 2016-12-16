using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MVCLearn.ModelDTO;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebAPI.Controllers
{
    /// <summary>
    /// 用户.
    /// </summary>
    public class UserController : ApiController
    {
        public UserController(IUserInfoService userInfoService)
        {
            UserInfoService = userInfoService;
        }

        private readonly IUserInfoService UserInfoService;

        /// <summary>
        ///获取全部用户.
        /// </summary>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        public async Task<IHttpActionResult> GetAllUsers()
        {
            var data = await this.UserInfoService
                .GetAllUserWidthDapperAsync()
                .ConfigureAwait(true);
            var session = HttpContext.Current.Request.Cookies["session"];
            return this.Ok(ResponseUtils.Converter(data));
        }
    }
}
