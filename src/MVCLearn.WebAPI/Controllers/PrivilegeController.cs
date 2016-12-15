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
    public class PrivilegeController : ApiController
    {
        private readonly IPrivilegeService PrivilegeService;

        public PrivilegeController(IPrivilegeService privilegeService)
        {
            PrivilegeService = privilegeService;
        }

        public async Task<IHttpActionResult> GetButtonByUserID(int userID)
        {
            var result = await this.PrivilegeService
                .GetButtonByUserIDAsync(userID)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(result));
        }

        public async Task<IHttpActionResult> GetButtonByRoleID(int roleID)
        {
            var result = await this.PrivilegeService
                .GetButtonByRoleIDAsync(roleID)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(result));
        }
    }
}
