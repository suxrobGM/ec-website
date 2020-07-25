$(document).ready(() => {    
    $("[data-toggle=\"tooltip\"]").tooltip();
    $(".delete-item").click((e) => {
        const result = confirm("Do you want to delete this item? \nWARNING! this non refundable operation!");
        if (result === false) {
            e.preventDefault();
        }
    });
    $(".emojis").emojioneArea();
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

function likeArticle(blogId = "") {
    $.ajax({
        url: `/Blog/Ajax?handler=LikeArticle&blogId=${blogId}`,
        type: "GET",
        success: function(result) {
            const likeButton = $(`#${blogId}-likes-count`);

            if (likeButton.hasClass("far fa-heart")) {
                likeButton.removeClass("far fa-heart").addClass("fas fa-heart");
            } else {
                likeButton.removeClass("fas fa-heart").addClass("far fa-heart");
            }
            likeButton.text(` ${result}`);
        }
    });
}

function checkExtension(event) {
    const file = document.querySelector("#select_image");
    if (/\.(jpe?g|png|gif)$/i.test(file.files[0].name) === false) {
        $("#upload_fail_text").text("This is not valid image file");
    }
    else {
        loadFile(event);
        $("#upload_fail_text").text("");
    }
}

function loadFile(event) {
    var reader = new FileReader();
    reader.onload = () => {
        $("#uploaded_image").attr("src", reader.result);
    };
    reader.readAsDataURL(event.target.files[0]);
}