using SUPPORT_APPROVAL_ONLINE.Models;
using SUPPORT_APPROVAL_ONLINE.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPPORT_APPROVAL_ONLINE.Controllers
{
    public class LoginController : Controller
    {
        private RequestEntity db = new RequestEntity();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_User user)
        {
            var pass = Common.EncryptionMD5(user.password);
            var userDetail = db.tbl_User.Where(r => r.username == user.username && r.password == pass).FirstOrDefault();
            if (userDetail == null)
            {
                ViewBag.loginInvalid = "Tên đăng nhập hoặc mật khẩu sai";
                return View("Index");
            }
            Session["user"] = userDetail;
            if (userDetail.tbl_Permission.allow == "root")
            {
                return RedirectToAction("Index", "Root");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}