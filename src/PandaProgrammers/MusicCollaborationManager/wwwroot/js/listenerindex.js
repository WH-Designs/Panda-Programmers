$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authuser",
        success: getAuthUser,
        error: errorOnAjax
    });
});


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function getAuthUser(data)
{
    /*console.log(data);*/
    let htmlDisplayName = `<h1 class="flex flex-col items-center justify-center h-screen w-screen">${data["displayName"]}</h1> `
    $("#display-name-div").append(htmlDisplayName);
}