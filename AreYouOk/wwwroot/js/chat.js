"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/healthhub").build();
connection.start();

connection.on("ReceiveHealth", function (timestamp, success, statusCode, url, elapsedMilliseconds) {
    console.log("received message " + timestamp + " " + success + " " + statusCode + " " + url + " " + elapsedMilliseconds);
});