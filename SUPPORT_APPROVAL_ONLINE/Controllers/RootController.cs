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
    public class RootController : BaseController
    {
        private RequestEntity db = new RequestEntity();
        private tbl_User userSession = new tbl_User();
        // GET: Root
        public ActionResult Index()
        {
            var tbl_User = db.tbl_User.Include(t => t.tbl_Group).Include(t => t.tbl_Permission);
            return View(tbl_User.ToList());
        }
        public ActionResult ListRequest()
        {
            userSession = Session["user"] as tbl_User;
            var tbl_Request = db.tbl_Request.Include(t => t.tbl_Customer).Include(t => t.tbl_User);
            ViewBag.totalRecord = db.tbl_Request.Where(r => r.U_Id_Approval == userSession.id).Count();
            return View(tbl_Request.ToList());
        }
        public ActionResult RequestDetails(int? id)
        {
            userSession = Session["user"] as tbl_User;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            if (tbl_Request == null)
            {
                return HttpNotFound();
            }
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group.Where(r => !r.group_Name.Contains("Support") && r.group_Name.Contains("LCA")), "group_Id", "group_Name");
            return View(tbl_Request);
        }
        public ActionResult RequestDelete(int? id)
        {
            userSession = Session["user"] as tbl_User;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            if (tbl_Request == null)
            {
                return HttpNotFound();
            }

            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_Request);

        }
        [HttpPost, ActionName("RequestDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RequestDeleteConfirm(int? id)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }

            tbl_Request tbl_Request = db.tbl_Request.Find(id);
            db.tbl_Request.Remove(tbl_Request);
            db.SaveChanges();
            return RedirectToAction("ListRequest");

        }
        // GET: Root/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_User tbl_User = db.tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }

        // GET: Root/Create
        public ActionResult Create()
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group, "group_Id", "group_Name");
            ViewBag.permission_Id = new SelectList(db.tbl_Permission, "permission_Id", "allow");
            return View();
        }

        // POST: Root/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,group_Id,permission_Id,username,password,fullname,phone,email,stamp")] tbl_User tbl_User)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (db.tbl_User.Any(r=>r.username == tbl_User.username))
                {
                    ViewBag.duplicate = "Tài khoản đã tồn tại";
                    ViewBag.group_Id = new SelectList(db.tbl_Group, "group_Id", "group_Name", tbl_User.group_Id);
                    ViewBag.permission_Id = new SelectList(db.tbl_Permission, "permission_Id", "allow", tbl_User.permission_Id);
                    return View("Create", tbl_User);
                }
                tbl_User.password = Common.EncryptionMD5(tbl_User.password);
                tbl_User.createAt = userSession.fullname;
                db.tbl_User.Add(tbl_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.group_Id = new SelectList(db.tbl_Group, "group_Id", "group_Name", tbl_User.group_Id);
            ViewBag.permission_Id = new SelectList(db.tbl_Permission, "permission_Id", "allow", tbl_User.permission_Id);
            return View(tbl_User);
        }

        // GET: Root/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_User tbl_User = db.tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group, "group_Id", "group_Name", tbl_User.group_Id);
            ViewBag.permission_Id = new SelectList(db.tbl_Permission, "permission_Id", "allow", tbl_User.permission_Id);
            return View(tbl_User);
        }

        // POST: Root/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,group_Id,permission_Id,username,password,fullname,phone,email,stamp,createAt")] tbl_User tbl_User)
        {
            var newPass = Common.EncryptionMD5(tbl_User.password);
            tbl_User.password = newPass;
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbl_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_Id = new SelectList(db.tbl_Group, "group_Id", "group_Name", tbl_User.group_Id);
            ViewBag.permission_Id = new SelectList(db.tbl_Permission, "permission_Id", "allow", tbl_User.permission_Id);
            return View(tbl_User);
        }

        // GET: Root/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_User tbl_User = db.tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }

        // POST: Root/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            tbl_User tbl_User = db.tbl_User.Find(id);
            db.tbl_User.Remove(tbl_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult GroupIndex()
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(db.tbl_Group.ToList());
        }
        [HttpGet]
        public ActionResult GroupCreate()
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupCreate([Bind(Include = "group_Id,group_Name")] tbl_Group tbl_Group)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.tbl_Group.Add(tbl_Group);
                db.SaveChanges();
                return RedirectToAction("GroupIndex");
            }

            return View(tbl_Group);
        }
        public ActionResult GroupDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Group tbl_Group = db.tbl_Group.Find(id);
            if (tbl_Group == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_Group);
        }
        public ActionResult GroupEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Group tbl_Group = db.tbl_Group.Find(id);
            if (tbl_Group == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_Group);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupEdit([Bind(Include = "group_Id,group_Name")] tbl_Group tbl_Group)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GroupIndex");
            }
            return View(tbl_Group);
        }
        public ActionResult GroupDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Group tbl_Group = db.tbl_Group.Find(id);
            if (tbl_Group == null)
            {
                return HttpNotFound();
            }
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            return View(tbl_Group);
        }
        [HttpPost, ActionName("GroupDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDelete(int id)
        {
            userSession = Session["user"] as tbl_User;
            if (!userSession.tbl_Permission.permission_Id.Equals(1))
            {
                return HttpNotFound();
            }
            tbl_Group tbl_Group = db.tbl_Group.Find(id);
            db.tbl_Group.Remove(tbl_Group);
            db.SaveChanges();
            return RedirectToAction("GroupIndex");
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
