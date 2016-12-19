using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MVCLearn.Model;
using MVCLearn.ModelDTO;
using MVCLearn.Service.Interface;
using MVCLearn.Utilities;

namespace MVCLearn.Service
{
    /// <summary>
    /// 账户相关 Service.
    /// </summary>
    public class AccountService : BaseService, IAccountService
    {
        public AccountService()
        {
        }

        public AccountService(HttpContextBase httpContext) : base(httpContext)
        {
        }

        public async Task<UserInfoDTO> LoginAsync(string username, string password)
        {
            var user = await this.GetUserByUserIDAsync(username)
                .ConfigureAwait(false);
            if (user == null) // 找不到该用户
            {
                return null;
            }
            if (user.Password != password.MD5()) // 密码错误 todo:密码错误次数限制
            {
                return null;
            }
            var result = Mapper.Map<UserInfoDTO>(user);
            return result;
        }

        /// <summary>
        /// 根据用户名获取用户.
        /// </summary>
        /// <param name="userName">用户名.</param>
        /// <returns>Task&lt;UserInfo&gt;.</returns>
        private async Task<UserInfo> GetUserByUserIDAsync(string userName)
        {
            var user = await this.LearnDB
                .UserInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserName == userName)
                .ConfigureAwait(false);
            return user;
        }
    }
}