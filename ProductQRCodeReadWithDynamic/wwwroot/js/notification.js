"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("OnConnected", function () {
    OnConnected();
});

function OnConnected() {
    var email = $('#hfEmail').val();
    connection.invoke("SaveUserConnection", email).catch(function (err) {
        return console.error(err.toString());
    })
}

connection.on("ReceivedNotification", function (message) {
    // alert(message);
    DisplayGeneralNotification(message, 'General Message');
});

connection.on("ReceivedPersonalNotification", function (message, email) {
    // alert(message + ' - ' + email);
    DisplayPersonalNotification(message, 'hey ' + email);
});

