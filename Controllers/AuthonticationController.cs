using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TripTravelSystem.Models;

namespace TripTravelSystem.Controllers
{
    public class AuthonticationController : Controller
    {
        private TripsTravel_DBEntities db = new TripsTravel_DBEntities();
        //Get:Registration Action
        [HttpGet]
        public ActionResult RegisterPopUp()
        {

            ViewBag.roleTypeID = new SelectList(db.RoleTypes, "roleID", "roleName");
            return PartialView();
        }

        //Post: Registration Action
        [HttpPost]
        public ActionResult RegisterPopUp([Bind(Exclude = "isEmailVerified,ActivationCode")] User user)
        {
            ViewBag.roleTypeID = new SelectList(db.RoleTypes, "roleID", "roleName");
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                var IsExist = IsEmailExist(user.email);
                if (IsExist)
                {
                    ModelState.AddModelError("Emailexist", "Email is already exist");
                    return PartialView(user);
                }

                user.ActivationCode = Guid.NewGuid();
                user.password = Crypto.Hash(user.password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                user.isEmailVerified = false;
                using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();
                    sendVerificationLinkEmail(user.email, user.ActivationCode.ToString());
                    message = "Registration Successfully done. Account activation link has been sent to your email address:" + user.email;
                    Status = true;
                }
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            return PartialView(user);
        }

        [NonAction]
        public bool IsEmailExist(string emailAddress)
        {
            using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
            {
                var v = dc.Users.Where(a => a.email == emailAddress).FirstOrDefault();
                return v != null;
            }
        }

        public void sendVerificationLinkEmail(string email, string acvtivationCode)
        {
            var verifyUrl = "/Authontication/VerifyAccount/" + acvtivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEnail = new MailAddress("triptravelsystem@gmail.com", "Trip Travel");
            var toEmail = new MailAddress(email);
            var fromEmailPass = "ppoo7878";
            string subject = "your account is successfully created";
            string body = "<br/><br/> We are exited to tell you that your Trip Travel account is Successfully created. Click on the below link to verify your account " +
                "<br/><br/> <a href='" + link + "'>" + link + "</a>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEnail.Address, fromEmailPass)
            };
            using (var message = new MailMessage(fromEnail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }

        //Verifiy email
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.isEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }




        //Login Action
        [HttpGet]
        public ActionResult LoginPopUp()
        {

            return PartialView();
        }

        //post:Login Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPopUp(UserLogin login, string ReturnUrl)
        {
            string message = "";
            using (TripsTravel_DBEntities dc = new TripsTravel_DBEntities())
            {
                var v = dc.Users.Where(a => a.email == login.EmailAddress).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;  //525600 MIN = 1 YEAR
                        var ticket = new FormsAuthenticationTicket(login.EmailAddress, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {   
                            //to direct every page to the right user
                             
                            if (v.roleTypeID == 3)
                            {
                                return Redirect("/Agency/Index");
                                //return RedirectToAction("Index", "Agency");
                            }
                            else if (v.roleTypeID == 1)
                            {
                                return Redirect("/Admin/Index");
                                //return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Traveler");
                            }

                            //return RedirectToAction("Index", "Traveler");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return PartialView();
        }

        //logout
        [Authorize]
        [HttpPost]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Traveler");
        }
    }
}