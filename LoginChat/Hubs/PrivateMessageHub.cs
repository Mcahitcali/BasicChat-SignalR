using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LoginChat.Models;
using Microsoft.AspNet.SignalR;

namespace LoginChat.Hubs
{
    public class PrivateMessageHub : Hub
    {
        static Dictionary<string, string> OnlineUsers = new Dictionary<string, string>();

        
        public void ConnectedUser(string username)
        {
            string ConnectID = Context.ConnectionId;
            try
            {
                OnlineUsers.Add(username, ConnectID);
            }
            catch (Exception)
            {
                OnlineUsers[username] = Context.ConnectionId;
            }
           
        }
            
        public void ShowOnlineUser()
        {
            foreach (var item in OnlineUsers)
            {
                Clients.All.showUsers(item.Key);
            }
        }

        public void send(string Tousername,string FromUsername,string message)
        {
            using (var db= new UserContext())
            {
                List<User> user = db.Users.Where(x => x.Username == Tousername || x.Username==FromUsername).Take(2).ToList();

                var toID = user[0].UserID;
                var fromID=0;
                try
                {
                    fromID = user[1].UserID;
                }
                catch (Exception)
                {

                    fromID = user[0].UserID;
                }

                
                Message messages = new Message
                {
                    FromID=fromID,
                    ToID=toID,
                    MessageContent=message
                };
                db.Messages.Add(messages);
                db.SaveChanges();
            }
            

            string ID= OnlineUsers.Where(kvp => kvp.Key == Tousername).Select(kvp => kvp.Value).FirstOrDefault();
            string ConnectID = ID == null ? Context.ConnectionId : ID;
           Clients.Client(ConnectID).privateMessage(FromUsername,message);
        }
       
    }
}