// Create the connection to the ChatHub
// The URL is the one which was registered in the Configure method of Startup.cs
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub')
    .build();

// Register a callback for the "ReceiveMessage" method call
connection.on('ReceiveMessage',
    function (user, message) {
        var msg = message.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
        var encodedMsg = user + ' says ' + msg;
        var li = document.createElement('li');
        li.textContent = encodedMsg;
        document.getElementById('messagesList').appendChild(li);
    });

// Establish a connection the the SignalR hub, registering a callback for when an error occurs
// during establishment
connection.start().catch(function(err) { console.error(err.toString()) });

document.getElementById('sendButton').addEventListener('click', function (event) {
    var user = document.getElementById('userInput').value;
    var message = document.getElementById('messageInput').value;

    // Send a message to the hub to invoke the "SendMessage" method, registering a callback for when
    // an error occurs during the sending of it
    connection.invoke('SendMessage', user, message).catch(function (err) { console.error(err.toString()); });

    event.preventDefault();
});