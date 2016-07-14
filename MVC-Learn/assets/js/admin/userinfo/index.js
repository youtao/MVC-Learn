$(function () {
    //$('#main-layout').layout();
    //$('#main-layout').layout('add',
    //{
    //    region: 'north',
    //    height: 100        
    //});
    //$('#main-layout').layout('add',
    //{
    //    region: 'south',
    //    height: 200
    //});
    //$('#main-layout').layout('add',
    //{
    //    region: 'center'
    //});

    //$('#main-layout').layout('add',
    //{
    //    region: 'east',
    //    height: 100,
    //    width:300
    //});

    $('#user-list')
        .datagrid({
            url: '',
            method: 'GET',
            striped: true,
            loadMsg: '拼命加载中...',
            rownumbers: true,
            pagination: true,
            singleSelect:true,
            checkOnSelect:false,
            selectOnCheck: false,            
            columns: [
                [
                    { field: 'Unique', checkbox: true },
                    { field: 'UserName', title: '用户名', width: '20%', align: 'center' },
                    { field: 'NickName', title: '昵称', width: '20%', align: 'center' },
                    { field: 'CreateTime', title: '注册日期', width: '20%', align: 'center' },
                    { field: 'Age', title: '年龄', width: '20%', align: 'center' },
                    {
                        field: 'Online', title: '是否在线', width: '10%', align: 'center', styler: function (value, row, index) {
                            var color = 'red';
                            if (value==='是') {
                                color = 'green';
                            }
                            return 'background-color:'+color;
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
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '否' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
                { Unique: '123456', UserName: 'youtao', NickName: 'youtao', CreateTime: '2016-07-14', Age: '18', Online: '是' },
            ]
        });

});