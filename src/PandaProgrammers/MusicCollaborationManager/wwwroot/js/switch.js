function toggleCheckbox() {
    const checkbox = $('#aiTitle');
    const titleText = $('#titletext');
    const titleInput = $('#titleinput');

    if (checkbox.prop('checked')) {
        checkbox.val(true);
        titleInput.hide();
        titleText.hide();
    } else {
        checkbox.val(false);
        titleInput.show();
        titleText.show();
    }
}

//Used on all playlist generators (except 'TrackInput')
function toggleSingleCheckboxPlaylistVisibility() {
    const checkbox = $('#playlistVisibilityInput');

    if (checkbox.prop('checked')) {
        checkbox.val(true);
    } else {
        checkbox.val(false);
    }
}

//Just for 'TrackInput' generator
function toggleGroupCheckboxPlaylistVisiblity() {
    const mainCheckbox = $(`#playlistVisibilityMain`);

    if (mainCheckbox.prop(`checked`)) {
        console.log("All should now be true");
        $(`.playlistVisibilitySubSwitch`).val(true);
        $('.playlistVisibilitySubSwitch').prop('checked', true);
    }
    else {
        console.log("All should now be false");
        $(`.playlistVisibilitySubSwitch`).val(false);
        $('.playlistVisibilitySubSwitch').prop('checked', false);
    }
}

$('input[type="checkbox"][id="aiTitle"]').on('click', toggleCheckbox);
$('input[type="checkbox"][id="playlistVisibilityInput"]').on('click', toggleSingleCheckboxPlaylistVisibility);
$('input[type="checkbox"][id="playlistVisibilityMain"]').on('click', toggleGroupCheckboxPlaylistVisiblity);