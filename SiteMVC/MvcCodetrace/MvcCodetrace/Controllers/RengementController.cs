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
    public class RengementController : Controller
    {
        private CodeTraceEntities db = new CodeTraceEntities();

        //
        // GET: /Rengement/

        public ActionResult Index()
        {
            var rengements = db.Rengements.Include(r => r.Cave).Include(r => r.Code);
            return View(rengements.ToList());
        }

        //
        // GET: /Rengement/Details/5

        public ActionResult Details(int id = 0)
        {
            Rengement rengement = db.Rengements.Find(id);
            if (rengement == null)
            {
                return HttpNotFound();
            }
            return View(rengement);
        }

        //
        // GET: /Rengement/Create

        public ActionResult Create()
        {
            ViewBag.CaveId = new SelectList(db.Caves, "Id", "Proprietaire");
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace");
            return View();
        }

        //
        // POST: /Rengement/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rengement rengement)
        {
            if (ModelState.IsValid)
            {
                Random rdm = new Random();
                rengement.Code = new Code { CodeTrace = rdm.Next().ToString()};
                if (rengement.CaveId != null)
                {
                    Cave cave = db.Caves.Find(rengement.CaveId);
                    Agregation agregation = new Agregation { Code = cave.Code, CodeContenu = rengement.Code.CodeTrace };
                    db.Agregations.Add(agregation);
                }                
                db.Rengements.Add(rengement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CaveId = new SelectList(db.Caves, "Id", "Proprietaire", rengement.CaveId);
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", rengement.Id);
            return View(rengement);
        }

        //
        // GET: /Rengement/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Rengement rengement = db.Rengements.Find(id);
            if (rengement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaveId = new SelectList(db.Caves, "Id", "Proprietaire", rengement.CaveId);
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", rengement.Id);
            return View(rengement);
        }

        //
        // POST: /Rengement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rengement rengement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rengement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CaveId = new SelectList(db.Caves, "Id", "Proprietaire", rengement.CaveId);
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", rengement.Id);
            return View(rengement);
        }

        //
        // GET: /Rengement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Rengement rengement = db.Rengements.Find(id);
            if (rengement == null)
            {
                return HttpNotFound();
            }
            return View(rengement);
        }

        //
        // POST: /Rengement/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rengement rengement = db.Rengements.Find(id);
            var query = from a in db.Agregations
                        where a.CodeContenu == rengement.Code.CodeTrace
                        select a.Id;
            foreach (int item in query)
            {
                Agregation agregation = db.Agregations.Find(item);
                db.Agregations.Remove(agregation);
            }
            db.Rengements.Remove(rengement);
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