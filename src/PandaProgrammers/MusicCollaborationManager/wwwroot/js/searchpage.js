$(document).ready(function () {
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

            if (mcmSearch.status == true) {
                $.ajax({
                    method: "GET",
                    url: "/api/listenerinfo/basicuserinfo/getall",
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    success: displayMCMSearchResults,
                    error: errorOnAjax
                });
            }
            else {
                console.log("(MCM Search) INPUT IS REQUIRED!");
            }
        }
    })
});

function errorOnAjax(data) {
    console.log("ERROR in ajax request: " + data.status + " " + data.statusText);
}




function displaySearchResults(data) {
    console.log('displaySearchResults');
    console.log(data.jsonify);
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
                    let itemID = item[index]["id"];
                    
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
                            <p>${ownerDisplayName}</p>
                            <p>${itemType}</p>
                            <p>${itemReleaseDate}</p>
                            <br>
                            <button name='playlistID' class="hover:text-blue-500 classicpanda:text-blacktext" id="playlist-@playlistIndex" aria-current="page" value="${itemID}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-heart-fill" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z"/>
                                    </svg>
                            </button>
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


    $("#search-query-display").append(`<p id='query-display'>Showing results for: ${searchQuery.value}</p>`);
    
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
        return $("#spotify-radio").val();
    }
    else if ($("#mcm-radio").is(":checked")) {
        console.log("MCM will be searched");
        return $("#mcm-radio").val();
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
    let searchMCM = getSearchQuery();
    
    console.log("MCM search RESULT: " + data);
    console.log("confirming update");

    let searchArray = [];

    $.each(data, function(index) {
        if (data[index]["firstName"].toLowerCase().includes(searchMCM.SearchQuery.toLowerCase()) || 
            data[index]["username"].toLowerCase().includes(searchMCM.SearchQuery.toLowerCase()) || 
            data[index]["lastName"].toLowerCase().includes(searchMCM.SearchQuery.toLowerCase()))
        {
            if (data[index]["username"].length == 0) {
                data[index]["username"] = "Username Not Found";
            };

            searchArray.push(data[index]);
        };
    });
    
    if (searchArray.length == 0)
    {
        let noUserFoundDisplay = `
            <div id="mcm-user-info" class="bg-coreback text-textback pb-4">
                <h3 class="text-center font-bold text-2xl text-textback classicpanda:text-whitetext luxury:text-yellow-500 revolution:text-white autumn:text-white">(No results)</h3>
            </div>`;

        $(noUserFoundDisplay).appendTo("#search-query-display");
    };

    $.each(searchArray, function(index) {
        
        let userInfoDisplay = `
        <table id='search-table' class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                <tr id='search-headers'></tr>
            </thead>
            <tbody id='search-row' class="md:text-center text-lg">
                <div id="mcm-user-info" class="bg-coreback text-textback">
                    <h3 class="text-center font-bold text-2xl text-textback classicpanda:text-whitetext luxury:text-yellow-500 revolution:text-white autumn:text-white">User Info</h3>
                    <div class="table-row-group">
                        <div class="table-row"> 
                            <div class="font-bold table-cell text-textback classicpanda:text-whitetext text-1xl p-3">Full name:</div>
                            <div class="table-cell text-textback classicpanda:text-whitetext text-1xl p-3">${searchArray[index]["firstName"]} ${searchArray[index]["lastName"]}</div>
                        </div>

                        <div class="table-row">
                            <div class="font-bold table-cell text-textback classicpanda:text-whitetext text-1xl p-3">Username:</div>
                            <div class="table-cell text-textback classicpanda:text-whitetext text-1xl p-3">${searchArray[index]["username"]}</div>
                        </div>

                        <div class="table-row">        
                            <div class="font-bold table-cell text-textback classicpanda:text-whitetext text-1xl p-3">Playlists:</div>
                            <button name='spotifyID' class='text-blue-500' aria-current="page" value="${searchArray[index]["spotifyId"]}">${searchArray[index]["firstName"]}'s Playlists</button>
                        </div>
                    </div>
                </div>
            </tbody>
        </table>`

        $(userInfoDisplay).appendTo("#table-placement");
    });
}