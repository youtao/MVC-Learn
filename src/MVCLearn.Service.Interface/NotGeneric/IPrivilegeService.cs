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
        #region 访问权限

        #region async

        /// <summary>
        /// 获取访问权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        Task<List<AccessInfoDTO>> GetAccessByUserIDAsync(int userID);

        #endregion

        #region sync

        /// <summary>
        /// 获取访问权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        List<AccessInfoDTO> GetAccessByUserID(int userID);

        #endregion

        #endregion

        #region 菜单权限

        #region async

        /// <summary>
        /// 获取菜单权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        Task<List<MenuInfoDTO>> GetMenuByUserIDAsync(int userID);

        #endregion

        #region sync

        /// <summary>
        /// 获取菜单(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        List<MenuInfoDTO> GetMenuByUserID(int userID);

        #endregion

        #endregion

        #region 按钮权限

        #region async

        /// <summary>
        /// 获取按钮权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID.</param>
        Task<List<ButtonInfoDTO>> GetButtonByUserIDAsync(int userID);
        /// <summary>
        /// 获取按钮权限(根据角色ID)
        /// </summary>
        /// <param name="roleID">角色ID</param>
        Task<List<ButtonInfoDTO>> GetButtonByRoleIDAsync(int roleID);

        #endregion

        #region sync

        /// <summary>
        /// 获取按钮权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID.</param>
        List<ButtonInfoDTO> GetButtonByUserID(int userID);
        /// <summary>
        /// 获取按钮权限(根据角色ID)
        /// </summary>
        /// <param name="roleID">角色ID</param>
        List<ButtonInfoDTO> GetButtonByRoleID(int roleID);

        #endregion

        #endregion

        #region 用户权限

        #region async

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        Task<PrivilegeDTO> GetPrivilegeAsync(int userID);

        #endregion

        #region sync

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        PrivilegeDTO GetPrivilege(int userID);

        #endregion

        #endregion

        #region redis

        #region async

        /// <summary>
        /// 更新用户授权信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <exception cref="System.Exception">redis更新失败</exception>
        Task<RedisAuthorize> UpdateAuthorizeAsync(UserInfoDTO user);

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        Task<RedisAuthorize> GetAuthorizeAsync(string authorizeId);

        /// <summary>
        /// 删除用户授权信息.
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        Task<bool> DeleteAuthorizeAsync(string authorizeId);

        #endregion

        #region sync

        /// <summary>
        /// 更新用户授权信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <exception cref="System.Exception">redis更新失败</exception>
        RedisAuthorize UpdateAuthorize(UserInfoDTO user);

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        RedisAuthorize GetAuthorize(string authorizeId);

        /// <summary>
        /// 删除用户授权信息.
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        bool DeleteAuthorize(string authorizeId);

        #endregion

        #endregion
    }
}