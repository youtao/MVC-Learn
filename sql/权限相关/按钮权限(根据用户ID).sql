--根据用户ID获取按钮权限
select   
	distinct 
	button.ID as ButtonID,
	button.ButtonName,
    button.ButtonType
from
    dbo.System_UserInfo as userinfo
    join dbo.Privilege_MT_UserInfo_RoleInfo as userrole on userrole.UserInfo_ID = userinfo.ID
    join dbo.System_RoleInfo as roleinfo on roleinfo.ID = userrole.RoleInfo_ID
    join dbo.Privilege_MT_RoleInfo_ButtonInfo as rolebutton on rolebutton.RoleInfo_ID = roleinfo.ID
    join dbo.System_ButtonInfo as button on button.ID = rolebutton.ButtonInfo_ID
where
    userinfo.ID = 1 and
    userinfo.[Delete] = 0 and
    roleinfo.[Delete] = 0 and
    button.[Delete] = 0;