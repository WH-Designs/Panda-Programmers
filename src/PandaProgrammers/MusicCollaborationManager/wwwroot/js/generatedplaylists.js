﻿$(function () {
    $("#playlist-form").submit(function (event) {
        event.preventDefault();
    })

    $("#upload-playlist-cover-form").submit(function (event) {
        event.preventDefault();
    })
});

$("#save-playlist-btn").click(function () {

    const values = getNewPlaylistFormValues();
    console.log(`New playlist form status:  ${values.status}`);
    console.log(`Track URIs: ${values.newtrackuris}`);

    $.ajax({
        method: "POST",
        url: "/api/spotifyauth/savegeneratedplaylist",
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(values),
        success: savePlaylist,
        error: errorOnAjax
    });

});

function checkIfAnyTrackInPlaylist() {

    let tracksLeft = $('#playlist-form').children().length - 2; //The "-2" is because of the "save" & "discard" button within the form.
    if (tracksLeft > 0) {
        $("#save-playlist-btn").prop("disabled", false);
    }
    else {
        $("#save-playlist-btn").prop("disabled", true);
    }
    //    console.log(`Tracks left in playlist: ${tracksLeft}`);
};

function clearPlaylistChangeNotification() {
    $(".playlist-change-notification").remove()
}

let recentlyChangedPlaylistTimer = null;

function displayRecentRemovedTrack(trackName) {

    //https://stackoverflow.com/questions/75932984/reset-countdown-timer-when-button-is-pressed -- Roman Marusyk's answer for checking a timer.
    if (recentlyChangedPlaylistTimer !== null) {
        clearTimeout(recentlyChangedPlaylistTimer);
        $(".playlist-change-notification").remove();
        // console.log("Timer has been cleared. (Removal ver.)");
    }

    let recentlyRemovedEntryDisplay = `
    <div id="track-removal-notification" class="p-12 bg-coreback moon:bg-gray-500 classicpanda:bg-secondaryback rounded-2xl fixed playlist-change-notification">
        <p class="text-textback 
            revolution:text-white 
            autumn:text-white 
            classicpanda:text-textback">"${trackName}" has been removed from the playlist.
        </p>
    </div>    
    `;

    $("#explanation-title").append(recentlyRemovedEntryDisplay);
    recentlyChangedPlaylistTimer = setTimeout(clearPlaylistChangeNotification, 3000);
    // console.log("Timer SHOULD HAVE started. (Removal ver.)");
}

function displayRecentlyReAddedTrack(trackName) {
    if (recentlyChangedPlaylistTimer !== null) {
        clearTimeout(recentlyChangedPlaylistTimer);
        $(".playlist-change-notification").remove();
        // console.log("Timer has been cleared. (ReAdded ver.)");
    }

    let recentlyReAddedEntryDisplay = `
    <div id="track-removal-notification" class="p-12 bg-coreback moon:bg-gray-500 classicpanda:bg-secondaryback rounded-2xl fixed playlist-change-notification">
        <p class="text-textback 
            revolution:text-white 
            autumn:text-white 
            classicpanda:text-textback">"${trackName}" has been added to the playlist.
        </p>
    </div>    
    `;
    $("#explanation-title").append(recentlyReAddedEntryDisplay);
    recentlyChangedPlaylistTimer = setTimeout(clearPlaylistChangeNotification, 3000);
    // console.log("Timer SHOULD HAVE started. (ReAdded ver.)");
}

$(".remove-track-btn").click(function () {
    let trackToRemoveId = $(this).attr('id')
    // console.log("ID of track removed: " + trackToRemoveId);

    let removedTrackIndex = trackToRemoveId.substring(18);
    // console.log("Track removed (ID num only): " + removedTrackIndex);

    let entryToRemoveTrackName = $(`#entry-track-name-${removedTrackIndex}`).text();
    let entryToRemoveAlbumName = $(`#entry-album-name-${removedTrackIndex}`).text();

    displayRecentRemovedTrack(entryToRemoveTrackName);

    // console.log(`Removed ENTRY \n Track: ${entryToRemoveTrackName} \n Album: ${entryToRemoveAlbumName}`)

    let removedTrackEntry = `
    <div class="table-row" id="readdable-entry-${removedTrackIndex}">
        <div class="table-cell text-textback classicpanda:text-whitetext p-3 overflow-x-scroll">${entryToRemoveTrackName}</div>
        <div class="table-cell text-textback classicpanda:text-whitetext p-3 overflow-x-scroll">${entryToRemoveAlbumName}</div>
        <button class="cursor-pointer text-textback classicpanda:text-whitetext font-bold p-3 hover:text-textback/50 re-add-entry classicpanda:hover:text-gray-400" id="re-add-entry-${removedTrackIndex}">Add</button>
    </div>
    `;

    let trackUri = $(`#track-${removedTrackIndex}-input`).val();
    $(document).on("click", `#re-add-entry-${removedTrackIndex}`, function () {
        let trackToAddId = $(this).attr('id');
        // console.log(`Track to add ID: ${trackToAddId}`);

        let trackToAddIndex = trackToAddId.substring(13);
        // console.log(`Index of track to REadd: ${trackToAddIndex}`);
        // console.log(`RE-ADDING ENTRY. \n Track Uri: ${trackUri}`);
        let entryToReadd = `
            <input id="track-${removedTrackIndex}-input" value="${trackUri}" type="hidden" name="NewTrackUris"/>
        `;

        $("#playlist-form").append(entryToReadd);

        $(`#song-preview-${removedTrackIndex}`).show();
        $(`#readdable-entry-${removedTrackIndex}`).empty();
        $(`#readdable-entry-${removedTrackIndex}`).remove();

        checkIfAnyTrackInPlaylist();
        displayRecentlyReAddedTrack(entryToRemoveTrackName);
    });



    $("#removed-tracks-table-body").append(removedTrackEntry);
    $(`#song-preview-${removedTrackIndex}`).hide();
    $(`#track-${removedTrackIndex}-input`).remove();

    checkIfAnyTrackInPlaylist();
});

function redirectToGenIndex() {
    //https://stackoverflow.com/questions/503093/how-do-i-redirect-to-another-webpage - Govind Singh's answer.
    $(window).attr('location', '/Generator/Index')

}


function savePlaylist(data) {

    /*    console.log(`Result of saving playlist: ${data["playlistId"]}`);*/
    //console.log("Result of 'SaveMCMGeneratedPlaylist': " + data);

    if (data === null || data === undefined) {
        alert("There was an error adding the playlist to your Spotify account.");
    }
    else {
        alert("The playlist has been added to your Spotify account.");
        console.log(`The result of saving playlist attempt (return val from API): ${data}`);

        let playlistCoverToUse = $("#playlist-img-input-extra").val();

        if (playlistCoverToUse != "NO_PLAYLIST_COVER") {

            $("#new-playlist-id").val(data["playlistId"])

            let newPlaylistDetails = getNewPlaylistImgDetails()
            //console.log(`newPlaylistDetails (status): ${newPlaylistDetails.status}`);
            //console.log(`newPlaylistDetails (playlistid): ${newPlaylistDetails.playlistid}`);

            $.ajax({
                method: "PUT",
                url: "/api/spotifyauth/changeplaylistcover",
                dataType: "json",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify(newPlaylistDetails),
                success: playlistCoverSafe,
                error: errorOnAjax
            });
        }
    }



    //  setTimeout(redirectToGenIndex, 4000); //An "alert" was preferred over this.
}

function playlistCoverSafe(data) {

    if (data["coverSaveSuccessful"] == true) {
        console.log("Playlist cover uploaded successfully!");
    } else {
        alert("There was a problem uploading the cover for the playlist.")
        console.log(`There was a problem uploading the playlist cover`);
    }
}

function errorOnAjax(data) {
    console.log(`Error on Ajax. Data was: ${data.status}`);
}

let currentTheme = localStorage.theme;

$("#discard-playlist-btn").click(function () {

    $("#save-playlist-btn").attr("disabled", true);
    alert("Playlist discarded. You will be redirected shortly.");
    setTimeout(redirectToGenIndex, 3000);
});

//Playing indication (below)----------------

function removePlayingStatusText() {
    $("#playing-status-active").remove();
}

$(".playable-item").click(function () {


    $('#playing-status-active').remove();
    let trackHtmlID = $(this)[0].id;

    let playingStatus = '<p class="text-white" id="playing-status-active" style="font-family: Arial, sans-serif;">Playing...</p>'
    $(`#${trackHtmlID}`).append(playingStatus);

    setTimeout(removePlayingStatusText, 4000);

});

function getNewPlaylistFormValues() {
    let playlistDescriptionText = $("#visible-playlist-description").text();
    $("#new-playlist-description").val(playlistDescriptionText);


    const newPlaylistForm = document.getElementById("playlist-form");
    const tracks = document.getElementsByName('NewTrackUris');
    const playlistName = document.getElementById(`new-playlist-name`);
    const playlistDescription = document.getElementById(`new-playlist-description`);

    //https://stackoverflow.com/questions/15839169/how-to-get-the-value-of-a-selected-radio-button -- Joe's answer.
    let options = document.getElementsByName('NewPlaylistIsVisible');
    let playlistVisibilityDecision;
    for (let i = 0; i < options.length; i++) {
        if (options[i].checked) {
            playlistVisibilityDecision = options[i].value;
        }
    }

    console.log("RADIO CHOOSEN: " + playlistVisibilityDecision + ". type: " + typeof playlistVisibilityDecision);

    if (!newPlaylistForm.checkValidity()) {
        return { status: false };
    }

    let tracksAsArray = [];

    $.each(tracks, function (index, item) {
/*        console.log(`Adding item: ${index} - ${item.value}`);*/
        tracksAsArray.push(item.value);
    });

    let playlistIsPublic = null;
    if (playlistVisibilityDecision == "Public") {
        console.log(`Playlist is PUBLIC`);
        playlistIsPublic = true;
    }
    else {
        console.log(`Playlist is PRIVATE`);
        playlistIsPublic = false;
    }

/*    console.log(`j content: ${j}`);*/

    return {
        newtrackuris: tracksAsArray,
        newplaylistname: playlistName.value,
        newplaylistisvisible: playlistIsPublic,
        newplaylistdescription: playlistDescription.value,
        status: true
    }
}

//function uploadPlaylistCover(data) {
//    console.log(`Result of uploading playlist cover was: ${data}`);
//}

function getNewPlaylistImgDetails() {
    const newPlaylistForm = document.getElementById("upload-playlist-cover-form");
    const playlistimg = document.getElementById("playlist-img-input-extra");
    const newPlaylistId = document.getElementById(`new-playlist-id`);

    if (!newPlaylistForm.checkValidity()) {
        return { status: false };
    }

    return {
        playlistid: newPlaylistId.value,
        playlistimgbasestring: playlistimg.value,
        status: true
    }

}