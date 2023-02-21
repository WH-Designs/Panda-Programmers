$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authuser",
        success: getAuthUser,
        error: errorOnAjax
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authtoptracks",
        success: getAuthTopTracks,
        error: errorOnAjax
    });
});


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function getAuthUser(data)
{
    console.log(data);
    let htmlDisplayName = `<h1 class="flex flex-col items-center justify-center h-screen w-screen">${data["displayName"]}</h1> `
    $("#display-name-div").append(htmlDisplayName);
}

function getAuthTopTracks(data)
{
    console.log(data);

    $.each(data, function (index, item) {
        let trackName = `<a href="${item["externalUrls"]["spotify"]}">${item["name"]}</a>`;
        let artistName = `<p>${item["artists"][0]["name"]}</p>`;
        let trackImage = `<img src="${item["album"]["images"][1]["url"]}">`;

        $(trackImage).appendTo(`#user-track-${index}-container`);
        $(trackName).appendTo(`#user-track-${index}-container`);
        $(artistName).appendTo(`#user-track-${index}-container`);
    });
}
