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
            ViewBag.Message = "LCA REQUEST ONLINE APPROVAL";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ:";

            return View();
        }
        public ActionResult Chart()
        {
            ChartInfo charInfo = new ChartInfo();
            charInfo.lcaTotal = db.tbl_Request.Where(r => r.date_Dept_MNG_Approval != null).Count();
            charInfo.lcaSuccess = db.tbl_Request.Where(r => r.date_LCA_MNG_Approval != null).Count();
            charInfo.mcpcTotal = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("MC + PC")).Count();
            charInfo.mcpcSuccess = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("MC + PC") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.pe1Total = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Maemura")).Count();
            charInfo.pe1Success = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Maemura") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.pe2Total = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Kaneko")).Count();
            charInfo.pe2Success = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Kaneko") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.pe3Total = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Murayama")).Count();
            charInfo.pe3Success = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PE-Murayama") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.pd1Total = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PD1")).Count();
            charInfo.pd1Success = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PD1") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.pd2Total = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PD2")).Count();
            charInfo.pd2Success = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("PD2") && r.date_LCA_MNG_Approval != null).Count();
            charInfo.eduTotal = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("EDU")).Count();
            charInfo.eduSuccess = db.tbl_Request.Where(r => r.tbl_User.tbl_Group.group_Name.Contains("EDU") && r.date_LCA_MNG_Approval != null).Count();
            return View(charInfo);
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