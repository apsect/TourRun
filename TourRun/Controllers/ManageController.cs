using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TourRun.Models;

namespace TourRun.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        TourContext db = new TourContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public ActionResult ControlPanel()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public ActionResult OrderTour()
        {
            ViewBag.tourid = new SelectList(db.Tours, "id", "name");
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public ActionResult OrderTour(Order order)
        {
            order.status = "Wait";
            order.orderDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.tourid = new SelectList(db.Tours, "id", "name", order.tourid);
            return View(order);
        }

        [Authorize(Roles = "manager")]
        public ActionResult OrderList()
        {
            return View((from i in db.Orders where i.status == "Paid" select i));
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReadyOrderList()
        {
            var totalIncome = db.Orders.Where(i => i.status == "Ready").Sum(i => i.Tour.cost);
            ViewBag.TotalIncome = totalIncome;
            var monthIncome = db.Orders.Where(i => i.orderDate.Month == DateTime.Today.Month && 
                                                    i.orderDate.Year == DateTime.Today.Year && 
                                                    i.status == "Ready").Sum(i => i.Tour.cost);
            ViewBag.MonthIncome = monthIncome;
            return View((from o in db.Orders where o.status == "Ready" select o));
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public ActionResult MakeAgreement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            //Тут вставити формування договору як окремого документу
            return View(order);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public ActionResult MakeAgreement(int id)
        {
            Order order = db.Orders.Find(id);
            order.status = "Ready";
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(db.Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}