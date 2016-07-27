$(function () {
    var config = {
        editing: false, // 是否编辑中
        editingId: undefined // 编辑Id        
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
        checkOnSelect: false,
        selectOnCheck: false,
        columns: [[
            { field: 'Unique', checkbox: true },
            {
                field: 'Title', title: '标题', width: '30%', editor: 'text'
            },
            { field: 'Url', title: '地址', width: '40%', editor: 'text' },
            {
                field: 'Icon', title: '图标', width: '20%', align: 'center', editor: 'text', formatter: function (node) {
                    var html = '<i class="' + node + '"></i>&nbsp;&nbsp;&nbsp;' + node;
                    return html;
                }
            }
        ]],
        toolbar: [
            {
                text: '添加',
                iconCls: 'fa fa-plus',
                handler: function () {
                    if (config.editing) {
                        if (config.editingId != undefined) { // 编辑中
                            $('#menu-list').treegrid('select', config.editingId);
                        } else { // 添加中
                            $('#menu-list').treegrid('select', -1);
                        }
                        return;
                    }
                    config.editing = true;

                    var parent = null;
                    var node = $('#menu-list').treegrid('getSelected');
                    if (node) {
                        parent = node.Id;
                    }

                    $('#menu-list').treegrid('append', {
                        parent: parent,
                        data: [
                        {
                            Id: -1,
                            Title: '',
                            Url: '',
                            Icon: '',
                            ParentId: parent
                        }]
                    });
                    $('#menu-list').treegrid('beginEdit', -1);
                }
            }, '-', {
                text: '编辑',
                iconCls: 'fa fa-edit',
                handler: function () {
                    if (config.editing) {
                        if (config.editingId != undefined) { //编辑中
                            $('#menu-list').treegrid('select', config.editingId);
                        } else { // 添加中
                            $('#menu-list').treegrid('select', -1);
                        }
                        return;
                    }
                    var row = $('#menu-list').treegrid('getSelected');
                    if (row) {
                        config.editing = true;
                        config.editingId = row.Id;
                        $('#menu-list').treegrid('beginEdit', config.editingId);
                    }
                }
            }, '-', {
                text: '保存',
                iconCls: 'fa fa-save',
                handler: function () {
                    if (config) {
                        if (config.editingId != undefined) {
                            $('#menu-list').treegrid('endEdit', config.editingId);
                        } else {
                            $('#menu-list').treegrid('endEdit', -1);
                        }
                        config.editingId = undefined;
                        config.editing = false;
                    }
                }
            }, '-', {
                text: '取消',
                iconCls: 'fa fa-undo',
                handler: function () {
                    $('#menu-list').treegrid('unselectAll');
                    if (config.editing) {
                        if (config.editingId != undefined) { // 修改中
                            $('#menu-list').treegrid('cancelEdit', config.editingId);
                        } else { //添加中                            
                            $('#menu-list').treegrid('remove', -1);
                        }
                        config.editingId = undefined;
                        config.editing = false;
                    }

                }
            }],
        loadFilter: function (data, parent) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Count) {
                    data[i].state = 'closed';
                }
            }
            return data;
        },
        onBeforeExpand: function (node) {
            $('#menu-list').treegrid('options').url = "/admin/menu/getmenu?ParentId=" + node.Id;
        },
        onAfterEdit: function (node) { // TODO:判断是否更新或添加成功
            var url = '/admin/menu/edit';
            var data = {
                Id: node.Id,
                Title: node.Title,
                Url: node.Url,
                Icon: node.Icon
            }
            if (node.Id < 0) {
                data.ParentId = node.ParentId;
                url = '/admin/menu/create';
            }
            $.post(url, data, function (response) {
                if (node.Id < 0) {
                    if (node.ParentId) {
                        $('#menu-list').treegrid('reload', node.ParentId);
                    } else {
                        $('#menu-list').treegrid('unselectAll'); // 如果是全部从新加载则一定要移除之前所有的选择
                        $('#menu-list').treegrid('reload');
                    }
                }
            });
        }
    });

    $('#menu-order').tree({
        url: '/admin/menu/getmenu',
        dnd: true,
        loadFilter: function (data, parent) {
            for (var i = 0; i < data.length; i++) {
                data[i].id = data[i].Id;
                data[i].text = data[i].Title;
                if (data[i].Count) {
                    data[i].state = 'closed';
                }
            }
            return data;
        },
        onBeforeExpand: function (node) {
            $('#menu-order').tree('options').url = "/admin/menu/getmenu?ParentId=" + node.Id;
        },
        onDrop: function (target, source, point) {
            var menuOrder = 0;
            var parent = '';
            if (point === 'append') {
                parent = $('#menu-order').tree('getNode', target);
                if (parent) {
                    menuOrder = parent.children.length - 1;
                }
            } else {
                parent = $('#menu-order').tree('getParent', target);
                if (point === 'top') {
                    menuOrder = $('#menu-order').tree('getNode', target).MenuOrder;
                    if (menuOrder !== 0) {
                        menuOrder--;
                    }

                } else {
                    menuOrder = $('#menu-order').tree('getNode', target).MenuOrder + 1;
                }
            }
            var parentId = null;
            if (parent) {
                parentId = parent.Id;
            }

            $.post('/admin/menu/Order',
                {
                    Id: source.Id,
                    ParentId: parentId,
                    MenuOrder: menuOrder
                },
                function (response) {

                });
        }
    });
});