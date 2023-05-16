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

function toggleCheckboxPlaylistOnSpotifyVisibility() {
    const checkbox = $('#playlistVisibilityInput');

    if (checkbox.prop('checked')) {
        checkbox.val(true);
    } else {
        checkbox.val(false);
    }
}

$('input[type="checkbox"][id="aiTitle"]').on('click', toggleCheckbox);
$('input[type="checkbox"][id="playlistVisibilityInput"]').on('click', toggleCheckboxPlaylistOnSpotifyVisibility);