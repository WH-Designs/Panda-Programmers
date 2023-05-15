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

    let anyNameInputErrors = true;

    if (playlistNameInput.length >= 100) {
        console.log("Playlist name must be 100 characters or less.");

        let playlistNameTooLong = `
        <p id="playlist-name-too-long-msg" class="text-red-500 font-bold playlist-name-error-input">
            Playlist name must be 100 characters or less.
        </p>`;

        $("#playlist-name-too-long-msg").remove();
        $(playlistNameTooLong).insertAfter("#titleinput");
        anyNameInputErrors = true;
    }
    else {
        console.log("Playlist name length requirement satisfied.");
        $("#playlist-name-too-long-msg").remove();
        anyNameInputErrors = false;
    }

    if (playlistNameIsValid(playlistNameInput) == false) {
        console.log("Playlist name must have at least 1 character that is NOT whitespace!");

        let invalidPlaylistNameMsg = `
        <p id="playlist-name-input-invalid-msg" class="text-red-500 font-bold playlist-name-error-input playlist-name-error-input">
            Playlist name must include at least 1 character that is not whitespace.
        </p>`;

        $("#playlist-name-input-invalid-msg").remove();
        $(invalidPlaylistNameMsg).insertAfter("#titleinput");
        anyNameInputErrors = true;
    }
    else {
        console.log("Playlist name input meets criteria. (playlist name length criteron not verified here).");
        $("#playlist-name-input-invalid-msg").remove();
        anyNameInputErrors = false;
    }

    if (anyNameInputErrors) {
        let defaultPlaylistNameMsg = `
            <p id="default-playlist-name-msg" class="text-red-500 font-bold playlist-name-error-input">
                A default name will be given to the playlist if you proceed.
            </p>`;

        $("#default-playlist-name-msg").remove();
        $(defaultPlaylistNameMsg).insertAfter("#titleinput");
    }
    else {
        $("#default-playlist-name-msg").remove();
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
