using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
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
    }
}