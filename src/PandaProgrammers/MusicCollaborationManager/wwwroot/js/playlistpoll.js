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

    ////NEEDS TESTING
    //if (values.status) {
    //    $.ajax({
    //        type: "GET",
    //        dataType: "json",
    //        url: "api/PlaylistPolls/",
    //        success: displayPreExistingPollInfo,
    //        error: errorOnAjax
    //    });
    //} else {
    //    console.log("GET Status: failed");
    //}
});

function errorOnAjax(data) {
    console.log("ERROR in ajax request: " + data.status + " " + data.statusText);
}

//function displayPreExistingPollInfo(data) {

//    let userVoteDecision = data["userVotedYes"];


//    console.log("-----------Displaying pre-existing poll---------------");
//}




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
                            let trackId = item[index]["id"]
                            let ownerDisplayName = "";
                            /*   console.log("Index " + index);*/
                            /*                            console.log("The URI for the track" + itemName + "is :" + trackUri);*/
                            /*            console.log("The ID for the track" + itemName + "is :" + trackId);*/

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
                                    <button type="button" class="specific-track-to-poll" id="${trackId}">Start poll to add to playlist</button>
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


$('body').on('click', '.specific-track-to-poll', function () {

    console.log("Track has been selected for poll.");
    let trackSelectedSpotifyId = $(this).attr('id');

    $("#track-to-poll-uri-input").val(trackSelectedSpotifyId);

    console.log("Track ID: " + trackSelectedSpotifyId);

    const values = getPollFormValues();
    console.log("Playlist ID (from form): " + values.spotifyplaylistid);
    console.log("Track ID (from form) :" + values.tracktopolluri);
    if (values.status) {
        $.ajax({
            method: "POST",
            url: "/api/PlaylistPolls/createpoll",
            dataType: "json",
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(values),
            success: displayNewPoll,
            error: errorOnAjax
        });
    } else {
        console.log("POST Status: failed");
    }
});

function getPollFormValues() {
    const startPollForm = document.getElementById("start-poll-form");
    const curplaylistid = document.getElementById("spotify-playlist-id-input");
    const tracktopoll = document.getElementById("track-to-poll-uri-input");
    if (!startPollForm.checkValidity()) {
        return { status: false };
    }

    return {
        newpollplaylistid: curplaylistid.value,
        newpolltrackid: tracktopoll.value,
        status: true
    }
}

//$(".specific-track-to-poll").click(function () {
//    console.log("Poll should have started.")
//});

function displayNewPoll(data) {

    console.log("Displaying NEW POLL info:")
    console.log(`Track info: \n --artist: ${data["trackArtist"]} \n --name: ${data["trackTitle"]} \n --duration: ${data["trackDuration"]}`);
    console.log(`'Yes' option ID: ${data["yesOptionID"]}`);
    console.log(`'No' option ID: ${data["noOptionID"]}`);
    console.log(`Total votes: ${data["totalPollVotes"]}`);

    clearAbilityToStartAPoll();

    let totalPollVotes = data["totalPollVotes"];
    let playlistFolowerCount = data["playlistFollowerCount"];
    console.log("TotalPollVotes: " + totalPollVotes);
    console.log("PlaylistFollowercount: " + playlistFolowerCount);

    //NEEDS TESTING when it must go immediataly to results.
    if (Number(totalPollVotes) < Number(playlistFolowerCount)) {
        displayPolledTrackInfoForVotedUser(data);
    }
    else { //Make sure that the test values in the api controller can handle the end result.
        console.log("Poll not possible. Playlist follower count too low.");
        displayPollResults(data);
    }
   
}

function clearAbilityToStartAPoll() {

    $("#search-results-container").empty();
    $("#search-results-container").remove();

    $("#transition-to-search-track-btn").remove();
    $("#spotify-search").remove();
    $("#search-button").remove();
    $("#start-poll-form").empty();
    $("#start-poll-form").remove();
}


function getRemoveVoteFormValues() {
    const removeVoteForm = document.getElementById("remove-vote-form");
    const curplaylistid = document.getElementById("remove-vote-playlist-id-input");
    const curUsername = document.getElementById("remove-vote-username-input");
    if (!removeVoteForm.checkValidity()) {
        return { status: false };
    }

    return {
        removevoteplaylistid: curplaylistid.value,
        removevoteusername: curUsername.value,
        status: true
    }
}

//NEEDS TESTING (again)
function displayVoteOptionsForRemovedVote(data) {
    console.log("Vote removed (altered functions)");

    $(".polling-info-container").empty();
    $(".polling-info-container").remove();


    console.log("Displaying VOTE OPTIONS info:")
    console.log(`Track info: \n --artist: ${data["trackArtist"]} \n --name: ${data["trackTitle"]} \n --duration: ${data["trackDuration"]}`);
    console.log(`'Yes' option ID: ${data["yesOptionID"]}`);
    console.log(`'No' option ID: ${data["noOptionID"]}`);
    console.log(`Total votes: ${data["totalPollVotes"]}`);
    console.log(`Playlist follower count: ${data["playlistFollowerCount"]}`);

    displayPolledTrackInfoWithDecisions(data);

}

$('body').on('click', '#remove-vote-btn', function () {
    console.log("Vote removed");

    const values = getRemoveVoteFormValues();
    console.log("Playlist ID (from 'removevote' form): " + values.spotifyplaylistid);
    console.log("Track ID (from 'removevote' form) :" + values.tracktopolluri);
    if (values.status) {
        $.ajax({
            method: "POST",
            url: "/api/PlaylistPolls/removevote",
            dataType: "json",
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(values),
            success: displayVoteOptionsForRemovedVote,
            error: errorOnAjax
        });
    } else {
        console.log("POST Status: failed (for removing a vote)");
    }
});


$('body').on('click', '#create-vote-btn', function () {
    console.log("Vote create (for existing poll)");

    const values = getSubmitVoteFormValues();
    console.log("Playlist ID (from 'create vote' form): " + values.spotifyplaylistid);
    console.log("Track ID (from 'create vote' form) :" + values.tracktopolluri);
    console.log("User decision: " + values.createvoteoptionid);
    if (values.status) {
        $.ajax({
            method: "POST",
            url: "/api/PlaylistPolls/createvote",
            dataType: "json",
            contentType: "application/json; charset=UTF-8",
            data: JSON.stringify(values),
            success: displayResultOfCastVote,
            error: errorOnAjax
        });
    } else {
        console.log("POST Status: failed (for creating a VOTE)");
    }
});



function getSubmitVoteFormValues() {
    const createVoteForm = document.getElementById("create-vote-on-existing-poll-form");
    const curplaylistid = document.getElementById("create-vote-playlist-id-input");
    const curUsername = document.getElementById("create-vote-username-input");

    //https://stackoverflow.com/questions/596351/how-can-i-know-which-radio-button-is-selected-via-jquery -- Peter J's answer.
    const userVoteDecision = $('input[name=CreateVoteOptionId]:checked', '#create-vote-on-existing-poll-form').val();

    if (!createVoteForm.checkValidity()) {
        return { status: false };
    }


    return {
        createvoteplaylistid: curplaylistid.value,
        createvoteusername: curUsername.value,
        createvoteoptionid: userVoteDecision.val,
        status: true
    }
}

//IMPORTANT: The voting process could continue OR END at this stage!
//MISSING the scenario to display (or omit the notification of) in the case that the user did not vote.
function displayResultOfCastVote(data) {

    $(".polling-info-container").empty();
    $(".polling-info-container").remove();

    console.log("----------Displaying results of vote--------------------");
    console.log("Displaying VOTE OPTIONS info:")
    console.log(`Track info: \n --artist: ${data["trackArtist"]} \n --name: ${data["trackTitle"]} \n --duration: ${data["trackDuration"]}`);
    console.log(`'Yes' option ID: ${data["yesOptionID"]}`);
    console.log(`'No' option ID: ${data["noOptionID"]}`);
    console.log(`Total votes: ${data["totalPollVotes"]}`);
    console.log(`Playlist follower count: ${data["playlistFollowerCount"]}`);



    let totalVotes = data["totalPollVotes"];
    let trackTitle = data["trackTitle"];
    let playlistFollowerCount = data["playlistFollowerCount"];
    let yesVotes = data["yesVotes"];
    let noVotes = data["noVotes"];
    let userVotedYes = data["userVotedYes"]

    if (Number(playlistFollowerCount) <= totalVotes) {  //Poll has ended.

         //NEEDS TESTING.
        displayPollResults(data);
    }
    else { //Poll should continue.
        if (userVotedYes == true || userVotedYes == false) { //This user already voted.
            displayPolledTrackInfoForVotedUser(data);
        }
        else { //This user has NOT voted. (Note: Just realized this will NEVER happen after a user has CAST THEIR VOTE. Will leave it as a sort of alt "catch" statement)
            displayPolledTrackInfoWithDecisions(data);
        }
        
    }

}

//Displays for all possible polling scenarios (below)----------------------------


function displayPolledTrackInfoWithDecisions(data) {

    let trackDuration = data["trackDuration"];
    let trackTitle = data["trackTitle"];
    let trackArtist = data["trackArtist"]

    let totalVotes = data["totalPollVotes"];
    let YesVoteOptionId = data["trackArtist"];
    let NoVoteOptionId = data["trackTitle"];

    let curPlaylistId = $("#general-playlist-id").text();
    let curUser = $("#mcm-username").text();
    let playlistFollowercount = data["playlistFollowerCount"];
    console.log("playlistFollowerCount: " + playlistFollowercount);


    let polledTrackInfoWithDecisions = `
        <div class="flex flex-col polling-info-container">
            <div>
                <span id="num-poll-track-votes">${totalVotes}</span>
                out of
                <span id="total-playlist-followers">${playlistFollowercount}</span>
                votes
            </div>
            <div>Add track to playlist?</div>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500" id="track-polled-artist">Artist: ${trackArtist}</span>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500" id="track-polled-name">Track: ${trackTitle}</span>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500" id="track-polled-duration">Duration: ${trackDuration}</span>
            <div class="flex flex-row">
                <form id="create-vote-on-existing-poll-form">
                     <input type="text" class="hidden" id="create-vote-playlist-id-input" name="CreateVotePlaylistId" value="${curPlaylistId}"/>

                     <label for="create-vote-yes-option-id">Yes</label>
                     <input type="radio" id="create-vote-yes-option-id" name="CreateVoteOptionId" value="${YesVoteOptionId}">

                     <label for="create-vote-no-option-id">No</label>
                     <input type="radio" id="create-vote-no-option-id" name="CreateVoteOptionId" value="${NoVoteOptionId}" checked>

                     <input type="text" id="create-vote-username-input" name="CreateVoteUsername" value="${curUser}" class="hidden"/>
                    <button type="button" class="text-textback classicpanda:text-whitetext
                        autumn:text-white
                        revolution:text-white hover:contrast-50" id="create-vote-btn">Submit Vote</button>
                </form>
            </div>
        </div>
    `;


    $("#create-vote-btn").submit(function (event) {
        event.preventDefault();
    })
    $("#polls-header").append(polledTrackInfoWithDecisions);
}


//Should be fine, but need to remove all other values from their parent function (or there could be conflicts).
function displayPolledTrackInfoForVotedUser(data) {

    let totalVotes = data["totalPollVotes"];
    let trackTitle = data["trackTitle"];
    let playlistFollowerCount = data["playlistFollowerCount"];

    let trackDuration = data["trackDuration"];
    let trackArtist = data["trackArtist"];

    let curUser = $("#mcm-username").text();
    let curplaylistID = $("#general-playlist-id").text();

    let userDecisionAsText = "UNKNOWN_USER_DECISION"; //Just a placeholder value. An actual "yes" or "no will ALWAYS exist here.
    let userVotedYes = data["userVotedYes"];
    if (userVotedYes == true) {
        console.log("User want the track on the playlist");
        userDecisionAsText = "Yes";
    }
    else if (userVotedYes == false) {
        console.log("User does NOT want the track on the playlist");
        userDecisionAsText = "No";
    }
    else {
        console.log("User has NOT cast their vote.");
    }
  

    let allowVoteRemovalpolledTrackInfo = `
        <div class="flex flex-col polling-info-container" id="remove-vote-container">
            <div>
                <span id="num-poll-track-votes">${totalVotes}</span>
                out of
                <span id="total-playlist-followers">${playlistFollowerCount}</span>
                votes
            </div>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500" id="track-polled-artist">Artist: ${trackArtist}</span>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500" id="track-polled-name">Track: ${trackTitle}</span>
            <span class="text-textback classicpanda:text-whitetext luxury:text-yellow-500 hidden" id="track-polled-duration">Duration: ${trackDuration}</span>
            <div class="text-textback classicpanda:text-whitetext luxury:text-yellow-500 hidden" id="track-polled-duration">You voted: ${userDecisionAsText}</div>
            <div class="flex flex-row">
                <form id="remove-vote-form">
                    <input type="text" name="RemoveVotePlaylistID" id="remove-vote-playlist-id-input" class="hidden" value="${curplaylistID}">
                    <input type="text" name="RemoveVoteUsername" id="remove-vote-username-input" class="hidden" value="${curUser}"/>
                    <button type="button" class="text-textback classicpanda:text-whitetext
                        autumn:text-white
                        revolution:text-white hover:contrast-50" id="remove-vote-btn">Remove vote</button>
                </form>         
            </div>
        </div>
    `;
    $("#polls-header").append(allowVoteRemovalpolledTrackInfo);
    $("#remove-vote-btn").submit(function (event) {
        event.preventDefault();
    })
}


function displayPollResults(data) {

    console.log("----------------END of poll---------------------------");
    let yesVotes = data["yesVotes"];
    let noVotes = data["noVotes"];
    let trackTitle = data["trackTitle"];

    let pollEndedNotification = "<h3>ERROR_DISPLAYING_POLL_RESULTS</h3>";
    if (yesVotes > noVotes) { //Majority voted "yes". Add the track to the playlist.
        pollEndedNotification = `<h3> Voting ended. "${trackTitle}" has been added to the playlist.</h3>`;
    }
    else { //Majority voted "no".
        pollEndedNotification = `
            <div>
                <h3>Voting ended. Majority have voted not to add "${trackTitle}" to the playlist".</h3>
            </div>`;
    }
    $("#polls-header").append(pollEndedNotification);
}