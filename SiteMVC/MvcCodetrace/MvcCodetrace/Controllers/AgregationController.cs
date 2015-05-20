using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodetrace.Models;

namespace MvcCodetrace.Controllers
{
    public class AgregationController : Controller
    {
        private CodeTraceEntities db = new CodeTraceEntities();

        //
        // GET: /Agregation/

        public ActionResult Index()
        {
            var agregations = db.Agregations.Include(a => a.Code);
            return View(agregations.ToList());
        }

        //
        // GET: /Agregation/Details/5

        public ActionResult Details(int id = 0)
        {
            Agregation agregation = db.Agregations.Find(id);
            if (agregation == null)
            {
                return HttpNotFound();
            }
            return View(agregation);
        }

        //
        // GET: /Agregation/Create

        public ActionResult Create()
        {
            ViewBag.CodeId = new SelectList(db.Codes, "Id", "CodeTrace");
            return View();
        }

        //
        // POST: /Agregation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agregation agregation)
        {
            if (ModelState.IsValid)
            {
                db.Agregations.Add(agregation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeId = new SelectList(db.Codes, "Id", "CodeTrace", agregation.CodeId);
            return View(agregation);
        }

        //
        // GET: /Agregation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Agregation agregation = db.Agregations.Find(id);
            if (agregation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeId = new SelectList(db.Codes, "Id", "CodeTrace", agregation.CodeId);
            return View(agregation);
        }

        //
        // POST: /Agregation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agregation agregation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agregation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeId = new SelectList(db.Codes, "Id", "CodeTrace", agregation.CodeId);
            return View(agregation);
        }

        //
        // GET: /Agregation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Agregation agregation = db.Agregations.Find(id);
            if (agregation == null)
            {
                return HttpNotFound();
            }
            return View(agregation);
        }

        //
        // POST: /Agregation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agregation agregation = db.Agregations.Find(id);
            db.Agregations.Remove(agregation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}