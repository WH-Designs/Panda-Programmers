$(document).ready(function () {
    $(".collapsible").on("click", function () {
        $(this).toggleClass("active");
        var content = $(this).next();
        if (content.is(":visible")) {
            content.hide();
        } else {
            content.show();
        }
    });
});
