var connection = new signalR.HubConnectionBuilder().withUrl("/RealTimeInteractionHub").build();

$(document).ready(() => {    
    $('[data-toggle="tooltip"]').tooltip();
    $(".delete-item").click((e) => {
        const result = confirm("Do you want to delete this item? \nWARNING! this non refundable operation!");
        if (result === false) {
            e.preventDefault();
        }
    });

    if ($.contains($("body")[0], $("a#username.nav-link")[0])) {
        connection.start().catch(err => {
            return console.error(err.toString());
        });
    }
});

window.onload = function () {
    const $recaptcha = document.querySelector("#g-recaptcha-response");
    if ($recaptcha) {
        $recaptcha.setAttribute("required", "required");
    };
}

function collapseElement(targetElement = "") {
    if ($(targetElement).hasClass("show")) {
        $(targetElement).collapse("hide").removeClass("show");
    }
    else {
        $(targetElement).collapse("show").addClass("show");
    }
}
function showReplyCommentBox(commentId = "") {
    collapseElement(`#${commentId}.card-footer`);
    $(`button#${commentId}`).hide();
    $(`div#${commentId}.reply-commentbox`).removeClass("d-none");
}
function hideReplyCommentBox(commentId = "") {
    $(`button#${commentId}`).show();
    $(`div#${commentId}.reply-commentbox`).addClass("d-none");
}
