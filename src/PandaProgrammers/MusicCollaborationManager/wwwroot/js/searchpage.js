$(function () {
    $("#search-form").submit(function (event) {
        event.preventDefault();
    })

    $('#search-button').click(function() {
        $('#search-row').text("");
        $('#search-query-display').text("");
        $(`#search-headers`).text("");

        $("#mcm-user-info").empty();
        $("#mcm-user-info").remove();


        const spotifyOrMcm = checkIfMcmOrSpotifySearch();
        if (spotifyOrMcm == "spotify") {

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
        }
        else if (spotifyOrMcm == "mcm") {
            let mcmSearch = getSearchQueryMCM();
            console.log("(MCM) You entered: " + mcmSearch);
            //IF TIME, disable Spotify fileter input fields (and also add a mechanism for re-enabling them).
            console.log("MCM SEARCH selected");
            if (mcmSearch.status == true) {


                $.ajax({
                    method: "GET",
                    url: "/api/listenerinfo/basicuserinfo/" + mcmSearch.SearchQuery,
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    success: displayMCMSearchResults,
                    error: errorOnAjax
                });
            }
            else {
                console.log("INPUT IS REQUIRED (WAS EMPTY) [MCM search.]");
            }
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

        let searchColNames = `<th scope="col" class=" px-6 py-4">Image</th>
                              <th scope="col" class=" px-6 py-4">Information</th>`;

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
                    `<tr id='search-row' class="border-b border-gray-800">
                        <td class="whitespace-nowrap  px-6 py-4"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                        <td class="whitespace-nowrap  px-6 py-4">
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
                        `<tr id='search-row' class="border-b border-gray-800">
                            <td class="whitespace-nowrap  px-6 py-4"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                            <td class="whitespace-nowrap  px-6 py-4">
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
                            let ownerDisplayName = "";

                            if (itemReleaseDate == undefined) {
                                itemReleaseDate = "";
                            }
        
                            if (itemType == 0) {
                                itemType = "track";
                            }

                            let searchItem = 
                            `<tr id='search-row' class="border-b border-gray-800">
                                <td class="whitespace-nowrap  px-6 py-4"><a href='${itemUrl}'><img src='${imageUrl}' width=300 height=300></a></td>
                                <td class="whitespace-nowrap  px-6 py-4">
                                    <a class='text-blue-500' href='${itemUrl}'>${itemName}</a>
                                    <p>${itemType}</p>
                                    <p>${ownerDisplayName}</p>
                                    <p>${itemReleaseDate}</p>
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
                            `<tr id='search-row' class="border-b border-gray-800">
                                <td class="whitespace-nowrap  px-6 py-4"><img src='${imageUrl}' width=0 height=0></td>
                                <td class="whitespace-nowrap  px-6 py-4">
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
        let searchItem = `<tr id='search-row' class="border-b border-neutral-500">
                            <td class="whitespace-nowrap  px-6 py-4">No results</td>
                            </tr>`

        $(searchItem).appendTo(`#search-row`);
    }
}

function getCheckedFilters() {
    const checkedDict = {"All" : $("#checkbox-item-1").is(":checked"), 
                         "Artists" : $("#checkbox-item-2").is(":checked"),
                         "Playlists" : $("#checkbox-item-3").is(":checked"),
                         "Tracks" : $("#checkbox-item-4").is(":checked"),
                         "Albums" : $("#checkbox-item-5").is(":checked")};

    return checkedDict;
}

function getSearchQuery() {
    const searchQuery = document.getElementById("spotify-search");
    const searchForm = document.getElementById("search-form"); 
    const checkedList = getCheckedFilters();


    $("#search-query-display").append(`<p>Showing results for: ${searchQuery.value}</p>`);
    
    if (!searchForm.checkValidity()){
        console.log("Invalid form validation in getSearchQuery");
        return { status: false };
    }

    return {
        status: true,
        SearchQuery: searchQuery.value,
        CheckedItems: checkedList
    }
}

//MCM exclusive functions (below)-------------

function checkIfMcmOrSpotifySearch() {

    if ($("#spotify-radio").is(":checked")) {
        console.log("Spotify will be searched");
        let curSpotifyRadioVal = $("#spotify-radio").val();
        return curSpotifyRadioVal;
    }
    else if ($("#mcm-radio").is(":checked")) {
        console.log("MCM will be searched");
        let curMcmRadioVal = $("#mcm-radio").val();
        return curMcmRadioVal;
    }

}

function getSearchQueryMCM() {
    const searchQuery = document.getElementById("spotify-search");
    const searchForm = document.getElementById("search-form"); 
    if (!searchForm.checkValidity()) {
        return { status: false };
    }
    else {
        return {
            status: true,
            SearchQuery: searchQuery.value
        };
    }
}


function displayMCMSearchResults(data) {
    console.log("MCM search RESULT: " + data);
    console.log(data["firstName"]);
    console.log(data["lastName"]);
    console.log(data["username"]);

    let content = `<div id="mcm-user-info" class="bg-coreback text-textback">
        <h3 class="text-center font-bold text-2xl text-textback classicpanda:text-whitetext luxury:text-yellow-500 revolution:text-white autumn:text-white">User Info</h3>


         <div class="table-row-group">
            <div class="table-row"> 
                 <div class="font-bold table-cell text-textback classicpanda:text-whitetext text-1xl p-3">Full name:</div>
                <div class="table-cell text-textback classicpanda:text-whitetext text-1xl p-3">${data["firstName"]} ${data["lastName"]}</div>
            </div>

            <div class="table-row">
                <div class="font-bold table-cell text-textback classicpanda:text-whitetext text-1xl p-3">Username:</div>
                <div class="table-cell text-textback classicpanda:text-whitetext text-1xl p-3">${data["username"]}</div>
            </div>
         </div>
    </div>`;

    $(content).appendTo("#search-query-display")


}