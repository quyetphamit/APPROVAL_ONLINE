using SUPPORT_APPROVAL_ONLINE.Models;
using SUPPORT_APPROVAL_ONLINE.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPPORT_APPROVAL_ONLINE.Controllers
{
    public class UserController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        // GET: User
        public ActionResult Index()
        {
            var userSession = Session["user"] as tbl_User;
            int id = userSession.id;
            tbl_User tbl_User =  db.tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var userSession = Session["user"] as tbl_User;
            int id = userSession.id;
            tbl_User tbl_User = db.tbl_User.Find(id);
            return View(tbl_User);
        }
        [HttpPost]
        public ActionResult ChangePassword(tbl_User user)
        {
            //var userSession = Session["user"] as tbl_User;
            //int id = userSession.id;
            //tbl_User tbl_User = db.tbl_User.Find(id);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                var newPass = Common.EncryptionMD5(user.password);
                user.password = newPass;
                //user.id = tbl_User.id;
                //user.createAt = tbl_User.createAt;
                //user.email = tbl_User.email;
                //user.fullname = tbl_User.fullname;
                //user.group_Id = tbl_User.group_Id;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}