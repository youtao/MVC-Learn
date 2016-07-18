$(function () {
    $('#user-list').datagrid({
        url: '',
        method: 'GET',
        striped: true,
        loadMsg: '拼命加载中...',
        rownumbers: true,
        pagination: true,
        singleSelect: true,
        checkOnSelect: false,
        selectOnCheck: false,
        columns: [
            [
                { field: 'Unique', checkbox: true },
                {
                    field: 'UserName', title: '用户名', width: '20%', align: 'center', formatter: function (value, row, index) {
                        return `${value}-${row.Unique}`;
                    }
                },
                { field: 'NickName', title: '昵称', width: '20%', align: 'center' },
                { field: 'CreateTime', title: '注册日期', width: '20%', align: 'center' },
                { field: 'Age', title: '年龄', width: '20%', align: 'center' },
                {
                    field: 'Online', title: '是否在线', width: '10%', align: 'center', styler: function (value, row, index) {
                        var color = 'red';
                        if (value === '是') {
                            color = 'green';
                        }
                        return 'opacity:0.7;background-color:' + color;
                    }
                }
            ]
        ],

        data: [
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
        ],
        toolbar: '#user-list-toolbar',

        onClickRow: function (index, data) {
            $('#user-edit')
                .propertygrid({
                    showGroup: true
                });
            $('#user-edit').propertygrid('loadData', { total: 0, rows: [] });
            for (var key in data) {
                if (data.hasOwnProperty(key)) {
                    var row = {
                        name: key,
                        value: data[key],
                        group: '账号设置',
                        editor: 'text'
                    }
                    $('#user-edit').propertygrid('appendRow', row);
                };
                
            }
        }
    });

    $('#add-user').click(function () {
        $.messager.show({
            title: '添加用户',
            msg: '此功能稍后开放',
            timeout: 5000,
            showType: 'slide'
        });
    });
    $('#delete-user').click(function () {
        var list = [];
        $('#user-list').datagrid('getChecked').forEach(function (item) {
            list.push(item.Unique);
        });
        console.log(list);
    });


});