// queueHub.js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/queueHub")
    .build();

connection.start().then(() => {
    console.log("SignalR connected to QueueHub");
}).catch(err => console.error("SignalR error:", err));
