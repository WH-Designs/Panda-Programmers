//AI & user input
//"TrackInput" generator does NOT have a input for the user to name the track or for an AI to name the track.

//100 characters for playlist NAME
//300 characters for playlist DESCRIPTION


function clearAllPlaylistNameInputErrorMessages() {
    console.log("Erasing all playlist NAME error messages."); 

    
}

function playlistNameIsValid(playlistName) {
    const playlistNameCriteria = /\w+/;

    if (playlistNameCriteria.test(playlistName) == false)
        return false;

    return true;
}

function checkForPlaylistNameInputErrors(playlistNameInput) {

    if (playlistNameInput.length >= 100) {
        console.log("Playlist name must be less than 100 characters.");

        let playlistNameTooLong = `
        <p id="playlist-name-too-long-msg" class="playlist-name-error-input">
            Playlist name must be 100 characters or less.
        </p>`;

        $("#titleinput").append(playlistNameTooLong);
    }
    else {
        console.log("Playlist name length requirements have been satisfied.");
        $("#playlist-name-too-long-msg").empty();
        $("#playlist-name-too-long-msg").remove();
    }

    if (playlistNameIsValid(playlistNameInput) == false) {
        console.log("Playlist name must have at least 1 character that is NOT whitespace!");

        let invalidPlaylistNameMsg = `
        <p id="playlist-name-input-invalid-msg" class="playlist-name-error-input">
            Playlist name must include at least 1 character that is not whitespace.
        </p>`;

        $("#titleinput").append(invalidPlaylistNameMsg);
    }
    else {
        console.log("Playlist name input meets criteria. (playlist name length criteron not verified here).");
        $("#playlist-name-input-invalid-msg").empty();
        $("#playlist-name-input-invalid-msg").remove();
    }
  
}

$("#titleinput").keyup(function () {

    let playlistNameInput = $("#titleinput").val();
    console.log(`Playlist name input: ${playlistNameInput}`);

    checkForPlaylistNameInputErrors(playlistNameInput);
});


//$("#aiTitle").click(function () {

//    //If it's not checked:
//    clearAllPlaylistNameInputErrorMessages();

//    //If it is checked
//});
