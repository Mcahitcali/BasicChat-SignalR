using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LoginChat.Models
{
    public class ViewModel
    {
        public ViewModel()
        {

        }
        [Key]
        public string MessageContent { get; set; }
        public string ToUsername { get; set; }
        public string FromUsername { get; set; }
    }
}