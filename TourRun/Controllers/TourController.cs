using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TourRun.Models;

namespace TourRun.Controllers
{
    [Authorize(Roles = "manager")]
    public class TourController : Controller
    {
        private TourContext db = new TourContext();

        // GET: Tour
        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }

        // GET: Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tour/Create
        public ActionResult Create()
        {
            MultiSelectList hotels = new MultiSelectList(db.Hotels, "id", "name");
            ViewBag.Hotels = hotels;
            MultiSelectList transports = new MultiSelectList(db.Transports, "id", "name");
            ViewBag.Transports = transports;
            return View();
        }

        // POST: Tour/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id,name,route,eat,places,departure,duration,cost,plan,description")]
            Tour tour, IEnumerable<int> hotels, IEnumerable<int> transports)
        {
            if (ModelState.IsValid)
            {
                if (hotels != null)
                {
                    tour.Hotels = (from hotel in db.Hotels
                                   where hotels.Contains(hotel.id)
                                   select hotel).ToList();
                }
                if (transports != null)
                {
                    tour.Transports = (from transport in db.Transports
                                       where transports.Contains(transport.id)
                                       select transport).ToList();
                }
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour);
        }

        // GET: Tour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            MultiSelectList hotels = new MultiSelectList(db.Hotels, "id", "name");
            ViewBag.Hotels = hotels;
            MultiSelectList transports = new MultiSelectList(db.Transports, "id", "name");
            ViewBag.Transports = transports;
            return View(tour);
        }

        // POST: Tour/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "id,name,route,eat,places,departure,duration,cost,plan,description")]
            Tour tour, IEnumerable<int> hotels, IEnumerable<int> transports)
        {
            if (ModelState.IsValid)
            {
                if (hotels != null)
                {
                    /*foreach (var hotel in db.Hotels.ToList())
                    {
                        if (hotels.Contains(hotel.id))
                        {
                            if (!tour.Hotels.Contains(hotel))
                                tour.Hotels.Add(hotel);
                        }
                        else
                        {
                            if (tour.Hotels.Contains(hotel))
                                tour.Hotels.Remove(hotel);
                        }
                    }*/
                }
                if (transports != null)
                {
                    /*foreach (var transport in db.Transports.ToList())
                    {
                        if (transports.Contains(transport.id))
                        {
                            if (!tour.Transports.Contains(transport))
                                tour.Transports.Add(transport);
                        }
                        else
                        {
                            if (tour.Transports.Contains(transport))
                                tour.Transports.Remove(transport);
                        }
                    }*/
                }
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
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
