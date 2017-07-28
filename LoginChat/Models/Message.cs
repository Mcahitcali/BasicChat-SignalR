using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginChat.Models
{
    public class Message
    {
        public Message()
        {

        }

        public int MessageID { get; set; }
        public int? ToID { get; set; }
        public int? FromID { get; set; }
        public string MessageContent { get; set; }


        [ForeignKey("FromID")]
        public virtual User From { get; set; }
        
    }
}