$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyvisitor/playlists",
        success: getVisitorPlaylists,
        error: errorOnAjax
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyvisitor/topsongs",
        success: getVisitorTracks,
        error: errorOnAjax
    });
});


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function getVisitorPlaylists(data)
{
    $.each(data, function (index, item) {
        let playlistName = `<a href="${item["spotifyLinkToPlaylist"]}">${item["playlistName"]}</a>`;
        let playlistImage = `<img src="${item["playlistImageURL"]}">`;


        $(playlistImage).appendTo(`#playlist-${index}-container`);
        $(playlistName).appendTo(`#playlist-${index}-container`);
    });
}

function getVisitorTracks(data)
{
    $.each(data, function (index, item) {
        let trackName = `<a href="${item["spotifyTrackLinkURL"]}">${item["name"]}</a>`;
        let trackImage = `<img src="${item["imageURL"]}">`;

        
        $(trackImage).appendTo(`#track-${index}-container`);
        $(trackName).appendTo(`#track-${index}-container`);
    });
}