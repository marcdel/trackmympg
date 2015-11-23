using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrackMyMpg.Models;

namespace TrackMyMpg.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var vehicles = await db.Vehicles.Include(vehicle => vehicle.Make).ToListAsync();
            return View(new Report(vehicles).Makes);
        }
    }
}