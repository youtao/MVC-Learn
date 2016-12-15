using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MVCLearn.ModelDTO;
using MVCLearn.Service.Interface;

namespace MVCLearn.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IUserInfoService userInfoService)
        {
            UserInfoService = userInfoService;
        }

        private readonly IUserInfoService UserInfoService;

        public async Task<IHttpActionResult> GetAllUsers()
        {
            var data = await this.UserInfoService
                .GetAllUserWidthDapperAsync()
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(data));
        }
    }
}
