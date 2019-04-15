using HackerNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HackerNews.Controllers
{
    public class LoginController : Controller
    {
        HackerNewsModel _db = new HackerNewsModel();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user, string ReturnUrl)
        {
            try
            {
                if (IsValid(user))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return Redirect(ReturnUrl ?? "/Post/Index");
                }
            }
            catch (Exception ex)
            {
                throw;
                //Log Error 
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
            }
            catch(Exception ex)
            {
                throw;
                //Log Error
            }
            return Redirect("/Post/Index");
        }


        #region private methods
        private bool IsValid( User user)
        {
            return _db.Users.Any(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password));
        }
        #endregion

    }
}