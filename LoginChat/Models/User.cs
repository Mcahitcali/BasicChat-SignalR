using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LoginChat.Models
{
    public class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }
        public int UserID { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}