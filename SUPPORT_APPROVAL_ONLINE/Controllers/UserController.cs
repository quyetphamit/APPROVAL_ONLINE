using SUPPORT_APPROVAL_ONLINE.Models;
using System;
using System.Collections.Generic;
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