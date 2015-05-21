using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodetrace.Models;
using System.IO;
using System.Drawing.Imaging;

using Gma.QrCodeNet;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace MvcCodetrace.Controllers
{
    public class BouteilleController : Controller
    {
        private CodeTraceEntities db = new CodeTraceEntities();

        //
        // GET: /Bouteille/

        public ActionResult Index()
        {
            var bouteilles = db.Bouteilles.Include(b => b.Code).Include(b => b.Rengement);
            return View(bouteilles.ToList());
        }

        //
        // GET: /Bouteille/Details/5

        public ActionResult Details(int id = 0)
        {
            Bouteille bouteille = db.Bouteilles.Find(id);
            if (bouteille == null)
            {
                return HttpNotFound();
            }
            return View(bouteille);
        }

        //
        // GET: /Bouteille/Create

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace");
            ViewBag.RengementId = new SelectList(db.Rengements, "Id", "QuantiteMax");
            return View();
        }

        //
        // POST: /Bouteille/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bouteille bouteille)
        {
            if (ModelState.IsValid)
            {
                Random rdm = new Random();
                bouteille.Code = new Code { CodeTrace = rdm.Next().ToString()};
                if (bouteille.RengementId != null)
                {
                    Rengement rengement = db.Rengements.Find(bouteille.RengementId);
                    Agregation agregation = new Agregation { Code = rengement.Code, CodeContenu = bouteille.Code.CodeTrace };
                    db.Agregations.Add(agregation);
                }       
                db.Bouteilles.Add(bouteille);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", bouteille.Id);
            ViewBag.RengementId = new SelectList(db.Rengements, "Id", "QuantiteMax", bouteille.RengementId);
            return View(bouteille);
        }

        //
        // GET: /Bouteille/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bouteille bouteille = db.Bouteilles.Find(id);
            if (bouteille == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", bouteille.Id);
            ViewBag.RengementId = new SelectList(db.Rengements, "Id", "QuantiteMax", bouteille.RengementId);
            return View(bouteille);
        }

        //
        // POST: /Bouteille/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bouteille bouteille)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bouteille).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Codes, "Id", "CodeTrace", bouteille.Id);
            ViewBag.RengementId = new SelectList(db.Rengements, "Id", "QuantiteMax", bouteille.RengementId);
            return View(bouteille);
        }

        //
        // GET: /Bouteille/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bouteille bouteille = db.Bouteilles.Find(id);
            if (bouteille == null)
            {
                return HttpNotFound();
            }
            return View(bouteille);
        }

        //
        // POST: /Bouteille/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bouteille bouteille = db.Bouteilles.Find(id);
            var query = from a in db.Agregations
                          where a.CodeContenu == bouteille.Code.CodeTrace
                          select a.Id;
            foreach (int item in query)
            {
                Agregation agregation = db.Agregations.Find(item);
                db.Agregations.Remove(agregation);
            }
            db.Bouteilles.Remove(bouteille);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public string getQrCode(int? id){
            return "<img src=\"/Bouteille/QrCode?id=" + id + "\"/>"; 
        }
        public FileResult qrCode(int? id){
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode;
            MemoryStream ms = new MemoryStream();
            encoder.TryEncode("http://localhost:52857/Bouteille/Details/"+id, out qrCode);
            var render = new GraphicsRenderer(new FixedModuleSize(10, QuietZoneModules.Two));
            render.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            return File(ms.GetBuffer(), @"image/png");
        }
    }
}