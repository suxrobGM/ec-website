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

$(document).ready(() =>
{
    $('#summernote').summernote({
        height: 400,        
    });

    uploadPhotoToggle();
});




      