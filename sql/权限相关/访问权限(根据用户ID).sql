--全部访问权限
select
    menuinfo.ID as MenuID,
    menuinfo.Title,
    menuinfo.Url
from
    dbo.System_UserInfo as userinfo
    join dbo.Privilege_MT_UserInfo_RoleInfo userrole on userrole.UserInfo_ID = userinfo.ID
    join dbo.System_RoleInfo roleinfo on roleinfo.ID = userrole.RoleInfo_ID
    join dbo.Privilege_MT_RoleInfo_MenuInfo rolemenu on rolemenu.RoleInfo_ID = roleinfo.ID
    join dbo.System_MenuInfo menuinfo on menuinfo.ID = rolemenu.MenuInfo_ID
where
    userinfo.ID = 1 and --用户ID
    userinfo.[Delete] = 0 and
    roleinfo.[Delete] = 0 and
    menuinfo.[Delete] = 0
union
select
    ID as MenuID,
    Title,
    Url
from
    dbo.System_MenuInfo
where
    IsPublick = 1 and
    [Delete] = 0;