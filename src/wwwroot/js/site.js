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
});