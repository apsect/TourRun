using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourRun.Models;

namespace TourRun.Controllers
{
    [Authorize(Roles = "manager")]
    public class PictureController : Controller
    {
        private TourContext db = new TourContext();

        // GET: Picture
        public ActionResult Index()
        {
            var pictures = db.Pictures.Include(p => p.Tour);
            return View(pictures.ToList());
        }

        // GET: Picture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: Picture/Create
        public ActionResult Create()
        {
            ViewBag.tourid = new SelectList(db.Tours, "id", "name");
            return View();
        }

        // POST: Picture/Create
        [HttpPost]
        public ActionResult Create(int? tourid, IEnumerable<HttpPostedFileBase> images)
        {
            Picture picture = new Picture();
            foreach (var img in images)
            {
                if (img != null && tourid != null)
                {
                    string filePath = "~/Files/" + System.IO.Path.GetFileName(img.FileName);
                    picture.tourid = tourid;
                    picture.imagePath = filePath;
                    if (ModelState.IsValid)
                    {
                        img.SaveAs(Server.MapPath(filePath));
                        db.Pictures.Add(picture);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.tourid = new SelectList(db.Tours, "id", "name", picture.tourid);
                    return View(picture);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Picture/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.tourid = new SelectList(db.Tours, "id", "name", picture.tourid);
            return View(picture);
        }

        // POST: Picture/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tourid,imagePath")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tourid = new SelectList(db.Tours, "id", "name", picture.tourid);
            return View(picture);
        }

        // GET: Picture/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Picture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
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
