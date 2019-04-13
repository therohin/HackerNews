using HackerNews.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace HackerNews.Controllers
{
    public class PostController : Controller
    {
        Entities _db = new Entities();
        // GET: Post
        public ActionResult Index()
        {
            Entities db = new Entities();
            return View(db.Posts);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Post post)
        {
            try
            {
                var userName = User.Identity.Name;
                User currentUser = _db.Users.FirstOrDefault(user => string.Equals(user.UserName, userName));
                WebClient webCLient = new WebClient();
                string source = webCLient.DownloadString(post.URL);
                string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;


                post.RelatedUserId = currentUser.UserId;
                post.User = currentUser;
                post.Title = title;

                _db.Posts.Add(post);
                _db.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                //Log
            }
            catch(DbUpdateException ex)
            {
                //Log
            }
            catch(Exception ex)
            {
                //Log
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Vote(int id)
        {
            try { 
            var post = _db.Posts.FirstOrDefault(p => p.PostId == id);
            post.Vote += 1;
            _db.Entry(post).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //Handle DB Error
                //Log
            }
            catch(Exception ex)
            {
                //Log
            }
            return RedirectToAction("Index");
        }
    }
}