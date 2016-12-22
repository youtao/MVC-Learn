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
    /// 用户Service
    /// </summary>
    public partial class UserInfoService
    {
        /// <summary>
        /// 获取全部用户(EF) //todo:如果用户过多弃用此方法
        /// </summary>
        public async Task<List<UserInfoDTO>> GetAllUserAsync()
        {
            var dtoList = await this.AllNotDelete()
                .OrderByDescending(e=>e.LoginTime)
                .ProjectTo<UserInfoDTO>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return dtoList;
        }
    }
}