using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebChat.Database;
using WebChat.Domain.Entities;
using WebChat.Domain.Models;

namespace WebChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly IWebChatDbContext _dbContext;

        public ChatHub(IDictionary<string, UserConnection> connections, IWebChatDbContext dbContext)
        {
            _connections = connections;
            _dbContext = dbContext;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
            } 

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            var username = Context.User.FindFirst(ClaimTypes.Name).Value;

            await Groups.AddToGroupAsync(Context.ConnectionId, username);

            // UsersConnections is never used in this program. Their support has been added to handle the 'online' user list functions or similar.  
            // All user usernames are taken from the JWT.
            _connections[Context.ConnectionId] = userConnection; 

            await SendAllUsers();
            //await SendMessagesFromDatabase(username);
        }

        public async Task SendMessage(UserMessage userMessage)
        {
            var username = Context.User.FindFirst(ClaimTypes.Name).Value;

            if (!_dbContext.Users.Any(r => r.Username == userMessage.Receiver))
                return;

            var timestamp = DateTime.Now;
            var timestampFormatted = timestamp.ToString("dd MMMM, HH:mm");

            await Clients.Group(username).SendAsync("ReceiveMessage", username, userMessage.Receiver, userMessage.Message, timestampFormatted);
            await Clients.Group(userMessage.Receiver).SendAsync("ReceiveMessage", username, userMessage.Receiver, userMessage.Message, timestampFormatted);
            _dbContext.Messages.Add(new Message()
            {
                Sender = _dbContext.Users.First(r => r.Username == username),
                Receiver = _dbContext.Users.First(r => r.Username == userMessage.Receiver),
                Content = userMessage.Message,
                TimeStamp = timestamp
            });
            _dbContext.SaveChanges();
        }

        public Task SendAllUsers()
        {
            var users = _dbContext.Users.Select(r => r.Username).ToList();

            return Clients.Caller.SendAsync("AvailableUsers", users);
        }

        // Instead of sending messages each receiver when user opens chat,
        // you can use this function to send all messages intended to user on login
        private async Task SendMessagesFromDatabase(string sender)
        {
            if (!_dbContext.Users.Any(r => r.Username == sender))
                return;

            var messages = _dbContext.Messages
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .Where(r => r.Sender.Username == sender || r.Receiver.Username == sender)
                .OrderBy(r => r.TimeStamp)
                .ToList();

            foreach (var message in messages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.Sender.Username, message.Receiver.Username, message.Content, message.TimeStamp.ToString("dd MMMM, HH:mm"));
            }
        } 

        public async Task GetMessages(string receiver)
        {
            var username = Context.User.FindFirst(ClaimTypes.Name).Value;
            if (!_dbContext.Users.Any(r => r.Username == receiver) ||
                !_dbContext.Users.Any(r => r.Username == username))
                return;

            var messages = _dbContext.Messages
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .Where(r => (r.Sender.Username == username && r.Receiver.Username == receiver) ||
                            (r.Sender.Username == receiver && r.Receiver.Username == username))
                .OrderBy(r => r.TimeStamp)
                .ToList();

            foreach(var message in messages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.Sender.Username, message.Receiver.Username, message.Content, message.TimeStamp.ToString("dd MMMM, HH:mm"));
            }
        }

    }
}