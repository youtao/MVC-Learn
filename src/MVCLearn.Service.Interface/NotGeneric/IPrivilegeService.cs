using System.Collections.Generic;
using System.Threading.Tasks;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;

namespace MVCLearn.Service.Interface
{
    /// <summary>
    /// 权限 Service Interface
    /// </summary>
    public interface IPrivilegeService
    {
        #region 按钮权限

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

        #endregion

        #region 菜单权限

        Task<List<MenuInfoDTO>> GetMenuByUserIDAsync(int userID);

        #endregion

        #region 访问权限

        Task<List<AccessInfoDTO>> GetAccessByUserIDAsync(int userID);

        #endregion

        #region 用户权限

        Task<PrivilegeDTO> GetPrivilegeAsync(int userID);

        #endregion
    }
}