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
    public class CaveController : Controller
    {
        private CodeTraceEntities db = new CodeTraceEntities();

        //
        // GET: /Cave/

        public ActionResult Index()
        {
            var caves = db.Caves.Include(c => c.Code);
            return View(caves.ToList());
        }

        //
        // GET: /Cave/Details/5

        public ActionResult Details(int id = 0)
        {
            Cave cave = db.Caves.Find(id);
            if (cave == null)
            {
                return HttpNotFound();
            }
            return View(cave);
        }

        //
        // GET: /Cave/Create

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace");
            return View();
        }

        //
        // POST: /Cave/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cave cave)
        {
            if (ModelState.IsValid)
            {
                Random rdm = new Random();
                cave.Code = new Code { CodeTrace = rdm.Next().ToString() };
                db.Caves.Add(cave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", cave.Id);
            return View(cave);
        }

        //
        // GET: /Cave/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cave cave = db.Caves.Find(id);
            if (cave == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", cave.Id);
            return View(cave);
        }

        //
        // POST: /Cave/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cave cave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", cave.Id);
            return View(cave);
        }

        //
        // GET: /Cave/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cave cave = db.Caves.Find(id);
            if (cave == null)
            {
                return HttpNotFound();
            }
            return View(cave);
        }

        //
        // POST: /Cave/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cave cave = db.Caves.Find(id);
            db.Caves.Remove(cave);
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