using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using TripTravelSystem.Models;
using System.Web.Optimization;

namespace TripTravelSystem.Controllers
{
    public class TravelerController : Controller
    {

        private TripsTravel_DBEntities db = new TripsTravel_DBEntities();

        // GET: Traveler
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p=>p.User);
            return View(posts.ToList());
        }

        // save post
        [Authorize(Roles ="Traveler")]
        public ActionResult Save(int userid, int postid, [Bind(Include = "USERid,POSTid,saved")] SavedPost savedPost)
        {

            if (ModelState.IsValid)
            {
                using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
                {
                    var v = dc.SavedPosts.Where(a => (a.USERid == userid) && (a.POSTid==postid)).FirstOrDefault();
                    if (v == null)
                    {
                        db.SavedPosts.Add(savedPost);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript' >  alert('It is already Saved!');  </script>");
                    }
                }
                   

            }

            ViewBag.POSTid = postid;
            ViewBag.USERid = userid;
            ViewBag.saved = true;
            return View(savedPost);
        }

        //show saves
        [Authorize(Roles = "Traveler")]
        public ActionResult ShowSaves(int? userid)
        {
                var v = db.SavedPosts.Where(a => (a.USERid == userid));
                return View(v.ToList()); 
        }

        //delete a save
        [Authorize(Roles = "Traveler")]
        public ActionResult DeleteSaves(int userid, int postid)
        {
            SavedPost savedPost = db.SavedPosts.Find(userid, postid);
            db.SavedPosts.Remove(savedPost);
            db.SaveChanges();

            return RedirectToAction("ShowSaves");
        }

        [Authorize(Roles = "Traveler")]
        public ActionResult Like(int userid, int postid, int numlike)
        {
            Post posts = db.Posts.Find(postid);
            if (ModelState.IsValid)
            {
                
                    posts.numberOfLikes = numlike;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }


            return View(posts);
        }

        [Authorize(Roles = "Traveler")]
        public ActionResult DisLike(int userid, int postid, int numlike)
        {
            Post posts = db.Posts.Find(postid);
            if (ModelState.IsValid)
            {
                try
                {
                    posts.numberOfDisLikes = numlike;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {

                    return Content("<script language='javascript' type='text/javascript' >  alert('It is already liked!');  </script>");
                    
                }
            }
            return View(posts);
        }
    }
}