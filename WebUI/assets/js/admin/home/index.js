$(function () {
    $('#tabs-main').tabs({
        border: false
    });

    $('#accordion-main').accordion({
        fit: true,
        border: 0,
        //onAdd: function (title, index) {
        //    var panel = $('#accordion-main').accordion('getPanel', index);
        //    panel.content = '111';
        //    console.log(panel)
        //}
    });

    $.get('/admin/menu/getmenu',
        null,
        function (parent) {
            for (var i = 0; i < parent.length; i++) {
                $('#accordion-main').accordion('add', {
                    title: parent[i].Title,
                    iconCls: parent[i].Icon,
                    href: '/admin/menu/getmenu?parentId=' + parent[i].Id,
                    extractor: function (data) {
                        var html = '';                        
                        data = JSON.parse(data);
                        for (var j = 0; j < data.length; j++) {
                            html += '<p style="font-size:14px;text-align:center;margin-top:3px;margin-bottom:3px;">';
                            html += '<i class="' + data[j].Icon + '" style="color:#FFE48D;">';
                            html += '</i>';
                            html += '<a name="menu-button" data-url="' + data[j].Url + '" data-icon="' + data[j].Icon + '" href="javascript:void(0);" style="text-decoration:none;color:black;">' +
                                data[j].Title +
                                '</a>';
                            html += '</p><hr>';
                        }                        
                        return html;
                    }
                });
            }

        });

    $('a').click(function() {
            
        });

    $('#accordion-main')
        .on('click',
            'a[name=menu-button]',
            function() {
                var url = $(this).attr('data-url');
                var title = $(this).text();
                var icon = $(this).attr('data-icon');
                var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;" ></iframe>';
                $('#tabs-main').tabs('add', {
                    title: title,
                    closable: true,
                    icon: icon,
                    loadMsg:'ddd',
                    content: content
                });
            });
});