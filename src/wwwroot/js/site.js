var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

$(document).ready(() => {    
    $('[data-toggle="tooltip"]').tooltip();

    $(".delete-item").click((e) => {
        let result = confirm("Do you want to delete this item? \nWARNING! this non refundable operation!");
        if (result == false) {
            e.preventDefault()
        }
    });  

    if ($.contains($("body")[0], $("a#username.nav-link")[0])) {
        connection.start().catch(err => {
            return console.error(err.toString());
        });
    }
});