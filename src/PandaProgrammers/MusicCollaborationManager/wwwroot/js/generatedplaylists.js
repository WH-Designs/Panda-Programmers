
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
    console.log("save button clicked");

    var $form = $("#playlist-form");
    var actualData = getFormData($form);
    /*    console.log(actualData)*/

    //$.each(actualData, function (index, item) {
    //    console.log(`Item ${index}: ${item}`);
    //});

    console.log("LAST ENTRY: " + actualData["__RequestVerificationToken"]);
    delete actualData["__RequestVerificationToken"];

    let dataAsArray = [];
    $.each(actualData, function (index, item) {
        dataAsArray.push(item);
    });
 
});


function savePlaylist(data) {
    console.log("Printing 'data' (savePlaylist): " + data);  //Assume that 'data' is a bool. C# variable name is: "TracksSuccessfullyAdded".
}

function errorOnAjax(){
    console.log("Error on Ajax.");
}


$("#discard-playlist-btn").click(function () {
    console.log("discard button clicked");
});