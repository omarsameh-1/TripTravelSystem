using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripTravelSystem.Models;

namespace TripTravelSystem.Controllers
{
    public class AdminController : Controller  
    {
        private TripsTravel_DBEntities db = new TripsTravel_DBEntities();
        // GET: Admin
        public ActionResult Index(int? id)
        {
            var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
            if (v != null)
            {
                id = v.userID;
            }
            User user = db.Users.Find(id);

            return View(user);
        }

        public ActionResult ShowUsers()
        {
            var users = db.Users.Where(a => a.roleTypeID == 2 ||a.roleTypeID==3);
            return View(users.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.roleTypeID = new SelectList(db.RoleTypes, "roleID", "roleName");
            return View();
        }

       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,firstName,lastName,email,photo,roleTypeID,password,isEmailVerified,ConfirmPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("ShowUsers");
            }
            user.ConfirmPassword = user.password;
            ViewBag.roleTypeID = new SelectList(db.RoleTypes, "roleID", "roleName", user.roleTypeID);
            return View(user);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ShowUsers");
        }

        public ActionResult ShowPosts()
        {
            var posts = db.Posts.Include(p=>p.User);
            return View(posts.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.uID = new SelectList(db.Users.Where(a => a.roleTypeID == 3), "userID", "firstName", post.uID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postID,tripTitle,tripDate,tripImage,tripDescription,tripPrice,postDate,uID,numberOfLikes,numberOfDisLikes")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.numberOfDisLikes = 0;
                post.numberOfLikes = 0;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowPosts");
            }
            ViewBag.uID = new SelectList(db.Users.Where(a=>a.roleTypeID==3), "userID", "firstName", post.uID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("ShowPosts");
        }

    }
}