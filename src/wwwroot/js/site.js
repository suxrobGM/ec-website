// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function uploadPhotoToggle() {
    $("#uploadPhoto").hide();

    $(".editor-field .btn").on("ended", () => {        
        $("#uploadPhoto").show();
    });

    $("#uploadPhoto").click(() => {
        $("#uploadPhoto").hide();
    });
}

function checkExtension() {
    var file = document.querySelector("#selectPhoto");
    if (/\.(jpe?g|png|gif)$/i.test(file.files[0].name) === false) {
        alert("This file is not image file");
        $("#uploadPhoto").hide();
    }
    else {
        $("#uploadPhoto").show();
    }
}

$(document).ready(() => {
    $('#summernote').summernote({
        height: 400,        
    });

    $('.delete_board').click((e) => {
        let result = confirm("Do you want to delete board? WARNING! deleting this board will remove include all user threads and posts");
        if (result == false) {
            e.preventDefault()
        }
    })

    $('.delete_forumhead').click((e) => {
        let result = confirm("Do you want to delete forum? WARNING! deleting this Forum will remove include all user threads and posts");
        if (result == false) {
            e.preventDefault()
        }
    })

    $('.delete_post').click((e) => {
        let result = confirm("WARNING! Do you want to delete this post?");
        if (result == false) {
            e.preventDefault()
        }
    })

    uploadPhotoToggle();
});




      