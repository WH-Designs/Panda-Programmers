
//It's impossible (bases on quick search/attempts) to undo this & give control back to tailwind background properties.
//These function are only to applied to maintain illusion of reverting colors from the user's perspective.)

//$("#PrimaryColorInput").change(function () {
//    let primaryColor = $("#ColorInput").val();
//    console.log(primaryColor);

//    $("#real-background").css("background-color", `#${primaryColor}`)
//});

$(function () {
    console.log("Getting selected theme:\n")
    var classList = $('#main-primary-color-control').attr('class').split(/\s+/);
    var themeName = localStorage.theme;

    $.each(classList, function (index, item) {
        $('#main-primary-color-control').removeClass(item);

    });
    $('#main-primary-color-control').addClass(themeName);

});

$("#PrimaryColorInput").change(function () {
    let primaryColor = $("#PrimaryColorInput").val();
    console.log(primaryColor);

    $("#main-primary-color-control").css("background-color", `#${primaryColor}`)
    $("#main-primary-color-control").children().css("background-color",`#${primaryColor}`);
});

//$("#remove-primary-custom-color-btn").click(function () {
//    $("#main-primary-color-control").css("background-color", "#1F2937");  // <---bg-gray-800
//    $("#main-primary-color-control").children().css("background-color", `#1F2937`);
//});

$("#remove-primary-custom-color-btn").click(function () {
    $("#main-primary-color-control").css("background-color", "none");  // <---bg-gray-800
    $("#main-primary-color-control").children().css("background-color", `none`);
});

//-----------Secondary color (below)------------

$("#SecondaryColorInput").change(function () {
    let secondaryColor = $("#SecondaryColorInput").val();
    console.log(secondaryColor);
    $(".some-container").css("background-color", `#${secondaryColor}`)
});

$("#remove-secondary-custom-color-btn").click(function () {
    $(".some-container").css("background-color", "#D1D5DB"); // <---bg-gray-300
    $(".some-container").css("background-color", "#D1D5DB");
});




//---------------VERY SPECIAL BUTTON (Below)-----------------

// $(function () {
//     if (localStorage.theme === 'classicpanda' /*|| (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)*/) {
//         //document.documentElement.classList.remove('revolution');
//         //document.documentElement.classList.add('classicpanda');
//     }
//     else if (localStorage.theme === 'revolution') {
//         document.documentElement.classList.remove('classicpanda');
//         document.documentElement.classList.add('')
//     }
//     else if (localStorage.theme === 'zenmartian') {
//         document.documentElement.classList.remove('classicpanda');
//         document.documentElement.classList.add('zenmartian');
//     }
//     else if (localStorage.theme === 'elementary') {
//         document.documentElement.classList.remove('classicpanda');
//         document.documentElement.classList.add('elementary');
//     }


//     //localStorage.theme = 'light'

//     //localStorage.theme = 'dark'

//     //localStorage.removeItem('theme')
// });



//$("#changing-btn").click(function () {
//    console.log("CHANGE BTN CLICKED!");


//    event.preventDefault();

//    //document.documentElement.classList.toggle('dark')
//    //$('body').addClass("revolution");
//    //console.log("Length after: " + document.documentElement.classList.length)


//    document.documentElement.classList.toggle('classicpanda')

//    if (document.documentElement.classList.length == 2) {
//        console.log("Length before: " + document.documentElement.classList.length)
//        localStorage.theme = 'classicpanda'
//    }
//    else {
//        console.log("Length before: " + document.documentElement.classList.length)
//        localStorage.theme = 'revolution'
//    }
//});
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
    console.log("THE REVOLUTION HAS BEGUN!")
    getSelectedTheme('revolution');
});

$("#moon-btn").click(function () {
    console.log("The moon")
    getSelectedTheme('moon');
});

$("#zenmmartian-btn").click(function () {
    console.log("MARTIAN BTN CLICKED")
    getSelectedTheme('zenmartian');
});

$("#elementary-btn").click(function () {
    console.log("ELEMENTARY");
    getSelectedTheme('elementary');
});

$("#check-class-btn").click(function () {
    console.log("CHECKING CLASSES..");
    var classList = $('#main-primary-color-control').attr('class').split(/\s+/);

    console.log("\n")
    $.each(classList, function (index, item) {
        console.log("ITEM:" + item);

    });

});




$("#changing-btn").click(function () {
    console.log("CHANGE BTN CLICKED!");


    event.preventDefault();

    document.documentElement.classList.toggle('classicpanda')
    //$('body').addClass("revolution");
    //console.log("Length after: " + document.documentElement.classList.length)


    //document.documentElement.classList.remove('revolution');
    //document.documentElement.classList.add('classicpanda')
  

    if (document.documentElement.classList.length == 2) {
        console.log("Length before: " + document.documentElement.classList.length)
        localStorage.theme = 'classicpanda'
    }
    else {
        console.log("Length before: " + document.documentElement.classList.length)
        localStorage.theme = 'revolution'
    }
});