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
    public class AgencyController : Controller
    {
        private TripsTravel_DBEntities db = new TripsTravel_DBEntities(); 
        // GET: Agency
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

        // GET: Posts/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Posts/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id,[Bind(Include = "postID,tripTitle,tripDate,tripImage,tripDescription,tripPrice,postDate,uID,numberOfLikes,numberOfDisLikes")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.numberOfDisLikes = 0;
                post.numberOfLikes = 0;
                var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
                if (v != null)
                {
                    post.uID = v.userID;
                    //ViewBag.uID = v.userID;
                } 
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public ActionResult MyPosts(int? id)
        {
            var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
            if (v != null)
            {
                id = v.userID;
            }
            var user = db.Posts.Where(a => a.User.userID == id);

            return View(user.ToList());
        }

        public ActionResult EditProfile(int? id)
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
            ViewBag.roleTypeID = new SelectList(db.RoleTypes.Where(a => a.roleID == 3), "roleID", "roleName", user.roleTypeID);

            return View(user);
        }

        // POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "userID,firstName,lastName,email,photo,roleTypeID,password,ConfirmPaswword,isEmailVerified,ActivationCode")] User user)
        {
            if (ModelState.IsValid)
            {
               
            user.ConfirmPassword = user.password;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.roleTypeID = new SelectList(db.RoleTypes.Where(a=>a.roleID==3), "roleID", "roleName", user.roleTypeID);
            return View(user);
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
            
            //ViewBag.uID = new SelectList(db.Users.Where(a=>a.userID==id), "userID", "firstName", post.uID);
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
                var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
            if (v != null)
            {
                    post.uID = v.userID;
            }
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.uID = new SelectList(db.Users.Where(a => a.userID == id), "userID", "firstName", post.uID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult showQuestions(int? id)
        {
            var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
            if (v != null)
            {
                id = v.userID;
            }
            var q = db.Questions.Where(a => a.agencyID == id);

            return View(q.ToList());
        }
        public ActionResult AddAnswer(int? id)
        {

            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer( [Bind(Include  = "qID, question1, answer, questionDate, UID, agencyID")] Question question)
        {
            if (ModelState.IsValid)
            {
                var v = db.Users.Where(a => a.email == User.Identity.Name).FirstOrDefault();
                if (v != null)
                {
                   //question.
                    //ViewBag.uID = v.userID;
                }

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }


    }
}