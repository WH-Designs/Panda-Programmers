$(function () {
    $("#search-form").submit(function (event) {
        event.preventDefault();
    })

    $('#search-button').click(function() {
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

    $.each(data, function (index, item) {
        let searchItem = `<tr id='search-row' class="border-b border-neutral-500">
                          <td class="whitespace-nowrap  px-6 py-4">${item["items"][0]["name"]}</td>
                          </tr>`
        $(searchItem).appendTo(`#search-row`);
    });
}


function getSearchQuery() {
    const searchQuery = document.getElementById("spotify-search");
    const searchForm = document.getElementById("search-form");
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