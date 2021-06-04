using System;
using System.Collections.Generic;
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
           /* if (user == null)
            {
                return HttpNotFound();
            }*/
            return View(user);


        }

    }
}