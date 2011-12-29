using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using SignalR_Demo.Models;

namespace SignalR_Demo.Hubs
{
    public class ChatHub : Hub, IDisconnect
    {
        public static List<UserEntity> _users = new List<UserEntity>();

        public void Subscribe(string username)
        {
            UserEntity user = new UserEntity() { username = username, id = Context.ClientId };
            _users.Add(user);
            // returns back to the caller the list of users
            Caller.addUsers(_users.ToArray());
            // notifies all clients that a new user is online
            Clients.addUser(user);
        }

        public void SendMessage(MessageEntity message)
        {
            Clients.receiveMessage(message);
        }

        public void Disconnect()
        {
            var user = _users.Where(u => u.id == Context.ClientId).FirstOrDefault();
            _users.Remove(user);
            Clients.removeUser(user);
        }
    }
}