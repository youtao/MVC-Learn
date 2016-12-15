using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Dapper;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service
{
    public partial class UserInfoService
    {
        public async Task<List<UserInfoDTO>> AllUserAsync()
        {
            var dtoList = await this.AllNotDelete()
                .ProjectTo<UserInfoDTO>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return dtoList;
        }

        public async Task<List<UserInfoDTO>> AllUserWidthDapperAsync()
        {
            using (var conn = this.LearnDBConn())
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