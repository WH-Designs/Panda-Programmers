
//https://stackoverflow.com/questions/11338774/serialize-form-data-to-json - Maciej Pyszyński's answer.
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

/*
    <tr>
      <td>The Sliding Mr. Bones (Next Stop, Pottersville)</td>
      <td>Malcolm Lockyer</td>
      <td>1961</td>
    </tr>
 */


$("#save-playlist-btn").click(function () {

    var $form = $("#playlist-form");
    var actualData = getFormData($form);
    console.log("'Actual data': " + actualData)

    console.log("LAST ENTRY: " + actualData["__RequestVerificationToken"]);
    delete actualData["__RequestVerificationToken"];

    let dataAsArray = [];
    $.each(actualData, function (index, item) {
        dataAsArray.push(item);
        console.log(`Item ${index}, Item: ${item}`)
    });
    console.log("Form input: " + dataAsArray);

    $.ajax({
        method: "POST",
        url: "/api/spotifyauth/savegeneratedplaylist",
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(dataAsArray),
        success: savePlaylist,
        error: errorOnAjax
    });

});

$(".remove-track-btn").click(function(){
    let trackToRemoveId = $(this).attr('id')
    console.log("ID of track removed: " + trackToRemoveId)

    let removedTrackIndex = trackToRemoveId.substring(18);
    console.log("Track removed (ID num only): " + removedTrackIndex);

    let entryToRemoveTrackName = $(`#entry-track-name-${removedTrackIndex}`).text();
    let entryToRemoveAlbumName = $(`#entry-album-name-${removedTrackIndex}`).text();

    console.log(`Removed ENTRY \n Track: ${entryToRemoveTrackName} \n Album: ${entryToRemoveAlbumName}`)

    let removedTrackEntry = `
    <div class="table-row" id="readdable-entry-${removedTrackIndex}">
        <div class="table-cell text-textback classicpanda:text-whitetext p-3 overflow-x-scroll">${entryToRemoveTrackName}</div>
        <div class="table-cell text-textback classicpanda:text-whitetext p-3 overflow-x-scroll">${entryToRemoveAlbumName}</div>
        <button class="cursor-pointer text-textback classicpanda:text-whitetext font-bold p-3 re-add-entry" id="re-add-entry-${removedTrackIndex}">Add</button>
    </div>
    `;

    let trackUri =  $(`#track-${removedTrackIndex}-input`).val();
    $(document).on("click", `#re-add-entry-${removedTrackIndex}`, function(){
        let trackToAddId = $(this).attr('id');
        console.log(`Track to add ID: ${trackToAddId}`);
    
        let trackToAddIndex = trackToAddId.substring(13);
        console.log(`Index of track to REadd: ${trackToAddIndex}`);
        console.log(`RE-ADDING ENTRY. \n Track Uri: ${trackUri}`);
        let entryToReadd = `
            <input id="track-${removedTrackIndex}-input" value="${trackUri}" type="hidden" name="newTrackUris[${removedTrackIndex}]"/>
        `

        $("#playlist-form").append(entryToReadd);

        $(`#song-preview-${removedTrackIndex}`).show();
        $(`#readdable-entry-${removedTrackIndex}`).empty();
        $(`#readdable-entry-${removedTrackIndex}`).remove();
    });

    $("#removed-tracks-table-body").append(removedTrackEntry);
    $(`#song-preview-${removedTrackIndex}`).hide();
    $(`#track-${removedTrackIndex}-input`).remove();

});

function redirectToGenIndex() {
    //https://stackoverflow.com/questions/503093/how-do-i-redirect-to-another-webpage - Govind Singh's answer.
    $(window).attr('location', '/Generator/Index')

}


function savePlaylist(data) {
    //console.log("Result of 'SaveMCMGeneratedPlaylist': " + data);
    let text = "The playlist has been saved to your Spotify account";

    if (data === false) {
        text = "There was an error adding the playlist to your Spotify account";
    }
    

    let popUpMsg = `
    <div class="p-12 bg-coreback moon:bg-gray-500 classicpanda:bg-secondaryback rounded fixed">
            
            <p class=text-textback 
                revolution:text-white 
                autumn:text-white 
                classicpanda:text-textback">${text}. You will be redirected shortly...
            </p>
    </div>`;

    $("#explanation-title").append(popUpMsg);
    setTimeout(redirectToGenIndex, 4000);
}

function errorOnAjax(){
    console.log("Error on Ajax.");
}

let currentTheme = localStorage.theme;

$("#discard-playlist-btn").click(function () {
    let msg = `
    <div class="p-12 bg-coreback moon:bg-gray-500 classicpanda:bg-secondaryback rounded fixed">
            <p class="text-textback 
                revolution:text-white 
                autumn:text-white 
                classicpanda:text-textback">Playlist has been discarded. You will be redirected shortly...
            </p>
    </div>`;
    $("#explanation-title").append(msg);
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