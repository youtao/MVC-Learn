using System.Web;
using MVCLearn.Model;
using MVCLearn.ModelDbContext;
using MVCLearn.Service.Interface;

namespace MVCLearn.Service
{
	public partial class ButtonInfoService : BaseService<LearnDbContext,ButtonInfo>, IButtonInfoService
	{
		public ButtonInfoService() { }
		public ButtonInfoService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class AccessInfoService : BaseService<LearnDbContext,AccessInfo>, IAccessInfoService
	{
		public AccessInfoService() { }
		public AccessInfoService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class MenuInfoService : BaseService<LearnDbContext,MenuInfo>, IMenuInfoService
	{
		public MenuInfoService() { }
		public MenuInfoService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class RoleInfoService : BaseService<LearnDbContext,RoleInfo>, IRoleInfoService
	{
		public RoleInfoService() { }
		public RoleInfoService(HttpContextBase httpContext) : base(httpContext) { }
	}
	public partial class UserInfoService : BaseService<LearnDbContext,UserInfo>, IUserInfoService
	{
		public UserInfoService() { }
		public UserInfoService(HttpContextBase httpContext) : base(httpContext) { }
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
}