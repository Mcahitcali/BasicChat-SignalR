using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginChat.Models;

namespace LoginChat.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var db = new UserContext())
            {
                List<User> users = db.Users.ToList();
                if (users == null || users.Count == 0)
                {
                    return RedirectToAction("Create");
                }
                return View(users);
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "UserID,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new UserContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(user);
        }


        public ActionResult Login()
        {
            if (Session["isLogin"].ToString() == "False")
            {
                return View();
            }
            else
                return RedirectToAction("Details");
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if (Session["isLogin"].ToString() == "False")
            {
                using (var db = new UserContext())
                {
                    var get_User = db.Users.FirstOrDefault(s => s.Username == Username && s.Password == Password);
                    if (get_User != null)
                    {
                        Session["Username"] = get_User.Username.ToString();
                        Session["UserID"] = get_User.UserID.ToString();
                        Session["isLogin"] = "True";
                        return RedirectToAction("Details", get_User.UserID);
                    }
                }
            }
            else
            {
                return RedirectToAction("Details", Session["UserID"].ToString());
            }
            return View();
        }
        public ActionResult Details()
        {
            if (Session["isLogin"].ToString() == "False")
            {
                return RedirectToAction("Login");
            }
            else
                return View();
        }

        public ActionResult SendMessage()
        {
            if (Session["isLogin"].ToString() == "False")
            {
                return RedirectToAction("Login");
            }
            else
                return View();
        }


        public ActionResult CreateMessage()
        {

            GetView TopModel;
            IEnumerable<ViewModel> view;
            List<SelectListItem> UserNames = new List<SelectListItem>();
            var db = new UserContext();
            

                List<User> users = db.Users.ToList();
                foreach (var item in users)
                {
                UserNames.Add(new SelectListItem
                    {
                        Text = item.Username,
                        Value = item.UserID.ToString()
                    });
                }
            view = (IEnumerable<ViewModel>)getMessage();
            
            TopModel = new GetView
            {
                messages = new Message(),
                view=view,
                UserNames= UserNames
            };
            return View(TopModel);
        }
        [HttpPost]
        public ActionResult CreateMessage(GetView model)
        {
            if (ModelState.IsValid)
            {
                string userID = Session["UserID"].ToString();
                int tempVal;
                int? id = Int32.TryParse(userID, out tempVal) ? tempVal : (int?)null;

                model.messages.FromID = id;
                using (var db = new UserContext())
                {
                    db.Messages.Add(model.messages);
                    db.SaveChanges();
                   //return RedirectToAction("ListToMessage");
                }
            }
            return RedirectToAction("CreateMessage");
        }
        public ActionResult ListToMessage()
        {

            List<ViewModel> ViewMessage = getMessage();

            return View(ViewMessage);

        }
        public List<ViewModel> getMessage()
        {
            UserContext db = new UserContext();

            var query = from m in db.Messages
                        join u in db.Users
  on m.FromID equals u.UserID
                        join uu in db.Users on m.ToID equals uu.UserID into l
                        from a in l.DefaultIfEmpty()
                        select new
                        {
                            Content = m.MessageContent,
                            To = a.Username,
                            From = u.Username
                        };

            string userID = Session["Username"].ToString();
            query = query.Where(u => u.From == userID||u.To==userID);
            List<ViewModel> ViewMessage = new List<ViewModel>();

            foreach (var item in query)
            {
                ViewModel vm = new ViewModel()
                {
                    MessageContent = item.Content,
                    FromUsername = item.From,
                    ToUsername = item.To
                };
                ViewMessage.Add(vm);
            }

            return ViewMessage;
        }
    }
}