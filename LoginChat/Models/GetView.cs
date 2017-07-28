using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginChat.Models
{
    public class GetView
    {
        public Message messages { get; set; }
        public IEnumerable<ViewModel> view { get; set; }
        public List<SelectListItem> UserNames { get; set; }
    }
}