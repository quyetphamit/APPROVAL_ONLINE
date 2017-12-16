using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPPORT_APPROVAL_ONLINE.Models;

namespace SUPPORT_APPROVAL_ONLINE.Controllers
{
    public class BusinessController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        tbl_User sess = new tbl_User();
        //public BusinessController()
        //{
        //    sess = Session["user"] as tbl_User;
        //}

        // GET: Business
        public ActionResult Index()
        {
            var tbl_Request = db.tbl_Request.Include(t => t.tbl_Customer).Include(t => t.tbl_User);
            return View(tbl_Request.ToList());
        }
        [HttpGet]
        public ActionResult getRequestByUserApproval()
        {
            sess = Session["user"] as tbl_User;
            int id = sess.U_Id;
            var result = db.tbl_Request.Where(r => r.U_Id_Approval == id).ToList();
            return View(result);
        }
        [HttpGet]
        public ActionResult getRequestByUserCreate()
        {
            sess = Session["user"] as tbl_User;
            int id = sess.U_Id;
            var result = db.tbl_Request.Where(r => r.U_Id_Create == id).ToList();
            return View(result);
        }
        // GET: Business/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            if (tbl_Request == null)
            {
                return HttpNotFound();
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
            return View(tbl_Request);
        }

        // GET: Business/Create
        public ActionResult Create()
        {
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name");
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username");
            return View();
        }

        // POST: Business/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,U_Id_Create,U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Request.Add(tbl_Request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            return View(tbl_Request);
        }

        // GET: Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            if (tbl_Request == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            //ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            ViewBag.U_Id_Create = tbl_Request.U_Id_Create;
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
            return View(tbl_Request);
        }

        // POST: Business/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,U_Id_Create,U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            //var temp = db.tbl_Request.Find(tbl_Request.id) as tbl_Request;
            if (tbl_Request.date_Finish == null)
            {
                ModelState.AddModelError("dateFinishNull", "Ngày nhận không để trống");
                ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
                //ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
                ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
                return View(tbl_Request);
            }
            if (tbl_Request.cost == null)
            {
                ModelState.AddModelError("costNull", "Báo giá không để trống");
                ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
                //ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
                ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
                return View(tbl_Request);
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Request).State = EntityState.Modified;
                sess = Session["user"] as tbl_User;
                var userApproval = db.tbl_User.Find(tbl_Request.U_Id_Approval) as tbl_User;
                if (userApproval.tbl_Permission.allow.Equals("admin"))
                {
                    var group = userApproval.tbl_Group.group_Name;
                    if (group.Contains("GD"))
                    {
                        tbl_Request.U_Id_GD = tbl_Request.U_Id_Approval;
                    }
                    else if (group.Contains("FM"))
                    {
                        tbl_Request.U_Id_FM = tbl_Request.U_Id_Approval;
                    }
                    else if (group.Contains("MNG"))
                    {
                        tbl_Request.U_Id_LCA_MNG = tbl_Request.U_Id_Approval;
                    }
                    else
                    {
                        tbl_Request.U_Id_LCA_Leader = tbl_Request.U_Id_Approval;
                    }
                }
                tbl_Request.date_Received = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("getRequestByUserApproval", "Business");
            }
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
            return View(tbl_Request);
        }

        // GET: Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            if (tbl_Request == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Request);
        }

        // POST: Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            db.tbl_Request.Remove(tbl_Request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ViewPDF(string name)
        {
            ViewBag.src = name;
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
