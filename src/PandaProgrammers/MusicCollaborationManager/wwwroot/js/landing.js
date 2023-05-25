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

    $("#alert-button").on("click", function () {
        $("#generator-alert").hide();
    });
});


function errorOnAjax(data) {
    console.log("ERROR in ajax request: " + data.status + " " + data.statusText);
}

function getVisitorPlaylists(data) {
    $.each(data, function (index, item) {
        let playlistName = `<a href="${item["spotifyLinkToPlaylist"]}">${item["playlistName"]}</a>`;
        let playlistImage = `<img src="${item["playlistImageURL"]}">`;


        $(playlistImage).appendTo(`#playlist-${index}-container`);
        $(playlistName).appendTo(`#playlist-${index}-container`);
    });
}

function getVisitorTracks(data) {
    $.each(data, function (index, item) {
        let trackName = `<a href="${item["spotifyTrackLinkURL"]}">${item["name"]}</a>`;
        let trackImage = `<img src="${item["imageURL"]}">`;


        $(trackImage).appendTo(`#track-${index}-container`);
        $(trackName).appendTo(`#track-${index}-container`);
    });
}

var ytPlayerIsViewable = false;

$(".yt-thumbnail").click(function () {
    let ytVideoClickedID = $(this).attr('id');

    let ytVideoIndex = ytVideoClickedID.substr(ytVideoClickedID.length - 1);
    console.log("ID of ytVideoIndex: " + ytVideoClickedID);
    console.log("Index: " + ytVideoIndex);
    console.log("thumbnail clicked");
    $("#yt-player-container").removeClass("hidden");

    if (ytPlayerIsViewable == false) {
        ytPlayerIsViewable = true;
        let ytPlayerContainter = document.getElementById("yt-player-container");
        ytPlayerContainter.scrollIntoView();
    }

    changeVideoById(ytVideoClickedID);
});


// YouTube iframe player (below)------------------

//Code for removing script tag from HTML taken from here: https://www.youtube.com/watch?v=SVSf8fNp_kg&ab_channel=AlgosExplained
var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
        height: '500',
        width: '100%',
        videoId: 'Kf4GkHsRB2w',
        playerVars: {
            'playsinline': 1
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    event.target.changeVideoById(event.target.videoId);
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
var done = false;
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING && !done) {
        done = true;
    }
}
function stopVideo() {
    player.stopVideo();
}

function changeVideoById(videoId) {
    player.cueVideoById(videoId);
}