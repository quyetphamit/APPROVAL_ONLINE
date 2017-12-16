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
    public class RootController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        tbl_User userSession = new tbl_User();
        // GET: Root
        public ActionResult Index()
        {
            userSession = Session["user"] as tbl_User;
            if (userSession.tbl_Permission.allow != "root")
            {
                return HttpNotFound();
            }
            var tbl_Request = db.tbl_Request.Include(t => t.tbl_Customer).Include(t => t.tbl_User);
            return View(tbl_Request.ToList());
        }

        // GET: Root/Details/5
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

        // GET: Root/Create
        public ActionResult Create()
        {
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name");
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username");
            return View();
        }

        // POST: Root/Create
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

        // GET: Root/Edit/5
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
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            return View(tbl_Request);
        }

        // POST: Root/Edit/5
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

        // GET: Root/Delete/5
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

        // POST: Root/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            db.tbl_Request.Remove(tbl_Request);
            db.SaveChanges();
            return RedirectToAction("Index");
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
