
//https://stackoverflow.com/questions/11338774/serialize-form-data-to-json - Maciej Pyszyński's answer.
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}


$("#save-playlist-btn").click(function () {

    var $form = $("#playlist-form");
    var actualData = getFormData($form);
/*    console.log(data)*/

 /*   console.log("LAST ENTRY: " + actualData["__RequestVerificationToken"]);*/
    delete actualData["__RequestVerificationToken"];

    let dataAsArray = [];
    $.each(actualData, function (index, item) {
        dataAsArray.push(item);
    });
/*    console.log(dataAsArray);*/
    
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


function savePlaylist(data) {
    console.log("Result of 'SaveMCMGeneratedPlaylist': " + data);
    //Perform redirect
}

function errorOnAjax(){
    console.log("Error on Ajax.");
}


$("#discard-playlist-btn").click(function () {
    console.log("discard button clicked");
    //Perform redirect

});