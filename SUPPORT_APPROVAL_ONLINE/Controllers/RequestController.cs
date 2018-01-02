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
using System.Net.Mail;
using System.Threading.Tasks;

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
            ViewBag.totalRecord = db.tbl_Request.Where(r => r.U_Id_Approval == sess.id).Count();
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
            var userCreate = db.tbl_User.Find(tbl_Request.U_Id_Create);

            var deptMGN = db.tbl_User.Find(tbl_Request.U_Id_Dept_MNG);

            ViewBag.requestorName = userCreate.fullname;
            ViewBag.requestorPhone = userCreate.phone;
            ViewBag.deptMGNName = deptMGN.fullname;
            ViewBag.deptMNGPhone = deptMGN.phone;
            ViewBag.deptMGNStamp = deptMGN.stamp;
            var lcaLeader = db.tbl_User.Find(tbl_Request.U_Id_LCA_Leader);

            ViewBag.lcaLeaderStamp = lcaLeader.stamp;
            ViewBag.lcaLeaderName = lcaLeader.fullname;
            ViewBag.lcaLeaderPhone = lcaLeader.phone;


            var lcaMNG = db.tbl_User.Find(tbl_Request.U_Id_LCA_MNG);

            ViewBag.lcaMNGName = lcaMNG.fullname;
            ViewBag.lcaMNGPhone = lcaMNG.phone;
            ViewBag.lcaMNGStamp = lcaMNG.stamp;

            var fm = db.tbl_User.Find(tbl_Request.U_Id_FM);
            ViewBag.fmName = fm.fullname;
            ViewBag.fmPhone = fm.phone;
            ViewBag.fmStamp = fm.stamp;

            var gd = db.tbl_User.Find(tbl_Request.U_Id_GD);
            ViewBag.gdName = gd.fullname;
            ViewBag.gdPhone = gd.phone;
            ViewBag.gdStamp = gd.stamp;

            ViewBag.approved = tbl_Request.U_Id_Approval == 1 ? true : false;
            return View(tbl_Request);
        }

        // GET: Request/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name");
            //ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username");
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r=>r.group_Name.Contains("LCA") && !r.group_Name.Contains("MNG")), "group_Id", "group_Name");
            ViewBag.U_Id_Comtor = new SelectList(db.tbl_User.Where(r=>r.tbl_Group.group_Name.Equals("Comtor") && r.tbl_Permission.permission_Id.Equals(2)), "id", "fullname");
            return View();
        }

        // POST: Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_Comtor,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            sess = Session["user"] as tbl_User;

            if (tbl_Request.increaseProductivity == false && tbl_Request.newModel == false && tbl_Request.increaseProduction == false && tbl_Request.improve == false && tbl_Request.C_5s == false && tbl_Request.checkJig == false)
            {
                ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Equals("Support")), "group_Id", "group_Name");
                ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
                ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
                ViewBag.U_Id_Comtor = new SelectList(db.tbl_User.Where(r => r.tbl_Group.group_Name.Equals("Comtor") && r.tbl_Permission.permission_Id.Equals(2)), "id", "fullname",tbl_Request.U_Id_Comtor);
                ViewBag.error = "Chưa chọn phân loại Jig";
                return View(tbl_Request);
            }
            string groupName = db.tbl_User.Find(sess.id).tbl_Group.group_Name;

            tbl_Request.U_Id_LCA_MNG = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("MNG-LCA") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;
            //if (comtor != null)
            //{
            //    tbl_Request.U_Id_Comtor = comtor.id;
            //}
            tbl_Request.U_Id_FM = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("FM") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;
            tbl_Request.U_Id_GD = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("GD") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;

            var deptMngId = db.tbl_User.Where(r => r.tbl_Group.group_Name.Equals(groupName) && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;

            if (ModelState.IsValid)
            {
                tbl_Request.U_Id_Dept_MNG = tbl_Request.U_Id_Approval = deptMngId;
                tbl_Request.U_Id_Create = tbl_Request.U_Id_Send = sess.id;
                tbl_Request.date_Create = DateTime.Now;
                db.tbl_Request.Add(tbl_Request);
                db.SaveChanges();
                Common.SendMail("quyetphamit@gmail.com",sess.fullname, false);
                Common.SendMail("quyetphamit@gmail.com", sess.fullname, true);
                return RedirectToAction("Index");
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Equals("Support")), "group_Id", "group_Name");
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name", tbl_Request.customer_Id);
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username", tbl_Request.U_Id_Create);
            return View(tbl_Request);
        }
        public ActionResult CreateConfirm(int? id)
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
            ViewBag.customer_Id = new SelectList(db.tbl_Customer, "customer_Id", "customer_Name");
            ViewBag.U_Id_Create = new SelectList(db.tbl_User, "U_Id", "U_username");
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA") && !r.group_Name.Contains("MNG")), "group_Id", "group_Name");
            return View(tbl_Request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm([Bind(Include = "id,U_Id_Approval,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_Comtor,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_upload_update,costDetail_upload")] tbl_Request tbl_Request)
        {
            sess = Session["user"] as tbl_User;
            string groupName = db.tbl_User.Find(sess.id).tbl_Group.group_Name;

            // Tìm người phiên dịch
            //var comtor = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains(groupName) && r.tbl_Permission.permission_Id.Equals(3)).FirstOrDefault();
            tbl_Request.U_Id_LCA_MNG = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("MNG-LCA") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;
            //if (comtor != null)
            //{
            //    tbl_Request.U_Id_Comtor = comtor.id;
            //}
            tbl_Request.U_Id_FM = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("FM") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;
            tbl_Request.U_Id_GD = db.tbl_User.Where(r => r.tbl_Group.group_Name.Contains("GD") && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;

            var deptMngId = db.tbl_User.Where(r => r.tbl_Group.group_Name.Equals(groupName) && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault().id;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Request).State = EntityState.Modified;
                tbl_Request.U_Id_Dept_MNG = tbl_Request.U_Id_Approval = deptMngId;
                tbl_Request.U_Id_Create = tbl_Request.U_Id_Send = sess.id;
                tbl_Request.date_Create = DateTime.Now;
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
            if (sess.id == tbl_Request.U_Id_Create)
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
        public ActionResult Edit([Bind(Include = "id,U_Id_Create,U_Id_Approval,U_Id_Send,U_Id_Dept_MNG,U_Id_LCA_Leader,U_Id_LCA_MNG,U_Id_FM,U_Id_GD,customer_Id,quantity,dealLine,title,increaseProductivity,newModel,increaseProduction,improve,C_5s,checkJig,reducePeple,errorContent,currentError,afterError,cost_Savings,other,pay,model,pcb,contentDetail,cost,date_Create,date_Update,date_Received,date_Finish,file_upload,file_update_1,file_update_2,file_update_3,file_update_4,file_update_5,costDetail_upload")] tbl_Request tbl_Request)
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
        public ActionResult SearchYear(int year)
        {
            if (year == DateTime.Now.Year)
            {
                return RedirectToAction("Index");
            }
            sess = Session["user"] as tbl_User;
            ViewBag.totalRecord = db.tbl_Request.Where(r => r.U_Id_Approval == sess.id).Count();
            ViewBag.year = year;
            var result = db.tbl_Request.Where(r => r.date_Create.Year == year).ToList();
            List<RequestStatus> list = Common.Mapping(result);
            return View(list);
        }
        [HttpGet]
        public JsonResult FindUser(int groupId)
        {

            tbl_User user = db.tbl_User.Where(r => r.group_Id == groupId && r.tbl_Permission.permission_Id.Equals(2)).FirstOrDefault();
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
                id = user.id,
                name = user.fullname,
                phone = user.phone
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
