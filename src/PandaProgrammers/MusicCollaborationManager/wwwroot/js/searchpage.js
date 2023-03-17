$(function () {
    $("#search-form").submit(function (event) {
        event.preventDefault();
    })

    $('#search-button').click(function() {
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

    if (data[0] == []) {
        let searchItem = `<tr id='search-row' class="border-b border-neutral-500">
                            <td class="whitespace-nowrap  px-6 py-4">No results</td>
                          </tr>`

        $(searchItem).appendTo(`#search-row`);
    }

    try {

        let searchColNames = `<th scope="col" class=" px-6 py-4">Image</th>
                              <th scope="col" class=" px-6 py-4">Information</th>`;

        $(searchColNames).appendTo(`#search-headers`);

        $.each(data, function (index, item) {
            if (item.length == 0) {
                let searchItem = `<tr id='search-row' class="border-b border-neutral-500">
                                    <td class="whitespace-nowrap  px-6 py-4">No results</td>
                                  </tr>`

                $(searchItem).appendTo(`#search-row`);
                return false;
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

function getSearchQuery() {
    const searchQuery = document.getElementById("spotify-search");
    const searchForm = document.getElementById("search-form"); 
    // get the checked items and put them in a list
    $("#search-query-display").append(`<p>Showing results for: ${searchQuery.value}</p>`);
    
    if (!searchForm.checkValidity()){
        console.log("Invalid form validation in getSearchQuery");
        return { status: false };
    }

    return {
        status: true,
        SearchQuery: searchQuery.value
    }
}