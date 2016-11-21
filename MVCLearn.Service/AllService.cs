using System.Web;
using MVCLearn.Model;
using MVCLearn.ModelDbContext;
using MVCLearn.Service.Interface;

namespace MVCLearn.Service
{
	public partial class MenuService : BaseService<LearnDbContext,Menu>, IMenuService
	{
		public MenuService() { }
		public MenuService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class ConnectionService : BaseService<LearnDbContext,Connection>, IConnectionService
	{
		public ConnectionService() { }
		public ConnectionService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class GroupService : BaseService<LearnDbContext,Group>, IGroupService
	{
		public GroupService() { }
		public GroupService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class UserInfoService : BaseService<LearnDbContext,UserInfo>, IUserInfoService
	{
		public UserInfoService() { }
		public UserInfoService(HttpContextBase httpContext) : base(httpContext) { }
	}
}