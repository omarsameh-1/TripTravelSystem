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
        public ActionResult Index(string searchBy,string search)
        {
            var posts = db.Posts.Include(p=>p.User);
            if (searchBy == "Price" )
            {
                return View(posts.Where(a => a.tripPrice == search|| search==null).ToList());
            }
            else if (searchBy == "Date" )
            {
                DateTime Date = Convert.ToDateTime(search);
                return View(posts.Where(a => a.tripDate.Equals(Date) || search == null).ToList()); ;
            }
            else
            {
                return View(posts.Where(a => a.User.firstName.StartsWith(search)|| search == null).ToList());
            }
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
        public ActionResult Like(int userid, int postid, int numlike, [Bind(Include = "USERid,POSTid,isLike")] LikeDislike like)
        {
            Post posts = db.Posts.Find(postid);
            if (ModelState.IsValid)
            {
                using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
                {
                    var v = dc.LikeDislikes.Where(a => (a.USERid == userid) && (a.POSTid == postid)).FirstOrDefault();
                    //var S = dc.LikeDislikes.Where(a => (a.USERid == userid) && (a.POSTid == postid) && a.isLike == "false").FirstOrDefault();
                    if (v != null && v.isLike == "false")
                    {
                        LikeDislike liked = db.LikeDislikes.Find(userid, postid);
                        posts.numberOfDisLikes--;
                        db.LikeDislikes.Remove(liked);
                        db.SaveChanges();
                        posts.numberOfLikes = numlike + 1;
                        db.LikeDislikes.Add(like);
                        like.isLike = "true";
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (v == null)
                    {
                        posts.numberOfLikes = numlike+1;
                        db.LikeDislikes.Add(like);
                        like.isLike = "true";
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    } 
                    else if (v != null && v.isLike == "true")
                    {
                        LikeDislike liked = db.LikeDislikes.Find(userid, postid);
                        if (liked != null)
                        {
                            posts.numberOfLikes--;
                            db.LikeDislikes.Remove(liked);
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                   
                }
            }

            ViewBag.POSTid = postid;
            ViewBag.USERid = userid;
            return View(posts);
        }

        [Authorize(Roles = "Traveler")]
        public ActionResult DisLike(int userid, int postid, int numlike, [Bind(Include = "USERid,POSTid,isLike")] LikeDislike Dislike)
        {
            Post posts = db.Posts.Find(postid);
            if (ModelState.IsValid)
            {
                using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
                {
                    var v = dc.LikeDislikes.Where(a => (a.USERid == userid) && (a.POSTid == postid)).FirstOrDefault();
                    //var S = dc.LikeDislikes.Where(a => (a.USERid == userid) && (a.POSTid == postid) && a.isLike == "false").FirstOrDefault();
                    if (v != null && v.isLike == "true")
                    {
                        LikeDislike Disliked = db.LikeDislikes.Find(userid, postid);
                        posts.numberOfLikes--;
                        db.LikeDislikes.Remove(Disliked);
                        db.SaveChanges();
                        posts.numberOfDisLikes = numlike + 1;
                        db.LikeDislikes.Add(Dislike);
                        Dislike.isLike = "false";
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (v == null)
                    {
                        posts.numberOfDisLikes = numlike + 1;
                        db.LikeDislikes.Add(Dislike);
                        Dislike.isLike = "false";
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (v != null && v.isLike == "false")
                    {
                        LikeDislike Disliked = db.LikeDislikes.Find(userid, postid);
                        if (Disliked != null)
                        {
                            db.LikeDislikes.Remove(Disliked);
                            posts.numberOfDisLikes--;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }

                }
            }
            ViewBag.POSTid = postid;
            ViewBag.USERid = userid;
            return View(posts);
        }


       /* public ActionResult MakeQuestion(int userid,[Bind(Include = "UID,question,questionDate")] Question question)
        {
            
        }*/


    }
}