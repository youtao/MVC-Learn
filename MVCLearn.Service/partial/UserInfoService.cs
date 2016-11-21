using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service
{
    public partial class UserInfoService
    {
        public async Task<List<UserInfoDto>> AllUserAsync()
        {
            var dtoList = await this.AllNotDelete()
                .ProjectTo<UserInfoDto>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return dtoList;
        }
    }
}