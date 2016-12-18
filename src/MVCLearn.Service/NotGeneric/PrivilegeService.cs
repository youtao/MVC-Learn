using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using MVCLearn.Service.Interface;

namespace MVCLearn.Service
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        #region 按钮权限

        /// <summary>
        /// 根据用户ID获取按钮权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <returns>Task&lt;List&lt;ButtonInfoDTO&gt;&gt;.</returns>
        public async Task<List<ButtonInfoDTO>> GetButtonByUserIDAsync(int userID)
        {
            #region sql

            var sql = @"select
	                        distinct
                            button.ID as ButtonID,
	                        button.ButtonName,
                            button.ButtonType
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_ButtonInfo as rolebutton on rolebutton.RoleInfo_ID = roleinfo.ID
                            join dbo.System_ButtonInfo as button on button.ID = rolebutton.ButtonInfo_ID
                        where
                            userinfo.ID = @UserID and
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            button.[Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                var list = await conn.QueryAsync<ButtonInfoDTO>(sql, new { UserID = userID })
                    .ConfigureAwait(false);
                conn.Close();
                return list.ToList();
            }
        }

        /// <summary>
        /// 根据角色ID获取按钮权限.
        /// </summary>
        /// <param name="roleID">角色ID.</param>
        /// <returns>Task&lt;List&lt;ButtonInfoDTO&gt;&gt;.</returns>
        public async Task<List<ButtonInfoDTO>> GetButtonByRoleIDAsync(int roleID)
        {
            #region sql

            var sql = @"select
	                        distinct
                            button.ID as ButtonID,
                            button.ButtonName,
                            button.ButtonType
                        from
                            dbo.System_RoleInfo as roleinfo
                            join dbo.Privilege_MT_RoleInfo_ButtonInfo as rolebutton on rolebutton.RoleInfo_ID = roleinfo.ID
                            join dbo.System_ButtonInfo as button on button.ID = rolebutton.ButtonInfo_ID
                        where
                            roleinfo.ID = @RoleID and
                            roleinfo.[Delete] = 0 and
                            button.[Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                var list = await conn.QueryAsync<ButtonInfoDTO>(sql, new { RoleID = roleID })
                    .ConfigureAwait(false);
                conn.Close();
                return list.ToList();
            }
        }

        #endregion

        #region 菜单权限

        public async Task<List<MenuInfoDTO>> GetMenuByUserIDAsync(int userID)
        {
            #region sql

            var sql = @"select
                            menuinfo.ID as MenuID,
                            menuinfo.Title,
                            menuinfo.Url,
                            menuinfo.Icon,
                            menuinfo.[Order],
                            menuinfo.ParentID,
                            menuinfo.IsIframe
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_MenuInfo rolemenu on rolemenu.RoleInfo_ID = roleinfo.ID
                            join dbo.System_MenuInfo menuinfo on menuinfo.ID = rolemenu.MenuInfo_ID
                        where
                            userinfo.ID = @UserID and
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            menuinfo.[Delete] = 0 and
                            menuinfo.IsMenu = 1
                        union
                        select
                            ID as MenuID,
                            Title,
                            Url,
                            Icon,
                            [Order],
                            ParentID,
                            IsIframe
                        from
                            dbo.System_MenuInfo
                        where
                            IsPublick = 1 and
                            IsMenu = 1 and
                            [Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                var list = await conn.QueryAsync<MenuInfoDTO>(sql, new { UserID = userID })
                    .ConfigureAwait(false);
                conn.Close();
                return list.ToList();
            }
        }

        #endregion

        #region 访问权限

        public async Task<List<AccessInfoDTO>> GetAccessByUserIDAsync(int userID)
        {
            #region sql

            var sql = @"select
                            menuinfo.ID as MenuID,
                            menuinfo.Title,
                            menuinfo.Url
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_MenuInfo rolemenu on rolemenu.RoleInfo_ID = roleinfo.ID
                            join dbo.System_MenuInfo menuinfo on menuinfo.ID = rolemenu.MenuInfo_ID
                        where
                            userinfo.ID = @UserID and --用户ID
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            menuinfo.[Delete] = 0
                        union
                        select
                            ID as MenuID,
                            Title,
                            Url
                        from
                            dbo.System_MenuInfo
                        where
                            IsPublick = 1 and
                            [Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                var list = await conn.QueryAsync<AccessInfoDTO>(sql, new { UserID = userID })
                    .ConfigureAwait(false);
                conn.Close();
                return list.ToList();
            }
        }

        #endregion

        #region mongodb

        /// <summary>
        /// 更新用户权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <param name="privilege">用户权限.</param>
        public void UpdatePrivilegeToMongo(int userID, PrivilegeDTO privilege)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>
                .Set(e => e.Accesses, privilege.Accesses)
                .Set(e => e.Buttons, privilege.Buttons)
                .Set(e => e.Menus, privilege.Menus),
                UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户访问权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <param name="access">权限列表.</param>
        public void UpdateAccessToMongo(int userID, List<AccessInfoDTO> access)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Accesses, access), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户菜单权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <param name="menus">菜单列表.</param>
        public void UpdateMenuToMongo(int userID, List<MenuInfoDTO> menus)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Menus, menus), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户按钮权限.
        /// </summary>
        /// <param name="userID">用户ID.</param>
        /// <param name="buttons">按钮列表.</param>
        public void UpdateButtonToMongo(int userID, List<ButtonInfoDTO> buttons)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Buttons, buttons), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 获取用户权限.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>PrivilegeDTO.</returns>
        public async Task<PrivilegeDTO> GetPrivilegeAsync(int userID)
        {
            //todo:如果权限相关的表改变了,则删除所有mongo缓存
            //todo:如果只改变了某一个用户的权限,只删除该用户的mongodb
            //todo:没有权限的用户
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            var privilege = collection.FindOneAs<PrivilegeDTO>(Query<PrivilegeDTO>.EQ(e => e.UserID, userID));
            if (privilege == null)
            {
                privilege = new PrivilegeDTO();
                var buttonTask = Task.Run(async () =>
                {
                    privilege.Buttons = await this.GetButtonByUserIDAsync(userID);
                });
                var menuTask = Task.Run(async () =>
                {
                    privilege.Menus = await this.GetMenuByUserIDAsync(userID);
                });
                var accessTask = Task.Run(async () =>
                {
                    privilege.Accesses = await this.GetAccessByUserIDAsync(userID);
                });
                await Task.WhenAll(buttonTask, menuTask, accessTask);
                this.UpdatePrivilegeToMongo(userID, privilege);
            }
            return privilege;
        }

        #endregion

    }
}