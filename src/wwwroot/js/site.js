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

function quoteToPost(threadId = '', postId = '', postAuthorName = '') {
    let postNode = document.createElement('blockquote'); 
    postNode.className = 'blockquote p-1';
    postNode.style = 'border-left: 5px solid #f86466';
    postNode.innerHTML = $(`#${postId}`).html();

    let blockquoteFooter = document.createElement('footer');
    blockquoteFooter.className = 'blockquote-footer';
    blockquoteFooter.innerText = postAuthorName;
    blockquoteFooter.style = 'font-size: 50%';
    postNode.append(blockquoteFooter);

    $(`#${threadId}.post-texteditor`).summernote('insertNode', postNode);
}

$(document).ready(() => {  
    $('.post-texteditor').summernote({
        toolbar: [
            'picture',
            'video',
            'link',
            'codeview'
        ],
        height: 300,
        blockquoteBreakingLevel: 0
    });

    $('.article-texteditor').summernote({        
        height: 300
    });

    $('.delete-item').click((e) => {
        let result = confirm("Do you want to delete this item? \nWARNING! this non refundable operation!");
        if (result == false) {
            e.preventDefault()
        }
    })

    uploadPhotoToggle();
});