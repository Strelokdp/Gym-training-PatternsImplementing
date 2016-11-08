using System.Web.Mvc;
using Gym_sports_training.Repository.DAL;

namespace Gym_sports_training.Controllers
{
    public class HomeController : Controller
    {
        private GymContext db = new GymContext();

        // GET: TrainingSessions
        public ActionResult Index()
        {
            return Redirect("http://localhost:61467/TrainingSessions");
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}