select
    accessinfo.ID as AccessID,
    accessinfo.Title,
    lower(accessinfo.Url) as Url -- 转换小写
from
    dbo.System_UserInfo as userinfo
    join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
    join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
    join dbo.Privilege_MT_RoleInfo_AccessInfo as roleaccess on roleaccess.RoleInfo_ID = roleinfo.ID
    join dbo.System_AccessInfo as accessinfo on accessinfo.ID = roleaccess.AccessInfo_ID
where
    userinfo.ID = 1 and--用户ID
    userinfo.[Delete] = 0 and
    roleinfo.[Delete] = 0 and
    accessinfo.[Delete] = 0
union
select
    ID as MenuID,
    Title,
    lower(Url) as Url -- 转换小写
from
    dbo.System_AccessInfo
where
    IsPublick = 1 and
    [Delete] = 0;