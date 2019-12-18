var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

$(document).ready(() => {    
    $('[data-toggle="tooltip"]').tooltip();
    $(".delete-item").click((e) => {
        let result = confirm("Do you want to delete this item? \nWARNING! this non refundable operation!");
        if (result == false) {
            e.preventDefault()
        }
    });
    $("a.like").hover(
        () => {
            $("a.like i").removeClass().addClass("far fa-heart");
        },
        () => {
            $("a.like i").removeClass().addClass("fas fa-heart");
        }
    );
    $("a.unlike").hover(
        () => {
            $("a.unlike i").removeClass().addClass("fas fa-heart");
        },
        () => {
            $("a.unlike i").removeClass().addClass("far fa-heart");
        }
    );

    if ($.contains($("body")[0], $("a#username.nav-link")[0])) {
        connection.start().catch(err => {
            return console.error(err.toString());
        });
    }
});

function collapseElement(targetElement = '') {
    if ($(targetElement).hasClass('show')) {
        $(targetElement).collapse('hide').removeClass('show');
    }
    else {
        $(targetElement).collapse('show').addClass('show');
    }
}
function showReplyCommentBox(commentId = '') {
    collapseElement(`#${commentId}.card-footer`);
    $(`button#${commentId}`).hide();
    $(`div#${commentId}.reply-commentbox`).removeClass('d-none');
}
function hideReplyCommentBox(commentId = '') {
    $(`button#${commentId}`).show();
    $(`div#${commentId}.reply-commentbox`).addClass('d-none');
}