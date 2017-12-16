using SUPPORT_APPROVAL_ONLINE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPPORT_APPROVAL_ONLINE.Controllers
{
    public class HomeController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        public ActionResult Index()
        {
            List<int> lst = new List<int>();
            var listYear = db.tbl_Request.Select(s => s.date_Create.Year).Distinct().ToList();
            ViewBag.listYear = listYear;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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