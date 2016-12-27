using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVCLearn.ModelDTO;
using MVCLearn.ModelDTO.Privilege;
using Newtonsoft.Json;
using MVCLearn.Service.Interface;
namespace MVCLearn.Service
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        #region constructor
        public PrivilegeService()
        {
        }

        public PrivilegeService(HttpContextBase httpContext) : base(httpContext)
        {
        }
        #endregion

        #region 访问权限

        #region async

        /// <summary>
        /// 获取访问权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public async Task<List<AccessInfoDTO>> GetAccessByUserIDAsync(int userID)
        {
            #region sql

            var sql = @"select
                            accessinfo.ID as AccessID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url -- 转换小写
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_AccessInfo as roleaccess on roleaccess.RoleInfo_ID = roleinfo.ID
                            join dbo.System_AccessInfo as accessinfo on accessinfo.ID = roleaccess.AccessInfo_ID
                        where
                            userinfo.ID = @UserID and--用户ID
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            accessinfo.[Delete] = 0
                        union
                        select
                            ID as MenuID,
                            Title,
                            lower(Url) as Url -- 转换小写
                        from
                            dbo.System_AccessInfo
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

        #region sync

        /// <summary>
        /// 获取访问权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<AccessInfoDTO> GetAccessByUserID(int userID)
        {
            #region sql

            var sql = @"select
                            accessinfo.ID as AccessID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url -- 转换小写
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_AccessInfo as roleaccess on roleaccess.RoleInfo_ID = roleinfo.ID
                            join dbo.System_AccessInfo as accessinfo on accessinfo.ID = roleaccess.AccessInfo_ID
                        where
                            userinfo.ID = @UserID and--用户ID
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            accessinfo.[Delete] = 0
                        union
                        select
                            ID as MenuID,
                            Title,
                            lower(Url) as Url -- 转换小写
                        from
                            dbo.System_AccessInfo
                        where
                            IsPublick = 1 and
                            [Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                conn.Open();
                var list = conn.Query<AccessInfoDTO>(sql, new { UserID = userID });
                conn.Close();
                return list.ToList();
            }
        }

        #endregion

        #endregion

        #region 菜单权限

        #region async

        /// <summary>
        /// 获取菜单权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public async Task<List<MenuInfoDTO>> GetMenuByUserIDAsync(int userID)
        {
            #region sql

            var sql = @"select
                            menuinfo.ID as MenuID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url, -- 转换小写
                            menuinfo.Icon,
                            menuinfo.[Order],
                            menuinfo.ParentID,
                            menuinfo.IsIframe
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_AccessInfo as roleaccess on roleaccess.RoleInfo_ID = roleinfo.ID
                            join dbo.System_AccessInfo as accessinfo on accessinfo.ID = roleaccess.AccessInfo_ID
                            join dbo.System_MenuInfo as menuinfo on menuinfo.ID = accessinfo.ID
                        where
                            userinfo.ID = @UserID and--用户ID
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            accessinfo.[Delete] = 0
                        union
                        select
                            menuinfo.ID as MenuID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url, -- 转换小写
                            menuinfo.Icon,
                            menuinfo.[Order],
                            menuinfo.ParentID,
                            menuinfo.IsIframe
                        from
                            dbo.System_AccessInfo as accessinfo
                            join dbo.System_MenuInfo as menuinfo on menuinfo.ID = accessinfo.ID
                        where
                            accessinfo.IsPublick = 1 and
                            accessinfo.[Delete] = 0;";

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

        /// <summary>
        /// 获取菜单树权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public async Task<List<MenuInfoDTO>> GetMenuTreeByUserIDAsync(int userID)
        {
            var allMenus = await this.GetMenuByUserIDAsync(userID).ConfigureAwait(false);
            var topMenus = allMenus.Where(e => e.ParentID == null).ToList();
            foreach (var top in topMenus)
            {
                RecursiveMenuTree(allMenus, top);
            }
            // Parallel.ForEach(topMenus, (top, state) => RecursiveMenuTree(topMenus, top)); //todo: 如果菜单过多,使用并行处理
            return topMenus;
        }

        #endregion

        #region sync

        /// <summary>
        /// 获取菜单权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<MenuInfoDTO> GetMenuByUserID(int userID)
        {
            #region sql

            var sql = @"select
                            menuinfo.ID as MenuID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url, -- 转换小写
                            menuinfo.Icon,
                            menuinfo.[Order],
                            menuinfo.ParentID,
                            menuinfo.IsIframe
                        from
                            dbo.System_UserInfo as userinfo
                            join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
                            join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
                            join dbo.Privilege_MT_RoleInfo_AccessInfo as roleaccess on roleaccess.RoleInfo_ID = roleinfo.ID
                            join dbo.System_AccessInfo as accessinfo on accessinfo.ID = roleaccess.AccessInfo_ID
                            join dbo.System_MenuInfo as menuinfo on menuinfo.ID = accessinfo.ID
                        where
                            userinfo.ID = @UserID and--用户ID
                            userinfo.[Delete] = 0 and
                            roleinfo.[Delete] = 0 and
                            accessinfo.[Delete] = 0
                        union
                        select
                            menuinfo.ID as MenuID,
                            accessinfo.Title,
                            lower(accessinfo.Url) as Url, -- 转换小写
                            menuinfo.Icon,
                            menuinfo.[Order],
                            menuinfo.ParentID,
                            menuinfo.IsIframe
                        from
                            dbo.System_AccessInfo as accessinfo
                            join dbo.System_MenuInfo as menuinfo on menuinfo.ID = accessinfo.ID
                        where
                            accessinfo.IsPublick = 1 and
                            accessinfo.[Delete] = 0;";

            #endregion
            using (var conn = this.GetLearnDBConn())
            {
                conn.Open();
                var list = conn.Query<MenuInfoDTO>(sql, new { UserID = userID });
                conn.Close();
                return list.ToList();
            }
        }

        /// <summary>
        /// 获取菜单树权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<MenuInfoDTO> GetMenuTreeByUserID(int userID)
        {
            var allMenus = this.GetMenuByUserID(userID);
            var topMenus = allMenus.Where(e => e.ParentID == null).ToList();
            foreach (var top in topMenus)
            {
                RecursiveMenuTree(allMenus, top);
            }
            // Parallel.ForEach(topMenus, (top, state) => RecursiveMenuTree(topMenus, top)); //todo: 如果菜单过多,使用并行处理
            return topMenus;
        }

        #endregion

        #endregion

        #region 按钮权限

        #region async

        /// <summary>
        /// 获取按钮权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
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
        /// 获取按钮权限(根据角色ID)
        /// </summary>
        /// <param name="roleID">角色ID</param>
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

        #region sync

        /// <summary>
        /// 获取按钮权限(根据用户ID)
        /// </summary>
        /// <param name="userID">用户ID</param>
        public List<ButtonInfoDTO> GetButtonByUserID(int userID)
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
                conn.Open();
                var list = conn.Query<ButtonInfoDTO>(sql, new { UserID = userID });
                conn.Close();
                return list.ToList();
            }
        }

        /// <summary>
        /// 获取按钮权限(根据角色ID)
        /// </summary>
        /// <param name="roleID">角色ID</param>
        public List<ButtonInfoDTO> GetButtonByRoleID(int roleID)
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
                conn.Open();
                var list = conn.Query<ButtonInfoDTO>(sql, new { RoleID = roleID });
                conn.Close();
                return list.ToList();
            }
        }

        #endregion

        #endregion

        #region mongodb

        #region async

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        public async Task<PrivilegeDTO> GetPrivilegeAsync(int userID)
        {
            //todo:如果权限相关的表改变了,则删除所有mongo缓存
            //todo:如果只改变了某一个用户的权限,只删除该用户的mongodb
            //todo:没有权限的用户
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            var privilege = collection.FindOneAs<PrivilegeDTO>(Query<PrivilegeDTO>.EQ(e => e.UserID, userID));
            if (privilege == null)
            {
                privilege = new PrivilegeDTO { UserID = userID };
                var accessTask = Task.Run(async () =>
                {
                    privilege.Accesses = await this.GetAccessByUserIDAsync(userID).ConfigureAwait(false);
                });
                var menuTask = Task.Run(async () =>
                {
                    privilege.Menus = await this.GetMenuTreeByUserIDAsync(userID).ConfigureAwait(false);
                });
                var buttonTask = Task.Run(async () =>
                {
                    privilege.Buttons = await this.GetButtonByUserIDAsync(userID).ConfigureAwait(false);
                });
                await Task.WhenAll(accessTask, menuTask, buttonTask).ConfigureAwait(false);
                this.UpdatePrivilegeToMongo(privilege);
            }
            return privilege;
        }

        #endregion

        #region sync

        /// <summary>
        /// 更新用户访问权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="access">权限列表</param>
        public void UpdateAccessToMongo(int userID, List<AccessInfoDTO> access)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Accesses, access), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户菜单权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="menus">菜单列表</param>
        public void UpdateMenuToMongo(int userID, List<MenuInfoDTO> menus)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Menus, menus), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户按钮权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="buttons">按钮列表</param>
        public void UpdateButtonToMongo(int userID, List<ButtonInfoDTO> buttons)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, userID),
                Update<PrivilegeDTO>.Set(e => e.Buttons, buttons), UpdateFlags.Upsert);
        }

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="privilege">用户权限</param>
        public void UpdatePrivilegeToMongo(PrivilegeDTO privilege)
        {
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            collection.Update(
                Query<PrivilegeDTO>.EQ(e => e.UserID, privilege.UserID),
                Update<PrivilegeDTO>
                .Set(e => e.Accesses, privilege.Accesses)
                .Set(e => e.Buttons, privilege.Buttons)
                .Set(e => e.Menus, privilege.Menus),
                UpdateFlags.Upsert);
        }


        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        public PrivilegeDTO GetPrivilege(int userID)
        {
            //todo:如果权限相关的表改变了,则删除所有mongo缓存
            //todo:如果只改变了某一个用户的权限,只删除该用户的mongodb
            //todo:没有权限的用户
            var collection = this.MongoDB.GetCollection<PrivilegeDTO>("privilege");
            var privilege = collection.FindOneAs<PrivilegeDTO>(Query<PrivilegeDTO>.EQ(e => e.UserID, userID));
            if (privilege == null)
            {
                privilege = new PrivilegeDTO();
                privilege.UserID = userID;
                privilege.Accesses = this.GetAccessByUserID(userID);
                privilege.Menus = this.GetMenuTreeByUserID(userID);
                privilege.Buttons = this.GetButtonByUserID(userID);
                this.UpdatePrivilegeToMongo(privilege);
            }
            return privilege;
        }

        #endregion

        #endregion

        #region redis

        #region async

        /// <summary>
        /// 更新用户授权信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <exception cref="System.Exception">redis更新失败</exception>
        public async Task<RedisAuthorize> UpdateAuthorizeAsync(UserInfoDTO user)
        {
            RedisAuthorize authorize = new RedisAuthorize(user);
            var json = JsonConvert.SerializeObject(authorize);
            await this.DeleteAuthorizeAsync(authorize.AuthorizeId).ConfigureAwait(false);
            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var result = await db.HashSetAsync("MVCLearn_AuthorizeId", authorize.AuthorizeId, json)
                .ConfigureAwait(false);
                await conn.CloseAsync().ConfigureAwait(false);
                if (result)
                {
                    return authorize;
                }
                throw new Exception("redis更新失败");
            }
        }

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        public async Task<RedisAuthorize> GetAuthorizeAsync(string authorizeId)
        {
            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var json = await db.HashGetAsync("MVCLearn_AuthorizeId", authorizeId)
                .ConfigureAwait(false);
                await conn.CloseAsync().ConfigureAwait(false);
                if (json.HasValue)
                {
                    var authorize = JsonConvert.DeserializeObject<RedisAuthorize>(json);
                    return authorize;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 删除用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        public async Task<bool> DeleteAuthorizeAsync(string authorizeId)
        {
            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var result = await db.HashDeleteAsync("MVCLearn_AuthorizeId", authorizeId)
                .ConfigureAwait(false);
                await conn.CloseAsync().ConfigureAwait(false);
                return result;
            }
        }

        #endregion

        #region sync

        /// <summary>
        /// 更新用户授权信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <exception cref="System.Exception">redis更新失败</exception>
        public RedisAuthorize UpdateAuthorize(UserInfoDTO user)
        {
            RedisAuthorize authorize = new RedisAuthorize(user);
            var json = JsonConvert.SerializeObject(authorize);
            this.DeleteAuthorize(authorize.AuthorizeId);

            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var result = db.HashSet("MVCLearn_AuthorizeId", authorize.AuthorizeId, json);
                conn.Close();
                if (result)
                {
                    return authorize;
                }
                throw new Exception("redis更新失败");
            }
        }

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        public RedisAuthorize GetAuthorize(string authorizeId)
        {
            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var json = db.HashGet("MVCLearn_AuthorizeId", authorizeId);
                conn.Close();
                if (json.HasValue)
                {
                    var authorize = JsonConvert.DeserializeObject<RedisAuthorize>(json);
                    return authorize;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 删除用户授权信息
        /// </summary>
        /// <param name="authorizeId">授权Id</param>
        public bool DeleteAuthorize(string authorizeId)
        {
            using (var conn = this.GetRedisConn())
            {
                var db = conn.GetDatabase();
                var result = db.HashDelete("MVCLearn_AuthorizeId", authorizeId);
                conn.Close();
                return result;
            }
        }

        #endregion

        #endregion

        #region private

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="source"></param>
        /// <param name="parent"></param>
        private void RecursiveMenuTree(List<MenuInfoDTO> source, MenuInfoDTO parent)
        {
            var children = source
                .Where(e => e.ParentID == parent.MenuID)
                .OrderBy(e => e.Order)
                .ToList();
            foreach (var child in children)
            {
                if (parent.Children == null)
                {
                    parent.Children = new List<MenuInfoDTO>();
                }
                parent.Children.Add(child);
                if (source.Any(e => e.ParentID == child.MenuID))
                {
                    RecursiveMenuTree(source, child);
                }
            }
        }

        #endregion
    }
}