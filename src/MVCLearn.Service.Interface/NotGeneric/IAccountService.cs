using System.Threading.Tasks;
using MVCLearn.ModelDTO;

namespace MVCLearn.Service.Interface
{
    /// <summary>
    /// 账户相关 Service Interface
    /// </summary>
    public interface IAccountService
    {
        Task<UserInfoDTO> LoginAsync(string username, string password);
    }
}