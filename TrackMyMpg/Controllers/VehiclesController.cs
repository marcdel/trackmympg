using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackMyMpg.Models;
using Microsoft.AspNet.Identity;

namespace TrackMyMpg.Controllers
{
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
            return View(await db.Vehicles.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Userid,Make,Mpg")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Userid = User.Identity.GetUserId();

                db.Vehicles.Add(vehicle);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Userid,Make,Mpg")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
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

            var vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
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
            var vehicle = await db.Vehicles.FindAsync(id);

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
