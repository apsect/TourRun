using System;
using System.Linq;
using System.Web.Mvc;
using TourRun.Models;
using TourRun.Util;

namespace TourRun.Controllers
{
    public class HomeController : Controller
    {
        TourContext db = new TourContext();

        public ActionResult Index()
        {
            dynamic query = (
                from t in db.Tours
                join p in db.Pictures
                on t.id equals p.Tour.id
                select new
                {
                    t.id,
                    t.name,
                    t.route,
                    t.eat,
                    t.places,
                    t.departure,
                    t.duration,
                    t.cost,
                    imagePath = p.imagePath
                }).GroupBy(t => t.id)
                .Select(g => g.FirstOrDefault())
                .AsEnumerable()
                .Select(r => r.ToExpando());
            ViewBag.Tours = query;
            return View();
        }

        public ActionResult TourPage(int id)
        {
            ViewBag.Tour = db.Tours.FirstOrDefault(i => i.id == id);
            ViewBag.Images = from p in db.Pictures
                             where p.Tour.id == id
                             select p;
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult OrderTour(int id)
        {
            ViewBag.TourId = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTour(int? tourid, Order order)
        {
            order.status = "Wait";
            order.orderDate = DateTime.Now;
            order.tourid = tourid;
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(order);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
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