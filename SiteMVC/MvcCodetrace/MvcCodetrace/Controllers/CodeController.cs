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
    public class CodeController : Controller
    {
        private CodeTraceEntities db = new CodeTraceEntities();

        //
        // GET: /Code/

        public ActionResult Index()
        {
            return View(db.Codes.ToList());
        }

        //
        // GET: /Code/Details/5

        public ActionResult Details(int id = 0)
        {
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        //
        // GET: /Code/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Code/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Code code)
        {
            if (ModelState.IsValid)
            {
                db.Codes.Add(code);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(code);
        }

        //
        // GET: /Code/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        //
        // POST: /Code/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Code code)
        {
            if (ModelState.IsValid)
            {
                db.Entry(code).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(code);
        }

        //
        // GET: /Code/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        //
        // POST: /Code/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Code code = db.Codes.Find(id);
            db.Codes.Remove(code);
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