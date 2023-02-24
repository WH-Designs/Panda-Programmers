$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/spotifyauth/authuser",
        success: getAuthUser,
        error: errorOnAjax
    });

    //$.ajax({
    //    type: "GET",
    //    dataType: "json",
    //    url: "/api/spotifyauth/authtoptracks",
    //    success: getAuthTopTracks,
    //    error: errorOnAjax
    //});

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

    //window.onSpotifyIframeApiReady = (IFrameAPI) => {
    //    let element = document.getElementById('embed-iframe');
    //    let options = {
    //        width: '60%',
    //        height: '200',
    //        uri: 'spotify:track:2iuZJX9X9P0GKaE93xcPjk'
    //    };
    //    let callback = (EmbedController) => {   //Does NOT load any songs that get loaded AFTERWARDS. Consider loading all songs from controller into a VM.
    //        document.querySelectorAll('.some-song').forEach(
    //            episode => {
    //                episode.addEventListener('click', () => {
    //                    EmbedController.loadUri(episode.dataset.spotifyId)
    //                });
    //            })
    //    };
    //    IFrameAPI.createController(element, options, callback);
    //};
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


//function getAuthTopTracks(data)  //To add: "Play" btn w/ Uri 
//{
//  /*  console.log(data);*/

//    $.each(data, function (index, item) {

//        let trackName = `<a href="${item["externalUrls"]["spotify"]}">${item["name"]}</a>`;
//        let artistName = `<p>${item["artists"][0]["name"]}</p>`;
//        let trackImage = `<img src="${item["album"]["images"][1]["url"]}">`;
//        //let playBtn = `<button class="some-song" data-spotify-id="${item["uri"]}">
//        //                  Play
//        //               </button>`;

//        $(trackImage).appendTo(`#user-track-${index}-container`);
//        $(trackName).appendTo(`#user-track-${index}-container`);
//        $(artistName).appendTo(`#user-track-${index}-container`);
// /*       $(playBtn).appendTo(`#user-track-${index}-container`);*/
//    });
    


//}

function getRecomPlaylists(data) {

    $.each(data, function (index, item) {

        let playlistImage = `<img src="${item["playlistImageURL"]}">`;
        $(playlistImage).appendTo(`#user-playlist-${index}-container`);

        let playlistName = `<a href="${item["spotifyToPlaylist"]}">${item["playlistName"]}</a>`;
        $(playlistName).appendTo(`#user-playlist-${index}-container`);

    });
}

function getPersonalPlaylists(data) {

    $.each(data, function(index, item) {
        let playlistImage = `<img src="${item["playlistImageURL"]}">`;
        $(playlistImage).appendTo(`#user-personal-playlist-${index}-container`);

        let playlistName = `<a href="${item["spotifyToPlaylist"]}">${item["playlistName"]}</a>`;
        $(playlistName).appendTo(`#user-personal-playlist-${index}-container`);
    });
}