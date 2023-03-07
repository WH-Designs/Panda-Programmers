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

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authtopartists",
        success: getAuthTopArtists,
        error: errorOnAjax
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/SpotifyAuth/authplaylists",
        success: getRecomPlaylists,
        error: errorOnAjax
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/SpotifyAuth/authpersonalplaylists",
        success: getPersonalPlaylists,
        error: errorOnAjax
    });
});


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function getAuthUser(data)
{
    // console.log(data);
    let htmlDisplayName = `<h1 class="flex flex-col items-center justify-center h-screen w-screen">${data["displayName"]}</h1> `
    $("#display-name-div").append(htmlDisplayName);
}

function getAuthTopTracks(data)
{
    // console.log(data);

    $.each(data, function (index, item) {
        let trackName = `<a href="${item["externalUrls"]["spotify"]}">${item["name"]}</a>`;
        let artistName = `<p>${item["artists"][0]["name"]}</p>`;
        let trackImage = `<img src="${item["album"]["images"][1]["url"]}">`;

        $(trackImage).appendTo(`#user-track-${index}-container`);
        $(trackName).appendTo(`#user-track-${index}-container`);
        $(artistName).appendTo(`#user-track-${index}-container`);
    });
}


function getAuthTopArtists(data) 
{
    let genreList = [];
    const genreDict = {};

    data.forEach(item => {item["genres"].forEach(genre => genreList.push(genre));});
    
    genreList.sort();
    genreList.forEach(genre => {genreDict[genre] = (genreDict[genre] || 0) + 1}); // found here: https://www.jsowl.com/count-duplicate-values-in-an-array-in-javascript/

    var items = Object.keys(genreDict).map((key) => { return [key, genreDict[key]] });
    items.sort((first, second) => { return first[1] - second[1] });
    var keys = items.map((e) => { return e[0] });

    // sorting dictionary method^ found here: https://www.educative.io/answers/how-can-we-sort-a-dictionary-by-value-in-javascript

    let count = 0;
    for (let i = keys.length - 1; i >= 0; i--) {
        let currentGenre = `<p>${keys[i]}</p>`;
        $(currentGenre).appendTo(`#user-genre-${count}-container`);  
        count = count + 1;  
    }   
}


function getRecomPlaylists(data) {

    $.each(data, function (index, item) {

        let playlistImage = `<img src="${item["playlistImageURL"]}">`;
        $(playlistImage).appendTo(`#user-playlist-${index}-container`);

        let playlistName = `<a href="${item["spotifyToPlaylist"]}">${item["playlistName"]}</a>`;
        $(playlistName).appendTo(`#user-playlist-${index}-container`);

    });
}

function getPersonalPlaylists(data) {
    console.log(data);
    $.each(data, function(index, item) {
        let playlistImage = `<img src="${item["playlistImageURL"]}">`;
        $(playlistImage).appendTo(`#user-personal-playlist-${index}-container`);

        let playlistName = `<a href="${item["spotifyToPlaylist"]}">${item["playlistName"]}</a>`;
        $(playlistName).appendTo(`#user-personal-playlist-${index}-container`);
    });
}
