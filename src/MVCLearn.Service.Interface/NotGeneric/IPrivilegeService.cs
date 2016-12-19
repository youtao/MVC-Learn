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

        #region redis

        /// <summary>
        /// 更新用户授权信息.
        /// </summary>
        /// <param name="user">用户.</param>
        /// <returns>Task&lt;RedisAuthorize&gt;.</returns>
        /// <exception cref="System.Exception">redis更新失败</exception>
       Task<RedisAuthorize> UpdateAuthorizeAsync(UserInfoDTO user);

        /// <summary>
        /// 获取用户授权信息.
        /// </summary>
        /// <param name="authorizeId">授权Id.</param>
        /// <returns>Task&lt;RedisAuthorize&gt;.</returns>
        Task<RedisAuthorize> GetAuthorizeAsync(string authorizeId);

        /// <summary>
        /// 删除用户授权信息.
        /// </summary>
        /// <param name="authorizeId">授权Id.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> DeleteAuthorizeAsync(string authorizeId);

        #endregion
    }
}