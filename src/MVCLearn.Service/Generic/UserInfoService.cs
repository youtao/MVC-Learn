using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Dapper;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service
{
    /// <summary>
    /// 用户Service.
    /// </summary>
    public partial class UserInfoService
    {
        /// <summary>
        /// 获取全部用户(EF).
        /// </summary>
        /// <returns>Task&lt;List&lt;UserInfoDTO&gt;&gt;.</returns>
        public async Task<List<UserInfoDTO>> GetAllUserAsync()
        {
            var dtoList = await this.AllNotDelete()
                .ProjectTo<UserInfoDTO>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return dtoList;
        }
        /// <summary>
        /// 获取全部用户(Dapper).
        /// </summary>
        /// <returns>Task&lt;List&lt;UserInfoDTO&gt;&gt;.</returns>
        public async Task<List<UserInfoDTO>> GetAllUserWidthDapperAsync()
        {
            using (var conn = this.GetLearnDBConn())
            {
                var sql = "select ID as UserID,UserName,NickName,LoginTime from System_UserInfo;";
                await conn.OpenAsync().ConfigureAwait(false);
                var results = await conn.QueryAsync<UserInfoDTO>(sql)
                    .ConfigureAwait(false);
                return results.ToList();
            }
        }

    }
}