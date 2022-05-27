using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PorjectMVC.Models;

namespace PorjectMVC.Controllers
{
    [Authorize]

    public class tblItemsController : Controller
    {
        private CafeManagementSystemEntities db = new CafeManagementSystemEntities();

        // GET: tblItems
        public ActionResult Index()
        {
            var tblItems = db.tblItems.Include(t => t.tblCategory);
            return View(tblItems.ToList());
        }

        // GET: tblItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            return View(tblItem);
        }

        // GET: tblItems/Create
        public ActionResult Create()
        {
            ViewBag.Cateid = new SelectList(db.tblCategories, "CategoriesId", "CategoriesName");
            return View();
        }

        // POST: tblItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Cateid,ItemName,size,qty,price")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                db.tblItems.Add(tblItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cateid = new SelectList(db.tblCategories, "CategoriesId", "CategoriesName", tblItem.Cateid);
            return View(tblItem);
        }

        // GET: tblItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cateid = new SelectList(db.tblCategories, "CategoriesId", "CategoriesName", tblItem.Cateid);
            return View(tblItem);
        }

        // POST: tblItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Cateid,ItemName,size,qty,price")] tblItem tblItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cateid = new SelectList(db.tblCategories, "CategoriesId", "CategoriesName", tblItem.Cateid);
            return View(tblItem);
        }

        // GET: tblItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblItem tblItem = db.tblItems.Find(id);
            if (tblItem == null)
            {
                return HttpNotFound();
            }
            return View(tblItem);
        }

        // POST: tblItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblItem tblItem = db.tblItems.Find(id);
            db.tblItems.Remove(tblItem);
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
