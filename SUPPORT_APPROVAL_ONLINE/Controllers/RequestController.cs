using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUPPORT_APPROVAL_ONLINE.Models;
using SUPPORT_APPROVAL_ONLINE.Util;

namespace SUPPORT_APPROVAL_ONLINE.Controllers
{
    public class RequestController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        private tbl_User sess = new tbl_User();

        // GET: Request
        public ActionResult Index()
        {
            sess = Session["user"] as tbl_User;
            var tbl_Request = db.tbl_Request.Include(t => t.tbl_Customer).Include(t => t.tbl_User);
            ViewBag.totalRecord = db.tbl_Request.Where(r => r.U_Id_Approval == sess.U_Id).Count();
            return View(tbl_Request.ToList());
        }
        [HttpGet]
        public ActionResult Test()
        {
            sess = Session["user"] as tbl_User;
            var tbl_Request = db.tbl_Request.Include(t => t.tbl_Customer).Include(t => t.tbl_User);
            ViewBag.totalRecord = db.tbl_Request.Where(r => r.U_Id_Approval == sess.U_Id).Count();
            List<RequestStatus> list = Common.Mapping(tbl_Request.ToList());
            return View(list);
        }

        // GET: Request/Details/5
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
            return View(tbl_Request);
        }

        // GET: Request/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name");
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username");
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && !r.group_Name.Contains("LCA")), "group_Id", "group_Name");
            return View();
        }

        // POST: Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            sess = Session["user"] as tbl_User;


            if (ModelState.IsValid)
            {
                tbl_Request.U_Id_Dept_MNG = tbl_Request.U_Id_Approval;
                tbl_Request.U_Id_Create = sess.U_Id;
                tbl_Request.date_Create = DateTime.Now;
                db.tbl_Request.Add(tbl_Request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Equals("Support")), "group_Id", "group_Name");
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            return View(tbl_Request);
        }

        // GET: Request/Edit/5
        public ActionResult Edit(int? id)
        {
            sess = Session["user"] as tbl_User;
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
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            if (sess.U_Id == tbl_Request.U_Id_Create)
            {
                return View(tbl_Request);

            }
            return View("Index");
        }

        // POST: Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,U_Id_Create,U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            return View(tbl_Request);
        }

        // GET: Request/Delete/5
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

        // POST: Request/Delete/5
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
        public JsonResult FindUser(int groupId)
        {
            tbl_User user = db.tbl_User.Where(r => r.group_Id == groupId && r.tbl_Permission.allow.Equals("admin")).FirstOrDefault();
            if (user == null)
            {
                return Json(new
                {
                    name = "Chưa xác định",
                    phone = "Chưa xác định"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                id = user.U_Id,
                name = user.U_fullname,
                phone = user.U_phone
            }, JsonRequestBehavior.AllowGet);
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
