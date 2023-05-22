$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authuser",
        success: GetAuthUserAsync,
        error: errorOnAjax
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authtopartists",
        success: TopArtistDomManip,
        error: errorOnAjax
    });
    
    $("#alert-button").on("click", function () {
        $("#generator-alert").hide();
    });

});

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function GetAuthUserAsync(data)
{
    // console.log(data);
    let htmlDisplayName = `<h1 class="font-header flex flex-col items-center justify-center h-screen w-screen">${data["displayName"]}</h1> `
    $("#display-name-div").append(htmlDisplayName);
}


function TopArtistDomManip(data) {
    var keys = GetAuthTopArtistsAsync(data);
    
    let count = 0;
    for (let i = keys.length - 1; i >= 0; i--) {
        let currentGenre = `<p>${keys[i]}</p>`;
        let genreContainer = `<div class="flex items-center justify-center md:h-[100px] md:w-[full] text-2xl rounded-xl shadow-xl shadow-gray-950 p-5 bg-primback  text-textback classicpanda:text-blacktext luxury:text-yellow-500" id="user-genre-${count}-container">${currentGenre}</div>`
        $("#userGenresContainer").append(genreContainer);
        count = count + 1;  
    } 
}

function GetAuthTopArtistsAsync(data) 
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
    return keys;
}


//Managning extra items (below)---------------

let extraRecomTracksVisible = false;

$("#toggle-recom-tracks-btn").click(function () {

    if (extraRecomTracksVisible) {
        $(".extra-recom-tracks").removeClass("hidden");
        $("#toggle-recom-tracks-btn").text("Show more");
        extraRecomTracksVisible = false;
        $("#link-to-spotify-recom-tracks").remove();

        $("#link-to-spotify-recom-tracks").removeClass("hidden");
    }
    else {
        $("#toggle-recom-tracks-btn").text("Show less");
        extraRecomTracksVisible = true;
    }

    $("#link-to-spotify-recom-tracks").toggle();
    $(".extra-recom-tracks").toggle();
});

let extraFeatPlaylistsVisible = false;

$("#toggle-feat-playlists-btn").click(function () {
    console.log("Feat playlist btn CLICKED")
    if (extraFeatPlaylistsVisible) {
        $(".extra-feat-playlists").removeClass("hidden");
        $("#toggle-feat-playlists-btn").text("Show more");
        extraFeatPlaylistsVisible = false;
        $("#link-to-spotify-feat-playlists").removeClass("hidden");
    }
    else {
        $("#toggle-feat-playlists-btn").text("Show less");
        extraFeatPlaylistsVisible = true;
    }

    $("#link-to-spotify-feat-playlists").toggle();
    $(".extra-feat-playlists").toggle();
});


let extraUserPlaylistsVisible = false;
$("#toggle-user-playlists-btn").click(function () {

    if (extraUserPlaylistsVisible) {
        $(".extra-user-playlists").removeClass("hidden");
        $("#toggle-user-playlists-btn").text("Show more");
        extraUserPlaylistsVisible = false;

        $("#link-to-spotify-user-playlists").removeClass("hidden");

    }
    else {
        $("#toggle-user-playlists-btn").text("Show less");
        extraUserPlaylistsVisible = true;
    }

    $("#link-to-spotify-user-playlists").toggle();
    $(".extra-user-playlists").toggle();

});

