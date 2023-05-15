//AI & user input
//"TrackInput" generator does NOT have a input for the user to name the track or for an AI to name the track.

//100 characters for playlist NAME
//300 characters for playlist DESCRIPTION

function playlistNameIsValid(playlistName) {
    const playlistNameCriteria = /\w+/;

    if (playlistNameCriteria.test(playlistName) == false)
        return false;

    return true;
}

function checkForPlaylistNameInputErrors(playlistNameInput) {

    let playlistNameHasCharacters = true;
    let playlistNameAcceptableLength = true;

    if (playlistNameInput.length >= 100) {
        console.log("Playlist name must be 100 characters or less.");

        let playlistNameTooLong = `
        <p id="playlist-name-too-long-msg" class="text-red-500 font-bold playlist-name-error-input">
            Playlist name must be 100 characters or less.
        </p>`;

        $("#playlist-name-too-long-msg").remove();
        $(playlistNameTooLong).insertAfter("#titleinput");
        playlistNameAcceptableLength = false;
    }
    else {
        console.log("Playlist name length requirement satisfied.");
        $("#playlist-name-too-long-msg").remove();
        playlistNameAcceptableLength = true;
    }

    if (playlistNameIsValid(playlistNameInput) == false) {
        console.log("Playlist name must have at least 1 character that is NOT whitespace!");

        let invalidPlaylistNameMsg = `
        <p id="playlist-name-input-invalid-msg" class="text-red-500 font-bold playlist-name-error-input playlist-name-error-input">
            Playlist name must include at least 1 character that is not whitespace.
        </p>`;

        $("#playlist-name-input-invalid-msg").remove();
        $(invalidPlaylistNameMsg).insertAfter("#titleinput");
        playlistNameHasCharacters = false;
    }
    else {
        console.log("Playlist name input meets criteria. (playlist name length criteron not verified here).");
        $("#playlist-name-input-invalid-msg").remove();
        playlistNameHasCharacters = true;
    }

    if ((playlistNameHasCharacters == false) || (playlistNameAcceptableLength == false)) {
        let defaultPlaylistNameMsg = `
            <p id="default-playlist-name-msg" class="text-red-500 font-bold playlist-name-error-input">
                (A default name will be given to the playlist if you proceed.)
            </p>`;

        $("#default-playlist-name-msg").remove();
        $(defaultPlaylistNameMsg).insertAfter("#titleinput");
        console.log(`character ok: ${playlistNameHasCharacters} \n length ok: ${playlistNameAcceptableLength}`);
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


$("#aiTitle").click(function () {

    let AITitleWillBeGenerated = $("#aiTitle").val();
    console.log(`aiTitle (value): ${AITitleWillBeGenerated}`);

    //If it's not checked:
    console.log(`AI title type: ${typeof(AITitleWillBeGenerated)}`);

    if (AITitleWillBeGenerated == "true") {
        console.log("AI title will be generated.");
        $(".playlist-name-error-input").remove();
    }
    else {
        console.log("AI title will NOT be generated.");
        let playlistNameInput = $("#titleinput").val();
        console.log(`Playlist name input: ${playlistNameInput}`);

        checkForPlaylistNameInputErrors(playlistNameInput);
    }
});
