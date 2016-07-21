$(function () {
    $('#menu-list').treegrid({
        url: '/admin/menu/getmenu',
        idField: 'Id',
        treeField: 'Title',
        method: 'GET',        
        loadMsg: '拼命加载中...',
        rownumbers: true,
        animate: true,
        lines:true,
        columns: [[
            {
                field: 'Title', title: 'title', width: '30%'
            },
            { field: 'Url', title: 'url', width: '40%' },
            {
                field: 'Icon', title: 'icon', width: '20%', align:'center', formatter: function (node) {
                    var html = '<i class="' + node + '"></i>&nbsp;&nbsp;&nbsp;' + node;
                    return html;
                }
            }
        ]],
        loadFilter: function (data, parent) {            
            for (var i = 0; i < data.length; i++) {                
                if (data[i].HasChildren) {
                    data[i].state = 'closed';
                    data[i].children = [];
                }
            }            
            return data;
        },
        onBeforeExpand: function (node) {
            $('#menu-list').treegrid('options').url = "/admin/menu/getmenu?ParentId=" + node.Id;
        }
    });
});