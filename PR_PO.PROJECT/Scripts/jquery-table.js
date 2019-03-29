//Suchat.Si 20140422
//Use class NotJTable for none apply css

(function ($) {
    $.fn.styleTable = function (options) {
        var defaults = {
            css: 'ui-styled-table'
        };
        options = $.extend(defaults, options);
        
        return this.each(function () {
            $this = $(this);
            $this.addClass(options.css);

            //$this.on('mouseover mouseout', 'tbody tr', function (event) {
            //    $(this).children().toggleClass("ui-state-hover", event.type == 'mouseover');
            //});
            
            $this.find("th").not(".NotJTable th").addClass("ui-state-default");
            $this.find("td").not(".NotJTable td").addClass("ui-widget-content");
            $this.find("tr:last-child").not(".NotJTable tr").addClass("last-child");

            //$this.find("th").addClass("ui-state-default");
            //$this.find("td").addClass("ui-widget-content");
            //$this.find("tr:last-child").addClass("last-child");
        });
    };

    //$("table").styleTable();
})(jQuery);