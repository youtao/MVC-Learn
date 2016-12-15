﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service.Interface
{
    /// <summary>
    /// 权限 Service Interface
    /// </summary>
    public interface IPrivilegeService
    {
        /// <summary>
        /// 根据用户ID获取按钮权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <returns>Task&lt;List&lt;ButtonInfoDTO&gt;&gt;.</returns>
        Task<List<ButtonInfoDTO>> GetButtonByUserIDAsync(int userID);
        /// <summary>
        /// 根据角色ID获取按钮权限.
        /// </summary>
        /// <param name="roleID">角色ID.</param>
        /// <returns>Task&lt;List&lt;ButtonInfoDTO&gt;&gt;.</returns>
        Task<List<ButtonInfoDTO>> GetButtonByRoleIDAsync(int roleID);
    }
}