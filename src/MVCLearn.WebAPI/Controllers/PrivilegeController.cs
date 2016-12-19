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
    /// <summary>
    /// 权限
    /// </summary>
    public class PrivilegeController : ApiController
    {
        private readonly IPrivilegeService PrivilegeService;

        public PrivilegeController(IPrivilegeService privilegeService)
        {
            PrivilegeService = privilegeService;
        }

        #region 按钮权限

        /// <summary>
        /// 根据用户ID获取按钮权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        public async Task<IHttpActionResult> GetButtonByUserID(int userID)
        {
            var result = await this.PrivilegeService
                .GetButtonByUserIDAsync(userID)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(result));
        }

        /// <summary>
        /// 根据角色ID获取按钮权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        public async Task<IHttpActionResult> GetButtonByRoleID(int roleID)
        {
            var result = await this.PrivilegeService
                .GetButtonByRoleIDAsync(roleID)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(result));
        }

        #endregion

        #region 用户权限

        /// <summary>
        /// 获取权限根据用户ID
        /// </summary>
        /// <param name="userID">用户ID</param>
        public async Task<IHttpActionResult> GetPrivilege(int userID)
        {
            var result = await this.PrivilegeService
                .GetPrivilegeAsync(userID)
                .ConfigureAwait(true);
            return Ok(ResponseUtils.Converter(result));
        }

        #endregion
    }
}
