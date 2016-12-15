using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MVCLearn.ModelDTO;
using MVCLearn.Service.Interface;

namespace MVCLearn.Service
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
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
    }
}