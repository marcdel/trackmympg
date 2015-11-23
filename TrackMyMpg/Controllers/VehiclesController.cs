using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TrackMyMpg.Models;
using Microsoft.AspNet.Identity;

namespace TrackMyMpg.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var vehicles = db.Vehicles.Include(vehicle => vehicle.Make).Where(vehicle => vehicle.UserId == userId);

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.Include(v => v.Make).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null || vehicle.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.MakeList = new SelectList(db.Makes, "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,MakeId,Make,Mpg")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.UserId = User.Identity.GetUserId();
                vehicle.Make = db.Makes.Find(vehicle.MakeId);

                db.Vehicles.Add(vehicle);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.MakeList = new SelectList(db.Makes, "Id", "Name", vehicle.MakeId);

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.Include(v => v.Make).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null || vehicle.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,MakeId,Make,Mpg")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if(vehicle.UserId != User.Identity.GetUserId())
                {
                    return HttpNotFound();
                }

                vehicle.Make = db.Makes.Find(vehicle.MakeId);

                db.Entry(vehicle).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.Include(v => v.Make).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null || vehicle.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await db.Vehicles.Include(v => v.Make).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();

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
