using System.Collections.Generic;
using System.Threading.Tasks;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service.Interface
{
    /// <summary>
    /// 用户Service Interface
    /// </summary>
    public partial interface IUserInfoService
    {
        /// <summary>
        /// 获取全部用户(EF)
        /// </summary>
        Task<List<UserInfoDTO>> GetAllUserAsync();

        /// <summary>
        /// 获取全部用户(Dapper)
        /// </summary>
        Task<List<UserInfoDTO>> GetAllUserWidthDapperAsync();
    }
}