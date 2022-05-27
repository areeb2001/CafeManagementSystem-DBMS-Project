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

    public class tblOrderDetailsController : Controller
    {
        private CafeManagementSystemEntities db = new CafeManagementSystemEntities();

        // GET: tblOrderDetails
        public ActionResult Index()
        {
            var tblOrderDetails = db.tblOrderDetails.Include(t => t.tblItem).Include(t => t.tblOrder);
            return View(tblOrderDetails.ToList());
        }

        // GET: tblOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrderDetail tblOrderDetail = db.tblOrderDetails.Find(id);
            if (tblOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblOrderDetail);
        }

        // GET: tblOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemName");
            ViewBag.orderId = new SelectList(db.tblOrders, "OrderId", "OrderId");
            return View();
        }

        // POST: tblOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DetialOrder,orderId,ItemId,qty,price")] tblOrderDetail tblOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.tblOrderDetails.Add(tblOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemName", tblOrderDetail.ItemId);
            ViewBag.orderId = new SelectList(db.tblOrders, "OrderId", "OrderId", tblOrderDetail.orderId);
            return View(tblOrderDetail);
        }

        // GET: tblOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrderDetail tblOrderDetail = db.tblOrderDetails.Find(id);
            if (tblOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemName", tblOrderDetail.ItemId);
            ViewBag.orderId = new SelectList(db.tblOrders, "OrderId", "OrderId", tblOrderDetail.orderId);
            return View(tblOrderDetail);
        }

        // POST: tblOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetialOrder,orderId,ItemId,qty,price")] tblOrderDetail tblOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.tblItems, "ItemId", "ItemName", tblOrderDetail.ItemId);
            ViewBag.orderId = new SelectList(db.tblOrders, "OrderId", "OrderId", tblOrderDetail.orderId);
            return View(tblOrderDetail);
        }

        // GET: tblOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrderDetail tblOrderDetail = db.tblOrderDetails.Find(id);
            if (tblOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblOrderDetail);
        }

        // POST: tblOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblOrderDetail tblOrderDetail = db.tblOrderDetails.Find(id);
            db.tblOrderDetails.Remove(tblOrderDetail);
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
