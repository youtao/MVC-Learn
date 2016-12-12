using System.Collections.Generic;
using System.Threading.Tasks;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service.Interface
{
    public partial interface IUserInfoService
    {
        /// <summary>
        /// 全部用户
        /// </summary>
        /// <returns></returns>
        Task<List<UserInfoDTO>> AllUserAsync();

        /// <summary>
        /// 全部用户(使用Dapper)
        /// </summary>
        /// <returns></returns>
        Task<List<UserInfoDTO>> AllUserWidthDapperAsync();
    }
}