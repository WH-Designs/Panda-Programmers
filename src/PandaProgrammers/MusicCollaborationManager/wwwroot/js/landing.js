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
    // take care of the error, maybe display a message to the user
    // ...
}

function getVisitorPlaylists(data)
{
    $.each(data, function (index, item) {
        console.log(`Index ${index}: ${item["spotifyLinkToPlaylist"]}`);
        let playlistName = `<p><a href="${item["spotifyLinkToPlaylist"]}">${item["playlistName"]}</a></p>`;
        let playlistImage = `<img src="${item["playlistImageURL"]}">`;


        $(playlistImage).appendTo(`#playlist-${index}-container`);
        $(playlistName).appendTo(`#playlist-${index}-container`);
    });
}

function getVisitorTracks(data)
{
    $.each(data, function (index, item) {
        let trackName = `<p><a href="${item["spotifyTrackLinkURL"]}">${item["name"]}</a></p>`;
        let trackImage = `<img src="${item["imageURL"]}">`;
        let previewURL = `<p><a href="${item["trackPreviewURL"]}">Preview link</a></p>`

        console.log(`Track ${index} (preview URL): ${item["trackPreviewURL"]}`);

        
        $(trackImage).appendTo(`#track-${index}-container`);
        $(trackName).appendTo(`#track-${index}-container`);
    });
}