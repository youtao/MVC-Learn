$(function () {
    var config = {
        editingId: undefined
    }

    $('#menu-list').treegrid({
        url: '/admin/menu/getmenu',
        idField: 'Id',
        treeField: 'Title',
        method: 'GET',
        loadMsg: '拼命加载中...',
        rownumbers: true,
        animate: true,
        lines: true,
        columns: [[
            {
                field: 'Title', title: 'title', width: '30%', editor: 'text'
            },
            { field: 'Url', title: 'url', width: '40%', editor: 'text' },
            {
                field: 'Icon', title: 'icon', width: '20%', align: 'center', editor: 'text', formatter: function (node) {
                    var html = '<i class="' + node + '"></i>&nbsp;&nbsp;&nbsp;' + node;
                    return html;
                }
            }
        ]],
        toolbar: [
            {
                text: '添加',
                iconCls: 'fa fa-create',
                handler: function () {

                }
            }, '-', {
                text: '编辑',
                iconCls: 'fa fa-edit',
                handler: function () {
                    if (config.editingId != undefined) {
                        $('#menu-list').treegrid('select', config.editingId);
                        return;
                    }
                    var row = $('#menu-list').treegrid('getSelected');
                    if (row) {
                        config.editingId = row.Id
                        $('#menu-list').treegrid('beginEdit', config.editingId);
                    }
                }
            }, '-', {
                text: '保存',
                iconCls: 'fa fa-save',
                handler: function () {
                    if (config.editingId != undefined) {
                        var t = $('#tg');
                        $('#menu-list').treegrid('endEdit', config.editingId);
                        config.editingId = undefined;
                    }
                }
            }, '-', {
                text: '取消',
                iconCls: 'fa fa-user',
                handler: function () {
                    if (config.editingId != undefined) {
                        $('#menu-list').treegrid('cancelEdit', config.editingId);
                        config.editingId = undefined;
                    }


                }
            }],
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
        },
        onAfterEdit: function (node) { // TODO:判断是否更新或添加成功
            $.post('/admin/menu/edit', {
                Id: node.Id,
                Title: node.Title,
                Url: node.Url,
                Icon: node.Icon,
            }, function (response) {
                console.log(response);
            });
        }
    });
});