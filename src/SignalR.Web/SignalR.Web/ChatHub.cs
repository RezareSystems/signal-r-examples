using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            // Sends a message to all the clients currently connected to the hub
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
