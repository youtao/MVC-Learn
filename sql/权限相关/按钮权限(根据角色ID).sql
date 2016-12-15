--根据角色ID获取按钮权限
select   
	distinct
    button.ID as ButtonID,
    button.ButtonName,
    button.ButtonType
from
    dbo.System_RoleInfo as roleinfo
    join dbo.Privilege_MT_RoleInfo_ButtonInfo as rolebutton on rolebutton.RoleInfo_ID = roleinfo.ID
    join dbo.System_ButtonInfo as button on button.ID = rolebutton.ButtonInfo_ID
where
    roleinfo.ID = 1 and
    roleinfo.[Delete] = 0 and
    button.[Delete] = 0;