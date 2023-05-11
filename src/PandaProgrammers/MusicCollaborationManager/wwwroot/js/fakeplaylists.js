$(function () {
    $("#playlist-form").submit(function (event) {
        event.preventDefault();
    })

    $("#upload-playlist-cover-form").submit(function (event) {
        event.preventDefault();
    })
});


$("#save-playlist-btn").click(function () {

    const values = getNewPlaylistFormValues();
    console.log(`No errors obtaining playlist info: ${values.status}`);

    console.log(`Playlist name: ${values.newplaylistname}`);
    console.log(`Playlist description:  ${values.newplaylistDescription}`);
    console.log(`Playlist is 'public': ${values.isnewplaylistpublic}`);

    //$.ajax({
    //    method: "POST",
    //    url: "/api/spotifyauth/savegeneratedplaylist",
    //    dataType: "json",
    //    contentType: "application/json; charset=UTF-8",
    //    data: JSON.stringify(values),
    //    success: savePlaylist,
    //    error: errorOnAjax
    //});

});

function getNewPlaylistFormValues() {
    let playlistDescriptionText = $("#visible-playlist-description").text();
    $("#new-playlist-description").val(playlistDescriptionText);


    const newPlaylistForm = document.getElementById("playlist-form");
    /*    const tracks = document.getElementsByName('NewTrackUris');*/

    const playlistName = document.getElementById(`new-playlist-name`);
    const playlistDescription = document.getElementById(`new-playlist-description`);
    const playlistVisibility = document.getElementById(`new-playlist-visibility`);

    let playlistIsPublic = null;

    if (document.getElementById(`new-playlist-visibility`).checked) {
        playlistIsPublic = true;
    }
    else {
        playlistIsPublic = false;
    }

    if (!newPlaylistForm.checkValidity()) {
        return { status: false };
    }


    //let j = [];

    //$.each(tracks, function (index, item) {
    //    /*        console.log(`Adding item: ${index} - ${item.value}`);*/
    //    j.push(item.value);
    //});

    /*    console.log(`j content: ${j}`);*/

    return {
        /*        newtrackuris: j,*/
        newplaylistname: playlistName.value,  //NEEDS TESTING. NEED TO ADD A FUNCTION FOR TESTING whitespaace/empty input.
        newplaylistDescription: playlistDescription.value, //NEEDS TESTING
        isnewplaylistpublic: playlistIsPublic, //NEEDS TESTING
        status: true
    }
}


function validatePlaylistName(theName) {
    const re = /^[a-zA-Z\. \s \_ \,\-\']+$/;

    if (re.test(theName) == false)
        return false;

    return true;
}

function checkIfPlaylistNameIsValid(playlistName) {

    $(".playlist-name-error-feedback").empty();
    $(".playlist-name-error-feedback").remove();

    if (playlistName.length >= 100) {
        console.log("Playlist name is too long! (100 characters max)");
        let tooManyCharsMsg = `
        <p id="too-long-playlist-name-msg" class="playlist-name-error-feedback">
                Playlist name must be less than 100 characters.
            </p>`;
        $("#playlist-form").append(tooManyCharsMsg);
    }
    else if (playlistName.length == 0) {
        console.log(`Playlist name will default to 'MCM Playlist'`);
        let emptyInputMsg = `
        <p id="default-playlist-name-msg" class="playlist-name-error-feedback">
                Playlist name will default to <span class="font-bold">MCM Playlist</span> if left blank.
            </p>`;
        $("#playlist-form").append(emptyInputMsg);
        return "MCM Playlist";
    }
    else {
        if (validatePlaylistName(playlistName) == false) {
            let errorMsg = `<p id="invalid-playlist-name-msg" class="playlist-name-error-feedback">
                Playlist name cannot consist of only spaces
            </p>`;

            $("#playlist-form").append(errorMsg);
            return;
        }
    }
}