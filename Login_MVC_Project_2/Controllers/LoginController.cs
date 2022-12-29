using Login_MVC_Project_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_MVC_Project_2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Login_MVC_Project_2.Models.C_User userModel)
        {
            using (Login2MVCEntities db=new Login2MVCEntities())
            {
                var userDetails = db.C_User.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();

                if (userDetails==null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";

                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}