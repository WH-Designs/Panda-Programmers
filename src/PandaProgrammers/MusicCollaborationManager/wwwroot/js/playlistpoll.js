$(function () { 
    //Include an automatic ajax request when the page is loaded. The values to submit should be:
    //-username of current user
    //-the spotify playlist ID of the current playlist

    $("#search-form").submit(function (event) {
        event.preventDefault();
    })

    $('#search-button').click(function () {
        $('#search-row').text("");
        $('#search-query-display').text("");
        $(`#search-headers`).text("");


            let search = getSearchQuery();
            console.log("Search: " + JSON.stringify(search));
            if (search.status) {
                $.ajax({
                    method: "POST",
                    url: "/api/spotifyauth/search",
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    data: JSON.stringify(search),
                    success: displaySearchResults,
                    error: errorOnAjax
                });
            }
            else {
                console.log("Error on search status: " + search.status);
            }
        
    })
});

function errorOnAjax(data) {
    console.log("ERROR in ajax request: " + data.status + " " + data.statusText);
}




function displaySearchResults(data) {
    console.log('displaySearchResults');
    console.log(data);
    try {

        let count = 0;

        let searchColNames = `<th scope="col" class=" px-6 py-3">Image</th>
                              <th scope="col" class=" px-6 py-3">Information</th>`;

        $(searchColNames).appendTo(`#search-headers`);

        $.each(data, function (index, item) {
            if (item == null || item == [] || item.length == 0) {
                count += 1;
            }

            if (count == 4) {
                throw new Error("No items found");
            }

            $.each(item, function (index) {

                try {
                    let imageUrl = item[index]["images"][0]['url'];
                    let itemName = item[index]["name"];
                    let itemType = item[index]["type"];
                    let itemReleaseDate = item[index]["releaseDate"];
                    let itemUrl = item[index]["externalUrls"]["spotify"];
                    let ownerDisplayName = item[index]["owner"]["displayName"];

                    if (itemReleaseDate == undefined) {
                        itemReleaseDate = "";
                    }

                    if (itemType == 0) {
                        itemType = "track";
                    }

                    let searchItem =
                        `<tr id='search-row' class="g-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                        <td class="whitespace-nowrap"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                        <td class="whitespace-nowrap">
                            <a class='text-blue-500' href='${itemUrl}'>${itemName}</a>
                            <p>${itemType}</p>
                            <p>${ownerDisplayName}</p>
                            <p>${itemReleaseDate}</p>
                        </td>
                    </tr>`
                    $(searchItem).appendTo(`#search-row`);
                } catch (error) {
                    if (error.message.includes("owner")) {
                        let imageUrl = item[index]["images"][0]['url'];
                        let itemName = item[index]["name"];
                        let itemType = item[index]["type"];
                        let itemReleaseDate = item[index]["releaseDate"];
                        let itemUrl = item[index]["externalUrls"]["spotify"];
                        let ownerDisplayName = "";

                        if (itemReleaseDate == undefined) {
                            itemReleaseDate = "";
                        }

                        if (itemType == 0) {
                            itemType = "track";
                        }

                        let searchItem =
                            `<tr id='search-row' class="g-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                            <td class="whitespace-nowrap"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                            <td class="whitespace-nowrap">
                                <a class='text-blue-500' href='${itemUrl}'>${itemName}</a>
                                <p>${itemType}</p>
                                <p>${ownerDisplayName}</p>
                                <p>${itemReleaseDate}</p>
                            </td>
                        </tr>`
                        $(searchItem).appendTo(`#search-row`);
                    } else if (error.message.includes("images")) {
                        try {
                            let imageUrl = item[index]["album"]["images"][0]['url'];
                            let itemName = item[index]["name"];
                            let itemType = item[index]["type"];
                            let itemReleaseDate = item[index]["releaseDate"];
                            let itemUrl = item[index]["externalUrls"]["spotify"];
                            let trackUri = item[index]["uri"];
                            let ownerDisplayName = "";
                            console.log("Index " + index);
                            console.log("The URI for the track" + itemName + "is :" + trackUri);

                            if (itemReleaseDate == undefined) {
                                itemReleaseDate = "";
                            }

                            if (itemType == 0) {
                                itemType = "track";
                            }

                            let searchItem =
                                `<tr id='search-row' class="g-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                <td class="whitespace-nowrap"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                                <td class="whitespace-nowrap">
                                    <a class='text-blue-500' href='${itemUrl}'>${itemName}</a>
                                    <p>${itemType}</p>
                                    <p>${ownerDisplayName}</p>
                                    <p>${itemReleaseDate}</p>
                                    <button type="button" class="specific-track-to-poll" id="${trackUri}">Start poll to add to playlist</button>
                                </td>
                            </tr>`
                            $(searchItem).appendTo(`#search-row`);
                        } catch (error) {
                            let imageUrl = "";
                            let itemName = item[index]["name"];
                            let itemType = item[index]["type"];
                            let itemReleaseDate = item[index]["releaseDate"];
                            let itemUrl = item[index]["externalUrls"]["spotify"];
                            let ownerDisplayName = "";

                            if (itemReleaseDate == undefined) {
                                itemReleaseDate = "";
                            }

                            if (itemType == 0) {
                                itemType = "track";
                            }

                            let searchItem =
                                `<tr id='search-row' class="g-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                <td class="whitespace-nowrap"><img src='${imageUrl}' width=0 height=0></td>
                                <td class="whitespace-nowra">
                                    <a class='text-blue-500' href='${itemUrl}'>${itemName}</a>
                                    <p>${itemType}</p>
                                    <p>${ownerDisplayName}</p>
                                    <p>${itemReleaseDate}</p>
                                </td>
                            </tr>`
                            $(searchItem).appendTo(`#search-row`);
                        }
                    }
                }
            });
        });

    } catch (error) {
        console.log("error: " + error.status + ', ' + error.message);
        let searchItem = `<tr id='search-row-no-results' class="border-b border-neutral-500">
                            <td class="whitespace-nowrap  px-6 py-4">No results</td>
                            </tr>`

        $(searchItem).appendTo(`#search-row`);
    }
}

function getCheckedFilters() {
    const checkedDict = {
        "All": $("#checkbox-item-1").is(":checked"),
        "Artists": $("#checkbox-item-2").is(":checked"),
        "Playlists": $("#checkbox-item-3").is(":checked"),
        "Tracks": $("#checkbox-item-4").is(":checked"),
        "Albums": $("#checkbox-item-5").is(":checked")
    };

    return checkedDict;
}

function getSearchQuery() {
    const searchQuery = document.getElementById("spotify-search");
    const searchForm = document.getElementById("search-form");
    const checkedList = getCheckedFilters();


    $("#search-query-display").append(`<p id='query-display'>Showing results for: ${searchQuery.value}</p>`);

    if (!searchForm.checkValidity()) {
        console.log("Invalid form validation in getSearchQuery");
        return { status: false };
    }

    return {
        status: true,
        SearchQuery: searchQuery.value,
        CheckedItems: checkedList
    }
}





//--------------------
let isSearchVisible = false;

$("#transition-to-search-track-btn").click(function () {
    $("#user-playlist-contanier").toggle();
    $("#search-form").toggle();
    $("#search-results-container").toggle();

    if (isSearchVisible == false) {
        $("#transition-to-search-track-btn").text("Cancel search");
        isSearchVisible = true;
    }
    else {
        $("#transition-to-search-track-btn").text("Start poll for a track");
        isSearchVisible = false;
    }
});


$('body').on('click', '.specific-track-to-poll', function() {

    console.log("Track has been selected for poll.");
    let trackSelectedUri = $(this).attr('id');

    let randUri = ":spotify:track:0whSaAvMslbr9uSEt6pjQy";
    console.log(randUri.length);

    const values = getPollFormValues();

    //if (values.status) {
    //    $.ajax({
    //        method: "POST",
    //        url: "/api/playlistpolls/createpoll", //REPLACE with YOUR code HERE. You need: trackUri, playlistID
    //        dataType: "json",
    //        contentType: "application/json; charset=UTF-8",
    //        data: JSON.stringify(values),  //REPLACE with YOUR code OWN HERE
    //        success: afterAddTask,  //REPLACE with YOUR code OWN HERE
    //        error: errorOnAjax
    //    });
    //} else {
    //           console.log("POST Status: failed");
    //}
/*    let trackToPoll = $(`#${formToSubmitIndex}`)*/

    //NOTE: You made the form ID like this: id="track-to-poll-${index}-form"
    //let formToSubmitID = `#track-to-poll-${formToSubmitIndex}-form`
    //console.log("The form that would've been submited: " + formToSubmitID);


    //$.ajax({
    //    method: "POST",
    //    url: "/api/playlistpolls/startpoll",
    //    dataType: "json",
    //    contentType: "application/json; charset=UTF-8",
    //    data: JSON.stringify(),  //MISSING URI HERE
    //    success: , //NEED TO CREATE FUNCTION HERE?
    //    error: errorOnAjax
    //});
});

function getPollFormValues() {
    const curplaylistid = document.getElementById("spotify-playlist-id-input");
    const tracktopoll = document.getElementById("track-to-poll-uri-input");
    const startPollForm = document.getElementById("#start-poll-form");
    if (!startPollForm.checkValidity()) {
        return { status: false };
    }

    return {
        spotifyplaylistid: curplaylistid.value,
        tracktopolluri: tracktopoll.value,
        status: true,
    }
}

//$(".specific-track-to-poll").click(function () {
//    console.log("Poll should have started.")
//});