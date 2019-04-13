using HackerNews.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackerNews.Controllers
{
    public class RegistrationController : Controller
    {
        Entities _db = new Entities();

        // GET: Registration
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                RegisterUser(user);
                return Redirect("/Login/Login");
            }
            catch (DbEntityValidationException ex)
            {
                //Log
            }
            catch (DbUpdateException ex)
            {
                //Log
            }
            catch (Exception ex)
            {
                //Log Error 
                return View();
            }
            return View();
        }

        #region private methods

        private void RegisterUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        #endregion
    }
}