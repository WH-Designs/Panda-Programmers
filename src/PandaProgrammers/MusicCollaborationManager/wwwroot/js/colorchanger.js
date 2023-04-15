//It's impossible (bases on quick search/attempts) to undo this & give control back to tailwind background properties.
//These function are only to applied to maintain illusion of reverting colors from the user's perspective.)

var savedTheme = "";

$(function () {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/ListenerInfo/basicuserinfo/me",
        success: getSavedTheme,
        error: errorOnAjax
    });

    // console.log("Getting selected theme:\n")
    var themeName = localStorage.theme;
    if(themeName === null || themeName == undefined){
        $('#main-primary-color-control').addClass("classicpanda");
    }
    else{
        $('#main-primary-color-control').addClass(themeName);
    }

});


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function getSelectedTheme(themeName) {

    console.log("Getting selected theme:\n")
    let activeThemes = document.getElementById("main-primary-color-control")
    activeThemes.removeAttribute("class");

    $('#main-primary-color-control').addClass(themeName);
    console.log("Selected theme: " + themeName);
    localStorage.theme = themeName;
}


function getSavedTheme(data) {
    currentTheme = data["theme"];
    localStorage.theme = currentTheme;
}

//-------All current themes (below)------------

$("#classicpanda-btn").click(function () {
    
    savedTheme = 'classicpanda';

    $.ajax({
        method: "POST",
        url: "/api/Theme/themeAdd/" + savedTheme,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: savedTheme,
        success: console.log("success for classicpanda"),
        error: errorOnAjax
    });

    getSelectedTheme(savedTheme);
});

$("#autumn-btn").click(function () {
    
    savedTheme = 'autumn';
    
    $.ajax({
        method: "POST",
        url: "/api/Theme/themeAdd/" + savedTheme,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: savedTheme,
        success: console.log("success for autumn"),
        error: errorOnAjax
    });

    getSelectedTheme(savedTheme);
});

$("#mansion-btn").click(function () {
    
    savedTheme = 'luxury';

    $.ajax({
        method: "POST",
        url: "/api/Theme/themeAdd/" + savedTheme,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(savedTheme),
        success: console.log("success for luxury"),
        error: errorOnAjax
    });

    getSelectedTheme(savedTheme);
});

$("#revolution-btn").click(function () {
    
    savedTheme = 'revolution';

    $.ajax({
        method: "POST",
        url: "/api/Theme/themeAdd/" + savedTheme,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: savedTheme,
        success: console.log("success for revolution"),
        error: errorOnAjax
    });

    getSelectedTheme(savedTheme);
});

$("#moon-btn").click(function () {
    
    savedTheme = 'moon';
    
    $.ajax({
        method: "POST",
        url: "/api/Theme/themeAdd/" + savedTheme,
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: savedTheme,
        success: console.log("the moon is beautiful"),
        error: errorOnAjax
    });

    getSelectedTheme(savedTheme);
});