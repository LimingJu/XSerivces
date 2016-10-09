using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportingSystem;
using SharedModel;

namespace ReportingSystem.Controllers
{
    public class ReportController : Controller
    {
        private ReportingSystemDbContext db = new ReportingSystemDbContext();

        // GET: Report
        public ActionResult Chart(string chartType)
        {
            ViewBag.ChartTypeString = chartType; 
            return View("Report", db.PosTransactionModels.ToList());
        }

        // GET: Report
        //public ActionResult Index()
        //{
        //    return View(db.PosTransactionModels.ToList());
        //}

        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosTransactionModel posTransactionModel = db.PosTransactionModels.Find(id);
            if (posTransactionModel == null)
            {
                return HttpNotFound();
            }
            return View(posTransactionModel);
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransactionSource,TransactionType,ReceiptId,TerminalId,ShiftId,TransactionInitDateTime,NetAmount,GrossAmount,Currency")] PosTransactionModel posTransactionModel)
        {
            if (ModelState.IsValid)
            {
                db.PosTransactionModels.Add(posTransactionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posTransactionModel);
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosTransactionModel posTransactionModel = db.PosTransactionModels.Find(id);
            if (posTransactionModel == null)
            {
                return HttpNotFound();
            }
            return View(posTransactionModel);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransactionSource,TransactionType,ReceiptId,TerminalId,ShiftId,TransactionInitDateTime,NetAmount,GrossAmount,Currency")] PosTransactionModel posTransactionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posTransactionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posTransactionModel);
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosTransactionModel posTransactionModel = db.PosTransactionModels.Find(id);
            if (posTransactionModel == null)
            {
                return HttpNotFound();
            }
            return View(posTransactionModel);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PosTransactionModel posTransactionModel = db.PosTransactionModels.Find(id);
            db.PosTransactionModels.Remove(posTransactionModel);
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
