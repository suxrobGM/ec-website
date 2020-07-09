var connection = new signalR.HubConnectionBuilder().withUrl("/ApplicationHub").build();

connection.start().catch(err => {
    return console.error(err.toString());
});
