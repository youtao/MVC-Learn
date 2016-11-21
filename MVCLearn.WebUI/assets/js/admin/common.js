Date.prototype.Format = /**
                         * 格式化日期时间
                         * @param {string} fmt 日期格式
                         * @returns {string}
                         */
    function (fmt) {
        var o = {
            "M+": this.getMonth() + 1, //月份
            "d+": this.getDate(), //日
            "h+": this.getHours(), //小时
            "m+": this.getMinutes(), //分
            "s+": this.getSeconds(), //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            "S": this.getMilliseconds() //毫秒
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (o.hasOwnProperty(k))
                if (new RegExp('(' + k + ')').test(fmt))
                    fmt = fmt.replace(RegExp.$1,
                    (RegExp.$1.length === 1) ? (o[k]) : (('00' + o[k]).substr(('' + o[k]).length)));
        return fmt;
    };

Date.prototype.AddDate = /**
                          * 添加天数
                          * @param {number} days 要增加的天数
                          * @returns {string}
                          */
    function (days) {
        this.setDate(this.getDate() + days);
        var m = this.getMonth() + 1;
        if (m.length == 1) {
            m = '0' + m;
        }
        return this.getFullYear() + '-' + m + '-' + this.getDate();
    };