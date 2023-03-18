
//It's impossible (bases on quick search/attempts) to undo this & give control back to tailwind background properties.
//These function are only to applied to maintain illusion of reverting colors from the user's perspective.)


$(function () {
    console.log("Getting selected theme:\n")
    var classList = $('#main-primary-color-control').attr('class').split(/\s+/);
    var themeName = localStorage.theme;

    $.each(classList, function (index, item) {
        $('#main-primary-color-control').removeClass(item);

    });
    $('#main-primary-color-control').addClass(themeName);

});


function getSelectedTheme(themeName) {
    console.log("Getting selected theme:\n")
    var classList = $('#main-primary-color-control').attr('class').split(/\s+/);

    $.each(classList, function (index, item) {
        $('#main-primary-color-control').removeClass(item);
       
    });
    $('#main-primary-color-control').addClass(themeName);
    console.log("Selected theme: " + themeName);
    localStorage.theme = themeName;
}


//-------All current themes (below)------------

$("#classicpanda-btn").click(function () {
    getSelectedTheme('classicpanda');
});

$("#autumn-btn").click(function () {
    console.log('autumn');
    getSelectedTheme('autumn');
});

$("#mansion-btn").click(function () {
    console.log('luxury');
    getSelectedTheme('luxury');
});

$("#revolution-btn").click(function () {
    console.log("revolution")
    getSelectedTheme('revolution');
});

$("#moon-btn").click(function () {
    console.log("The moon")
    getSelectedTheme('moon');
});