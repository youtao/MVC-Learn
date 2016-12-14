select
    menuinfo.ID as MenuID,
    menuinfo.Title,
    menuinfo.Url,
    menuinfo.Icon,
    menuinfo.[Order],
    menuinfo.ParentID,    
    menuinfo.IsIframe    
from
    dbo.System_UserInfo as userinfo
    join dbo.Privilege_MT_UserInfo_RoleInfo userrole on userrole.UserInfo_ID = userinfo.ID
    join dbo.System_RoleInfo roleinfo on roleinfo.ID = userrole.RoleInfo_ID
    join dbo.Privilege_MT_RoleInfo_MenuInfo rolemenu on rolemenu.RoleInfo_ID = roleinfo.ID
    join dbo.System_MenuInfo menuinfo on menuinfo.ID = rolemenu.MenuInfo_ID
where
    userinfo.ID = 1 and
    userinfo.[Delete] = 0 and
    roleinfo.[Delete] = 0 and
    menuinfo.[Delete] = 0 and
    menuinfo.IsMenu = 1
union
select
    ID as MenuID,
    Title,
    Url,
    Icon,
    [Order],
    ParentID,
    IsIframe
from
    dbo.System_MenuInfo
where
    IsPublick = 1 and
    IsMenu = 1 and
    [Delete] = 0;
    