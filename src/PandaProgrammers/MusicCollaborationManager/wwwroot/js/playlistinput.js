/*Spotify allows for:
    100 characters for playlist NAME
    300 characters for playlist DESCRIPTION*/

function playlistNameIsValid(playlistName) {
    const playlistNameCriteria = /\w+/;

    if (playlistNameCriteria.test(playlistName) == false)
        return false;

    return true;
}

function checkForPlaylistNameInputErrors(playlistNameInput) {

    if (playlistNameInput.length >= 100) {
        console.log("Playlist name must be 100 characters or less.");

        let playlistNameTooLong = `
        <p id="playlist-name-too-long-msg" class="text-red-500 font-bold playlist-name-error-input">
            Playlist name must be 100 characters or less.
        </p>`;

        $("#playlist-name-too-long-msg").remove();
        $(playlistNameTooLong).insertAfter("#titleinput");
    }
    else {
        console.log("Playlist name length requirement satisfied.");
        $("#playlist-name-too-long-msg").remove();
    }

    if (playlistNameIsValid(playlistNameInput) == false) {
        console.log("Playlist name must have at least 1 character that is NOT whitespace!");

        let invalidPlaylistNameMsg = `
        <p id="playlist-name-input-invalid-msg" class="text-red-500 font-bold playlist-name-error-input playlist-name-error-input">
            Playlist name must include at least 1 character that is not whitespace.
        </p>`;

        $("#playlist-name-input-invalid-msg").remove();
        $(invalidPlaylistNameMsg).insertAfter("#titleinput");
    }
    else {
        console.log("Playlist name input meets criteria. (playlist name length criteron not verified here).");
        $("#playlist-name-input-invalid-msg").remove();
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
        $("#optional-playlist-title-note").hide();
    }
    else {
        $("#optional-playlist-title-note").show()
        console.log("AI title will NOT be generated.");
        let playlistNameInput = $("#titleinput").val();

        if (playlistNameInput == "") {

        }
        else {
            checkForPlaylistNameInputErrors(playlistNameInput);
        }

        console.log(`Playlist name input: ${playlistNameInput}`);

        
    }
});
