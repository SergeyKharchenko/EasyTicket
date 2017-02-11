(function($) {
    var app = $.sammy(function() {
        this.get('#/main',
            function() {
                $(".page").hide();
                $("#main-page").show();
            });
        this.get('#/request',
            function() {
                $(".page").hide();
                $("#request-page").show();
            });
        this.get(/.*/,
            function() {
                this.redirect('#/request');
            });
    });
    $(function() {
        app.run();
    });
})(jQuery);