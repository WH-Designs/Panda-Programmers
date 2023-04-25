let playlistId;
let listenerId;

console.log("Hello from comment.js");

// Get the data from the page Function
function GetCommentValues() {
    playlistId = $("#playlistId").val();
    listenerId = $("#listenerId").val();
    if (!$("#comment-form")[0].checkValidity()) {
        console.log("One or more form values are invalid");
        return { status: false }
    }
    return {
        status: true,
        Message: $("#comment-message-input").val(),
        Likes: Number(0),
        SpotifyId: playlistId,
        ListenerId: listenerId
    }
}

// Display List of comments Function
function DisplayComments(data) {
    $.each(data,
        function (index, comment) {
            $("#comments-container").append(
                `<div>
                    <h3>${comment.Listener.FirstName}</h3>
                    <p>${comment.Message}</p>
                </div>`
            );
        }
    );
}

// After the form submission Add the Comment Function
function afterAddComment(data) {
    console.log("Successfully added task: " + data.status);
}

// Ajax Error Fuction
function errorOnAjax(data) {
    console.log("ERROR in ajax call: " + data.status);
};

$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/comments",
        success: DisplayComments,
        error: errorOnAjax
    });

    $("#comment-form").submit(function (event) {
        event.preventDefault();
    });

    $("#comment-submit-button").click(function () {
        const data = GetCommentValues();
        console.log(data);
        if (data.status) {
            $.ajax({
                method: "POST",
                url: "/api/comment",
                dataType: "json",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify(data),
                success: afterAddComment,
                error: errorOnAjax
            });
        }
    });
});