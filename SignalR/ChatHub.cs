using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYPManagment.Models;
using Microsoft.AspNet.SignalR;

namespace FYPManagment.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _Context;
        public ChatHub()
        {
            _Context = new ApplicationDbContext();
        }
        public void Send(string name, string message, int groupId, string senderId)
        {
            // Call the addNewMessageToPage method to update clients.

            var groupChat = new GroupChat
            {
                GroupId = groupId,
                SenderId = senderId,
                Message = message,
                DateTime = DateTime.Now

            };

            _Context.GroupChats.Add(groupChat);
            _Context.SaveChanges();

            Clients.All.addNewMessageToPage(name, message, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
        }
    }
}