(function ($) {
    //判断该元素是否是另外一个元素的子元素或者自身
    $.fn.isChildAndSelfOf = function (selector) {
        return (this.closest(selector).length > 0);
    };
})(jQuery);
(function ($) {
    $.fn.serializeJson = function () {
        var json = {};
        $(this.serializeArray()).each(function () {
            json[this.name] = this.value;
        });
        return json;
    }
})(jQuery);